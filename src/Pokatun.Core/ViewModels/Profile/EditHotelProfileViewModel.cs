using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
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
    public sealed class EditHotelProfileViewModel : BaseViewModel<HotelDto, bool>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IMediaPicker _mediaPicker;
        private readonly IHotelsService _hotelsService;
        private readonly IPhotosService _photosService;
        private readonly INetworkRequestExecutor _networkRequestExecutor;
        private readonly ValidationHelper _validator;

        private bool _viewInEditMode = true;
        private long _currentHotelId;
        private string _photoFileName;

        private string _title;
        public override string Title => _title;

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

        public MvxObservableCollection<EntryItemViewModel> PhoneNumbers { get; private set; }

        public MvxObservableCollection<EntryItemViewModel> SocialResources { get; private set; }

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

        private MvxCommand<EntryItemViewModel> _deletePhoneCommand;
        public IMvxCommand<EntryItemViewModel> DeletePhoneCommand
        {
            get
            {
                return _deletePhoneCommand ?? (_deletePhoneCommand = new MvxCommand<EntryItemViewModel>(DoDeletePhoneCommand));
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

        private MvxCommand<EntryItemViewModel> _removeSocialResourceCommand;
        public IMvxCommand<EntryItemViewModel> RemoveSocialResourceCommand
        {
            get
            {
                return _removeSocialResourceCommand ?? (_removeSocialResourceCommand = new MvxCommand<EntryItemViewModel>(DoRemoveSocialResourceCommand));
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
            INetworkRequestExecutor networkRequestExecutor)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _mediaPicker = mediaPicker;
            _hotelsService = hotelsService;
            _photosService = photosService;
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
            _validator.AddRule(nameof(PhoneNumbers), () => RuleResult.Assert(_viewInEditMode || !DuplicatedPhones.Any(), Strings.PhoneNumbersDuplication));
            _validator.AddRule(nameof(SocialResources), () => RuleResult.Assert(_viewInEditMode || !DuplicatedSocialResources.Any(), Strings.SocialResourcesDuplication));

            PhoneNumbers = new MvxObservableCollection<EntryItemViewModel>();
            SocialResources = new MvxObservableCollection<EntryItemViewModel>();
        }

        public override void Prepare(HotelDto parameter)
        {
            _currentHotelId = parameter.Id;
            _title = parameter.HotelName;
            RaisePropertyChanged(nameof(Title));

            _photoFileName = parameter.PhotoUrl;
            PhotoStream = ct => _photosService.GetAsync(parameter.PhotoUrl);

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
            
            PhoneNumbers.AddRange(parameter.Phones.Select(p => new EntryItemViewModel(p.Id, p.Number, DeletePhoneCommand, IsPhoneDuplicated)));
            SocialResources.AddRange(parameter.SocialResources.Select(sr => new EntryItemViewModel(sr.Id, sr.Link, RemoveSocialResourceCommand, IsLinkDuplicated)));
        }

        private bool IsLinkDuplicated(string str)
        {
            return DuplicatedSocialResources.Contains(str);
        }

        private bool IsPhoneDuplicated(string str)
        {
            return DuplicatedPhones.Contains(str);
        }

        private string[] DuplicatedPhones => GetDuplications(PhoneNumbers);

        private string[] DuplicatedSocialResources => GetDuplications(SocialResources);

        private string[] GetDuplications(IEnumerable<EntryItemViewModel> collection)
        {
            return collection.GroupBy(x => x.Text).Where(g => g.Count() > 1).Select(g => g.Key).ToArray();
        }

        private void DoAddPhoneCommand()
        {
            PhoneNumbers.Add(new EntryItemViewModel(0, null, DeletePhoneCommand, IsPhoneDuplicated));
        }

        private void DoDeletePhoneCommand(EntryItemViewModel phoneVM)
        {
            PhoneNumbers.Remove(phoneVM);
        }

        private void DoAddSocialResourceCommand()
        {
            SocialResources.Add(new EntryItemViewModel(0, null, RemoveSocialResourceCommand, IsLinkDuplicated));
        }

        private void DoRemoveSocialResourceCommand(EntryItemViewModel srVM)
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
            return _navigationService.Close(this, false);
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

            foreach (EntryItemViewModel vm in PhoneNumbers)
            {
                vm.Validate();
            }

            foreach (EntryItemViewModel vm in SocialResources)
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

            await _navigationService.Close(this);
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
