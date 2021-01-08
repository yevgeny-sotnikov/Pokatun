using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmValidation;
using Pokatun.Core.Executors;
using Pokatun.Core.Models.Enums;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Core.ViewModels.ForgotPassword;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel<UserRole>
    {
        private readonly ValidationHelper _validator;
        private readonly IAuthExecutor _authExecutor;
        private readonly IUserDialogs _userDialogs;
        private readonly IMvxNavigationService _navigationService;
        private readonly IHotelsService _hotelsService;

        private bool _viewInEditMode = true;
        private UserRole _role;

        public override string Title => Strings.EntranceToAccount;

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

        private string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set
            {
                if (!SetProperty(ref _password, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsPasswordInvalid));
            }
        }

        public bool IsPasswordInvalid => CheckInvalid(nameof(Password));

        public bool IsEmailInvalid => CheckInvalid(nameof(Email));

        private MvxAsyncCommand _loginCommand;
        public IMvxCommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new MvxAsyncCommand(DoLoginCommandAsync));
            }
        }

        private MvxAsyncCommand _forgotPasswordCommand;
        public IMvxCommand ForgotPasswordCommand
        {
            get
            {
                return _forgotPasswordCommand ?? (_forgotPasswordCommand = new MvxAsyncCommand(DoForgotPasswordCommandAsync));
            }
        }

        public LoginViewModel(
            IAuthExecutor authExecutor,
            IUserDialogs userDialogs,
            IMvxNavigationService navigationService,
            IHotelsService hotelsService
        ) {
            _authExecutor = authExecutor;
            _userDialogs = userDialogs;
            _navigationService = navigationService;
            _hotelsService = hotelsService;

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(Email), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(Email.Trim(), DataPatterns.Email), Strings.InvalidEmailMessage));
            _validator.AddRule(nameof(Password), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(Password.Trim(), DataPatterns.Password), Strings.InvalidPasswordMessage));
        }

        public override void Prepare(UserRole role)
        {
            _role = role;
        }

        private bool CheckInvalid(string name)
        {
            return !_validator.Validate(name).IsValid;
        }

        private async Task DoLoginCommandAsync()
        {
            _viewInEditMode = false;

            ValidationResult validationResult = _validator.ValidateAll();

            await Task.WhenAll(
                RaisePropertyChanged(nameof(IsEmailInvalid)),
                RaisePropertyChanged(nameof(IsPasswordInvalid))
            );

            if (!validationResult.IsValid)
            {
                _userDialogs.Toast(validationResult.ErrorList[0].ErrorText);

                return;
            }

            await _authExecutor.MakeAuthAsync(
                () => _hotelsService.LoginAsync(Email, Password),
                new HashSet<string> { ErrorCodes.AccountDoesNotExistError, ErrorCodes.IncorrectPasswordError },
                this
            );
        }

        private Task DoForgotPasswordCommandAsync()
        {
            return _navigationService.Navigate<RequestVerificationCodeViewModel>();
        }
    }
}
