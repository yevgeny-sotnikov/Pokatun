using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.Extensions.Caching.Memory;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmValidation;
using Pokatun.Core.Executors;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Core.ViewModels.Collections;
using Pokatun.Data;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.ViewModels.Profile
{
    public sealed class EditHotelProfileViewModel : BaseViewModel<HotelDto, HotelDto>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IMediaPicker _mediaPicker;
        private readonly IHotelsService _hotelsService;
        private readonly IPhotosService _photosService;
        private readonly IMemoryCache _memoryCache;
        private readonly INetworkRequestExecutor _networkRequestExecutor;
        private readonly ValidationHelper _validator;

        private bool _viewInEditMode = true;
        private long _currentHotelId;
        private string _photoFileName;

        public override string Title => Strings.ProfileChanging;

        private Func<CancellationToken, Task<Stream>> _photoStream;
        public Func<CancellationToken, Task<Stream>> PhotoStream
        {
            get { return _photoStream; }
            set { SetProperty(ref _photoStream, value); }
        }

        private string _hotelName = string.Empty;
        public string HotelName
        {
            get { return _hotelName; }
            set
            {
                if (!SetProperty(ref _hotelName, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsHotelNameInvalid));
            }
        }

        private string _fullCompanyName = string.Empty;
        public string FullCompanyName
        {
            get { return _fullCompanyName; }
            set
            {
                if (!SetProperty(ref _fullCompanyName, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsFullCompanyNameInvalid));
            }
        }

        private string _email = string.Empty;
        public string Email
        {
            get { return _email; }
            set
            {
                if (!SetProperty(ref _email, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsEmailInvalid));
            }
        }

        private string _bankCardOrIban = string.Empty;
        public string BankCardOrIban
        {
            get { return _bankCardOrIban; }
            set
            {
                if (!SetProperty(ref _bankCardOrIban, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsBankCardOrIbanInvalid));
            }
        }

        private string _bankName = string.Empty;
        public string BankName
        {
            get { return _bankName; }
            set
            {
                if (!SetProperty(ref _bankName, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsBankNameInvalid));
            }
        }

        private string _usreou = string.Empty;
        public string USREOU
        {
            get { return _usreou; }
            set
            {
                if (!SetProperty(ref _usreou, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsUsreouInvalid));
            }
        }

        private TimeSpan? _checkInTime;
        public TimeSpan? CheckInTime
        {
            get { return _checkInTime; }
            set
            {
                if (!SetProperty(ref _checkInTime, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsCheckInTimeInvalid));
            }
        }

        private TimeSpan? _checkOutTime;
        public TimeSpan? CheckOutTime
        {
            get { return _checkOutTime; }
            set
            {
                if (!SetProperty(ref _checkOutTime, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsCheckOutTimeInvalid));
            }
        }

        private string _withinTerritoryDescription;
        public string WithinTerritoryDescription
        {
            get { return _withinTerritoryDescription; }
            set
            {
                if (!SetProperty(ref _withinTerritoryDescription, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsWithinTerritoryDescriptionInvalid));
            }
        }

        private string _hotelDescription;
        public string HotelDescription
        {
            get { return _hotelDescription; }
            set
            {
                if (!SetProperty(ref _hotelDescription, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsHotelDescriptionInvalid));
            }
        }

        public MvxObservableCollection<ValidatableEntryItemViewModel> PhoneNumbers { get; private set; }

        public MvxObservableCollection<ValidatableEntryItemViewModel> SocialResources { get; private set; }

        public bool IsHotelNameInvalid => CheckInvalid(nameof(HotelName));

        public bool IsFullCompanyNameInvalid => CheckInvalid(nameof(FullCompanyName));

        public bool IsEmailInvalid => CheckInvalid(nameof(Email));

        public bool IsUsreouInvalid => CheckInvalid(nameof(USREOU));

        public bool IsBankNameInvalid => CheckInvalid(nameof(BankName));

        public bool IsBankCardOrIbanInvalid => CheckInvalid(nameof(BankCardOrIban));

        public bool IsCheckInTimeInvalid => CheckInvalid(nameof(CheckInTime));

        public bool IsCheckOutTimeInvalid => CheckInvalid(nameof(CheckOutTime));

        public bool IsWithinTerritoryDescriptionInvalid => CheckInvalid(nameof(WithinTerritoryDescription));

        public bool IsHotelDescriptionInvalid => CheckInvalid(nameof(HotelDescription));

        private MvxAsyncCommand _closeCommand;
        public IMvxAsyncCommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new MvxAsyncCommand(DoCloseCommandAsync));
            }
        }

        private MvxAsyncCommand _addPhotoCommand;
        public IMvxAsyncCommand AddPhotoCommand
        {
            get
            {
                return _addPhotoCommand ?? (_addPhotoCommand = new MvxAsyncCommand(DoAddPhotoCommandAsync));
            }
        }

        private MvxCommand _addPhoneCommand;
        public IMvxCommand AddPhoneCommand
        {
            get
            {
                return _addPhoneCommand ?? (_addPhoneCommand = new MvxCommand(DoAddPhoneCommand));
            }
        }

        private MvxCommand<ValidatableEntryItemViewModel> _deletePhoneCommand;
        public IMvxCommand<ValidatableEntryItemViewModel> DeletePhoneCommand
        {
            get
            {
                return _deletePhoneCommand ?? (_deletePhoneCommand = new MvxCommand<ValidatableEntryItemViewModel>(DoDeletePhoneCommand));
            }
        }

        private MvxCommand _addSocialResourceCommand;
        public IMvxCommand AddSocialResourceCommand
        {
            get
            {
                return _addSocialResourceCommand ?? (_addSocialResourceCommand = new MvxCommand(DoAddSocialResourceCommand));
            }
        }

        private MvxCommand<ValidatableEntryItemViewModel> _removeSocialResourceCommand;
        public IMvxCommand<ValidatableEntryItemViewModel> RemoveSocialResourceCommand
        {
            get
            {
                return _removeSocialResourceCommand ?? (_removeSocialResourceCommand = new MvxCommand<ValidatableEntryItemViewModel>(DoRemoveSocialResourceCommand));
            }
        }

        private MvxAsyncCommand _chooseCheckInTimeCommand;
        public IMvxAsyncCommand ChooseCheckInTimeCommand
        {
            get
            {
                return _chooseCheckInTimeCommand ?? (_chooseCheckInTimeCommand = new MvxAsyncCommand(DoChooseCheckInTimeCommandAsync));
            }
        }

        private MvxAsyncCommand _chooseCheckOutTimeCommand;
        public IMvxAsyncCommand ChooseCheckOutTimeCommand
        {
            get
            {
                return _chooseCheckOutTimeCommand ?? (_chooseCheckOutTimeCommand = new MvxAsyncCommand(DoChooseCheckOutTimeCommandAsync));
            }
        }

        private MvxAsyncCommand _saveChangesCommand;
        public IMvxAsyncCommand SaveChangesCommand
        {
            get
            {
                return _saveChangesCommand ?? (_saveChangesCommand = new MvxAsyncCommand(DoSaveChangesCommandAsync));
            }
        }

        public EditHotelProfileViewModel(
            IMvxNavigationService navigationService,
            IUserDialogs userDialogs,
            IMediaPicker mediaPicker,
            IHotelsService hotelsService,
            IPhotosService photosService,
            IMemoryCache memoryCache,
            INetworkRequestExecutor networkRequestExecutor)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _mediaPicker = mediaPicker;
            _hotelsService = hotelsService;
            _photosService = photosService;
            _memoryCache = memoryCache;
            _networkRequestExecutor = networkRequestExecutor;

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(HotelName), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(HotelName), Strings.HotelNameRequiredMessage));
            _validator.AddRule(nameof(FullCompanyName), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(FullCompanyName), Strings.CompanyNameRequiredMessage));
            _validator.AddRule(
                nameof(BankCardOrIban),
                () => RuleResult.Assert(
                    _viewInEditMode
                    || Regex.IsMatch(BankCardOrIban.Trim(), DataPatterns.BankCard)
                    || Regex.IsMatch(BankCardOrIban.Trim(), DataPatterns.IBAN
                ),
                Strings.ValidBankCardOrIbanNotDefined)
            );

            _validator.AddRule(nameof(BankName), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(BankName), Strings.BankNameRequiredMessage));
            _validator.AddRule(nameof(USREOU), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(USREOU.Trim(), DataPatterns.USREOU), Strings.InvalidUSREOU));
            _validator.AddRule(nameof(CheckInTime), () => RuleResult.Assert(_viewInEditMode || CheckInTime != null, Strings.CheckInTimeDidntSetted));
            _validator.AddRule(nameof(CheckOutTime), () => RuleResult.Assert(_viewInEditMode || CheckOutTime != null, Strings.CheckOutTimeDidntSetted));
            _validator.AddRule(nameof(PhotoStream), () => RuleResult.Assert(_viewInEditMode || PhotoStream != null, Strings.HotelPhotoDidntChoosen));
            _validator.AddRule(nameof(WithinTerritoryDescription), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(WithinTerritoryDescription), Strings.WithinTerritoryDescriptionDidntAdded));
            _validator.AddRule(nameof(HotelDescription), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(HotelDescription), Strings.HotelDescriptionDidntAdded));
            _validator.AddRule(nameof(PhoneNumbers), () => RuleResult.Assert(_viewInEditMode || PhoneNumbers.Any(), Strings.PhoneNumbersArentSetted));
            _validator.AddRule(nameof(SocialResources), () => RuleResult.Assert(_viewInEditMode || SocialResources.Any(), Strings.LinksArentSetted));
            _validator.AddRule(nameof(InvalidPhones), () => RuleResult.Assert(_viewInEditMode || !InvalidPhones.Any(), Strings.InvalidPhones));
            _validator.AddRule(nameof(DuplicatedPhones), () => RuleResult.Assert(_viewInEditMode || !DuplicatedPhones.Any(), Strings.PhonesDuplication));
            _validator.AddRule(nameof(DuplicatedSocialResources), () => RuleResult.Assert(_viewInEditMode || !DuplicatedSocialResources.Any(), Strings.LinksDuplication));

            PhoneNumbers = new MvxObservableCollection<ValidatableEntryItemViewModel>();
            SocialResources = new MvxObservableCollection<ValidatableEntryItemViewModel>();
        }

        public override void Prepare(HotelDto parameter)
        {
            _currentHotelId = parameter.Id;

            _photoFileName = parameter.PhotoUrl;

            if (parameter.PhotoUrl != null)
            {
                PhotoStream = ct => _photosService.GetAsync(parameter.PhotoUrl);
            }
            else PhotoStream = null;

            HotelName = parameter.HotelName;
            FullCompanyName = parameter.FullCompanyName;
            Email = parameter.Email;
            BankCardOrIban = parameter.BankCard == null ? parameter.IBAN : parameter.BankCard.ToString();
            BankName = parameter.BankName;
            USREOU = parameter.USREOU.ToString();
            CheckInTime = parameter.CheckInTime;
            CheckOutTime = parameter.CheckOutTime;
            HotelDescription = parameter.HotelDescription;
            WithinTerritoryDescription = parameter.WithinTerritoryDescription;
            
            PhoneNumbers.AddRange(parameter.Phones.Select(p => new ValidatableEntryItemViewModel(p.Id, p.Number, DeletePhoneCommand, IsPhoneInvalid)));
            SocialResources.AddRange(parameter.SocialResources.Select(sr => new ValidatableEntryItemViewModel(sr.Id, sr.Link, RemoveSocialResourceCommand, IsLinkDuplicated)));
        }

        private bool IsLinkDuplicated(string str)
        {
            return DuplicatedSocialResources.Contains(str);
        }

        private bool IsPhoneInvalid(string phone)
        {
            return DuplicatedPhones.Contains(phone) || IsPhoneDoesntMatchPattern(phone);
        }

        private bool IsPhoneDoesntMatchPattern(string phone)
        {
            return !Regex.IsMatch(phone, Constants.PhonePattern);
        }

        private string[] DuplicatedPhones => GetDuplications(PhoneNumbers);

        private ValidatableEntryItemViewModel[] InvalidPhones => PhoneNumbers.Where(p => IsPhoneDoesntMatchPattern(p.Text)).ToArray();

        private string[] DuplicatedSocialResources => GetDuplications(SocialResources);

        private string[] GetDuplications(IEnumerable<ValidatableEntryItemViewModel> collection)
        {
            return collection.GroupBy(x => x.Text).Where(g => g.Count() > 1).Select(g => g.Key).ToArray();
        }

        private void DoAddPhoneCommand()
        {
            PhoneNumbers.Add(new ValidatableEntryItemViewModel(0, null, DeletePhoneCommand, IsPhoneInvalid));
        }

        private void DoDeletePhoneCommand(ValidatableEntryItemViewModel phoneVM)
        {
            PhoneNumbers.Remove(phoneVM);
        }

        private void DoAddSocialResourceCommand()
        {
            SocialResources.Add(new ValidatableEntryItemViewModel(0, null, RemoveSocialResourceCommand, IsLinkDuplicated));
        }

        private void DoRemoveSocialResourceCommand(ValidatableEntryItemViewModel srVM)
        {
            SocialResources.Remove(srVM);
        }

        private async Task DoChooseCheckInTimeCommandAsync()
        {
            TimePromptResult res = await ShowTimePromptAsync(Strings.CheckInTime, CheckInTime);

            if (!res.Ok) return;

            CheckInTime = res.Value;
        }

        private async Task DoChooseCheckOutTimeCommandAsync()
        {
            TimePromptResult res = await ShowTimePromptAsync(Strings.CheckOutTime, CheckOutTime);

            if (!res.Ok) return;

            CheckOutTime = res.Value;
        }

        private async Task DoAddPhotoCommandAsync()
        {
            FileResult result = await _mediaPicker.PickPhotoAsync();

            if (result == null) return;

            _photoFileName = result.FullPath;
            PhotoStream = ct => result.OpenReadAsync();
        }

        private Task DoCloseCommandAsync()
        {
            return _navigationService.Close(this);
        }

        private async Task DoSaveChangesCommandAsync()
        {
            _viewInEditMode = false;

            ValidationResult validationResult = _validator.ValidateAll();

            await Task.WhenAll(
                RaisePropertyChanged(nameof(IsHotelNameInvalid)),
                RaisePropertyChanged(nameof(IsFullCompanyNameInvalid)),
                RaisePropertyChanged(nameof(IsUsreouInvalid)),
                RaisePropertyChanged(nameof(IsBankCardOrIbanInvalid)),
                RaisePropertyChanged(nameof(IsBankNameInvalid)),
                RaisePropertyChanged(nameof(IsCheckInTimeInvalid)),
                RaisePropertyChanged(nameof(IsCheckOutTimeInvalid)),
                RaisePropertyChanged(nameof(IsHotelDescriptionInvalid)),
                RaisePropertyChanged(nameof(IsWithinTerritoryDescriptionInvalid))
            );

            foreach (ValidatableEntryItemViewModel vm in PhoneNumbers)
            {
                vm.Validate();
            }

            foreach (ValidatableEntryItemViewModel vm in SocialResources)
            {
                vm.Validate();
            }

            if (!validationResult.IsValid)
            {
                _userDialogs.Toast(validationResult.ErrorList[0].ErrorText);

                return;
            }

            string IBAN = null;
            long? bankCard = null;

            if (Regex.IsMatch(BankCardOrIban, DataPatterns.IBAN))
            {
                IBAN = BankCardOrIban;
            }
            else
            {
                bankCard = long.Parse(BankCardOrIban);
            }

            ServerResponce responce = await _networkRequestExecutor.MakeRequestAsync(() =>
                _hotelsService.SaveChangesAsync(
                    _currentHotelId,
                    HotelName,
                    FullCompanyName,
                    Email,
                    BankName,
                    IBAN,
                    bankCard,
                    int.Parse(USREOU),
                    PhoneNumbers.Select(e => new PhoneDto { Id = e.Id, Number = e.Text }).ToArray(),
                    SocialResources.Select(e => new SocialResourceDto { Id = e.Id, Link = e.Text }).ToArray(),
                    CheckInTime.Value,
                    CheckOutTime.Value,
                    WithinTerritoryDescription,
                    HotelDescription,
                    _photoFileName
                ),
                new HashSet<string>()
            );

            if (responce == null)
                return;

            _memoryCache.Set(
                Constants.Keys.ShortHotelInfo,
                new ShortInfoDto { HotelName = HotelName, PhotoName = _photoFileName, ProfileNotCompleted = false }
            );

            await _navigationService.Close(this, new HotelDto
            {
                HotelName = HotelName,
                Email = Email,
                FullCompanyName = FullCompanyName,
                BankCard = bankCard,
                IBAN = IBAN,
                BankName = BankName,
                USREOU = int.Parse(USREOU),
                CheckInTime = CheckInTime,
                CheckOutTime = CheckOutTime,
                WithinTerritoryDescription = WithinTerritoryDescription,
                HotelDescription = HotelDescription,
                PhotoUrl = _photoFileName,
                Phones = PhoneNumbers.Select(p => new PhoneDto { Id = p.Id, Number = p.Text }).ToList(),
                SocialResources = SocialResources.Select(sr => new SocialResourceDto { Id = sr.Id, Link = sr.Text }).ToList()
            });
        }

        private Task<TimePromptResult> ShowTimePromptAsync(string titleText, TimeSpan? startTime)
        {
            return _userDialogs.TimePromptAsync(new TimePromptConfig
            {
                iOSPickerStyle = iOSPickerStyle.Wheels,
                Use24HourClock = true,
                Title = titleText,
                SelectedTime = startTime
            });
        }

        private bool CheckInvalid(string name)
        {
            return !_validator.Validate(name).IsValid;
        }
    }
}
