using System;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmValidation;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Collections;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Profile
{
    public sealed class EditHotelProfileViewModel : BaseViewModel<HotelDto, bool>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ValidationHelper _validator;

        private bool _viewInEditMode = true;
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
            set { SetProperty(ref _checkInTime, value); }
        }

        private TimeSpan? _checkOutTime;
        public TimeSpan? CheckOutTime
        {
            get { return _checkOutTime; }
            set { SetProperty(ref _checkOutTime, value); }
        }

        public MvxObservableCollection<EntryItemViewModel> PhoneNumbers { get; private set; }

        public MvxObservableCollection<EntryItemViewModel> SocialResources { get; private set; }

        public bool IsHotelNameInvalid => CheckInvalid(nameof(HotelName));

        public bool IsFullCompanyNameInvalid => CheckInvalid(nameof(FullCompanyName));

        public bool IsEmailInvalid => CheckInvalid(nameof(Email));

        public bool IsUsreouInvalid => CheckInvalid(nameof(USREOU));

        public bool IsBankNameInvalid => CheckInvalid(nameof(BankName));

        public bool IsBankCardOrIbanInvalid => CheckInvalid(nameof(BankCardOrIban));

        private MvxAsyncCommand _closeCommand;
        public IMvxAsyncCommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new MvxAsyncCommand(DoCloseCommandAsync));
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

        public EditHotelProfileViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(HotelName), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(HotelName), Strings.HotelNameRequiredMessage));
            _validator.AddRule(nameof(FullCompanyName), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(FullCompanyName), Strings.CompanyNameRequiredMessage));

            PhoneNumbers = new MvxObservableCollection<EntryItemViewModel>();
            SocialResources = new MvxObservableCollection<EntryItemViewModel>();
        }

        public override void Prepare(HotelDto parameter)
        {
            HotelName = parameter.HotelName;
            FullCompanyName = parameter.FullCompanyName;
            Email = parameter.Email;
            BankCardOrIban = parameter.BankCard == null ? parameter.IBAN : parameter.BankCard.ToString();
            BankName = parameter.BankName;
            USREOU = parameter.USREOU.ToString();

            PhoneNumbers.AddRange(parameter.Phones.Select(p => new EntryItemViewModel(p.Number, DeletePhoneCommand)));
            SocialResources.AddRange(parameter.SocialResources.Select(sr => new EntryItemViewModel(sr.Link, RemoveSocialResourceCommand)));
        }

        private void DoAddPhoneCommand()
        {
            PhoneNumbers.Add(new EntryItemViewModel(null, DeletePhoneCommand));
        }

        private void DoDeletePhoneCommand(EntryItemViewModel phoneVM)
        {
            PhoneNumbers.Remove(phoneVM);
        }

        private void DoAddSocialResourceCommand()
        {
            SocialResources.Add(new EntryItemViewModel(null, RemoveSocialResourceCommand));
        }

        private void DoRemoveSocialResourceCommand(EntryItemViewModel srVM)
        {
            SocialResources.Remove(srVM);
        }

        private async Task DoChooseCheckInTimeCommandAsync()
        {
            TimePromptResult res = await ShowTimePromptAsync();

            if (!res.Ok) return;

            CheckInTime = res.Value;
        }

        private async Task DoChooseCheckOutTimeCommandAsync()
        {
            TimePromptResult res = await ShowTimePromptAsync();

            if (!res.Ok) return;

            CheckOutTime = res.Value;
        }


        private Task DoCloseCommandAsync()
        {
            return _navigationService.Close(this, false);
        }

        private Task<TimePromptResult> ShowTimePromptAsync()
        {
            return _userDialogs.TimePromptAsync(new TimePromptConfig { Use24HourClock = true });
        }

        private bool CheckInvalid(string name)
        {
            return !_validator.Validate(name).IsValid;
        }
    }
}
