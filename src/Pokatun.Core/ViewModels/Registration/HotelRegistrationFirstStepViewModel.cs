using System.Text.RegularExpressions;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmValidation;
using Pokatun.Core.Resources;

namespace Pokatun.Core.ViewModels.Registration
{
    public sealed class HotelRegistrationFirstStepViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;

        private bool _viewInEditMode = true;
        private ValidationHelper _validator;

        public override string Title => Strings.Registration;

        private string _hotelName = string.Empty;
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


        private string _phoneNumber = string.Empty;
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

        private string _email = string.Empty;
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

        private string _password = string.Empty;
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

        private string _passwordConfirm = string.Empty;
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

        private MvxCommand _furtherCommand;
        public IMvxCommand FurtherCommand
        {
            get
            {
                return _furtherCommand ?? (_furtherCommand = new MvxCommand(DoFurtherCommand));
            }
        }

        public HotelRegistrationFirstStepViewModel(IUserDialogs userDialogs)
        {
            _userDialogs = userDialogs;
            _validator = new ValidationHelper();

            _validator.AddRule(nameof(HotelName), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(HotelName), Strings.HotelNameRequiredMessage));
            _validator.AddRule(nameof(PhoneNumber), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(PhoneNumber.Trim(), Constants.PhonePattern), Strings.InvalidPhoneNumberMessage));
            _validator.AddRule(nameof(Email), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(Email.Trim(), Constants.EmailPattern), Strings.InvalidEmailMessage));
            _validator.AddRule(nameof(Password), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(Password.Trim(), Constants.PasswordPattern), Strings.InvalidPasswordMessage));
            _validator.AddRule(nameof(PasswordConfirm), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(PasswordConfirm.Trim(), Constants.PasswordPattern), Strings.InvalidPasswordMessage));
            _validator.AddRule(nameof(Password), nameof(PasswordConfirm), () => RuleResult.Assert(_viewInEditMode || Password == PasswordConfirm, Strings.PasswordMismatchMessage));
        }

        private bool CheckInvalid(string name)
        {
            return !_validator.Validate(name).IsValid;
        }

        private void DoFurtherCommand()
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
                return;
            }

            _userDialogs.Toast(validationResult.ErrorList[0].ErrorText);

        }
    }
}
