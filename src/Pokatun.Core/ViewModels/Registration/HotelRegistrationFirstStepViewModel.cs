using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmValidation;
using Pokatun.Core.Models;
using Pokatun.Core.Resources;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Registration
{
    public sealed class HotelRegistrationFirstStepViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IMvxNavigationService _navigationService;
        private readonly ValidationHelper _validator;

        private bool _viewInEditMode = true;

        public override string Title => Strings.Registration;

        private string _hotelName = "FFSDFD";
        public string HotelName
        {
            get { return _hotelName; }
            set
            {
                if (!SetProperty(ref _hotelName, value)) return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsHotelNameInvalid));
            }
        }


        private string _phoneNumber = "+380660303007";
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (!SetProperty(ref _phoneNumber, value)) return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsPhoneNumberInvalid));
            }
        }

        private string _email = "d@dd.com";
        public string Email
        {
            get { return _email; }
            set
            {
                if (!SetProperty(ref _email, value)) return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsEmailInvalid));
            }
        }

        private string _password = "Zxcvbn123!";
        public string Password
        {
            get { return _password; }
            set
            {
                if (!SetProperty(ref _password, value)) return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsPasswordInvalid));
            }
        }

        private string _passwordConfirm = "Zxcvbn123!";
        public string PasswordConfirm
        {
            get { return _passwordConfirm; }
            set
            {
                if (!SetProperty(ref _passwordConfirm, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsPasswordConfirmInvalid));
            }
        }

        public bool IsPasswordConfirmInvalid => CheckInvalid(nameof(PasswordConfirm));

        public bool IsPasswordInvalid => CheckInvalid(nameof(Password));

        public bool IsEmailInvalid => CheckInvalid(nameof(Email));

        public bool IsPhoneNumberInvalid => CheckInvalid(nameof(PhoneNumber));

        public bool IsHotelNameInvalid => CheckInvalid(nameof(HotelName));

        private MvxAsyncCommand _furtherCommand;
        public IMvxCommand FurtherCommand
        {
            get
            {
                return _furtherCommand ?? (_furtherCommand = new MvxAsyncCommand(DoFurtherCommandAsync));
            }
        }

        public HotelRegistrationFirstStepViewModel(IUserDialogs userDialogs, IMvxNavigationService navigationService)
        {
            _userDialogs = userDialogs;
            _navigationService = navigationService;

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(HotelName), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(HotelName), Strings.HotelNameRequiredMessage));
            _validator.AddRule(nameof(PhoneNumber), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(PhoneNumber.Trim(), DataPatterns.Phone), Strings.InvalidPhoneNumberMessage));
            _validator.AddRule(nameof(Email), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(Email.Trim(), DataPatterns.Email), Strings.InvalidEmailMessage));
            _validator.AddRule(nameof(Password), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(Password.Trim(), DataPatterns.Password), Strings.InvalidPasswordMessage));
            _validator.AddRule(nameof(PasswordConfirm), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(PasswordConfirm.Trim(), DataPatterns.Password), Strings.InvalidPasswordMessage));
            _validator.AddRule(nameof(Password), nameof(PasswordConfirm), () => RuleResult.Assert(_viewInEditMode || Password == PasswordConfirm, Strings.PasswordMismatchMessage));
        }

        private bool CheckInvalid(string name)
        {
            return !_validator.Validate(name).IsValid;
        }

        private async Task DoFurtherCommandAsync()
        {
            _viewInEditMode = false;

            ValidationResult validationResult = _validator.ValidateAll();

            RaisePropertyChanged(nameof(IsHotelNameInvalid));
            RaisePropertyChanged(nameof(IsPhoneNumberInvalid));
            RaisePropertyChanged(nameof(IsEmailInvalid));
            RaisePropertyChanged(nameof(IsPasswordInvalid));
            RaisePropertyChanged(nameof(IsPasswordConfirmInvalid));

            if (validationResult.IsValid)
            {
                HotelRegistrationFirstData data = new HotelRegistrationFirstData
                {
                    HotelName = HotelName,
                    PhoneNumber = PhoneNumber,
                    Email = Email,
                    Password = Password
                };

                await _navigationService.Navigate<HotelRegistrationSecondStepViewModel, HotelRegistrationFirstData>(data);
                return;
            }

            _userDialogs.Toast(validationResult.ErrorList[0].ErrorText);
        }
    }
}
