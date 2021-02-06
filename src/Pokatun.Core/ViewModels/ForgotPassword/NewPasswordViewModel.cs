using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmValidation;
using Pokatun.Core.Executors;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.ForgotPassword
{
    public sealed class NewPasswordViewModel : BaseViewModel<string>
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IAccountsService _accountService;
        private readonly ITouristFinalSetupExecutor _touristFinalSetupExecutor;
        private readonly IHotelFinalSetupExecutor _hotelFinalSetupExecutor;
        private readonly IAuthExecutor _authExecutor;

        private readonly ValidationHelper _validator;

        private bool _viewInEditMode = true;
        private string _token;

        public override string Title => Strings.NewPassword;

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

        private MvxAsyncCommand _savePasswordCommand;
        public IMvxAsyncCommand SavePasswordCommand
        {
            get
            {
                return _savePasswordCommand ?? (_savePasswordCommand = new MvxAsyncCommand(DoSavePasswordCommandAsync));
            }
        }

        public NewPasswordViewModel(
            IAuthExecutor authExecutor,
            IUserDialogs userDialogs,
            IAccountsService accountService,
            ITouristFinalSetupExecutor touristFinalSetupExecutor,
            IHotelFinalSetupExecutor hotelFinalSetupExecutor
        )
        {
            _authExecutor = authExecutor;
            _userDialogs = userDialogs;
            _accountService = accountService;
            _touristFinalSetupExecutor = touristFinalSetupExecutor;
            _hotelFinalSetupExecutor = hotelFinalSetupExecutor;

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(Password), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(Password.Trim(), DataPatterns.Password), Strings.InvalidPasswordMessage));
            _validator.AddRule(nameof(PasswordConfirm), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(PasswordConfirm.Trim(), DataPatterns.Password), Strings.InvalidPasswordMessage));
            _validator.AddRule(nameof(Password), nameof(PasswordConfirm), () => RuleResult.Assert(_viewInEditMode || Password == PasswordConfirm, Strings.PasswordMismatchMessage));
        }

        public override void Prepare(string parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(parameter));
            }

            _token = parameter;
        }


        private bool CheckInvalid(string name)
        {
            return !_validator.Validate(name).IsValid;
        }

        private async Task DoSavePasswordCommandAsync()
        {
            _viewInEditMode = false;

            ValidationResult validationResult = _validator.ValidateAll();

            await Task.WhenAll(
                RaisePropertyChanged(nameof(IsPasswordInvalid)),
                RaisePropertyChanged(nameof(IsPasswordConfirmInvalid))
            );

            if (!validationResult.IsValid)
            {
                _userDialogs.Toast(validationResult.ErrorList[0].ErrorText);
                return;
            }

            using (_userDialogs.Loading(Strings.ProcessingRequest))
            {
                TokenInfoDto dto = await _authExecutor.MakeAuthAsync(
                    () => _accountService.ResetPassword(_token, Password),
                    new HashSet<string> { ErrorCodes.InvalidTokenError, ErrorCodes.ExpiredTokenError }
                );

                if (dto == null)
                    return;

                if (dto.Role == UserRole.Tourist)
                {
                    await _touristFinalSetupExecutor.FinalizeSetupAsync(dto, this);
                }
                else
                {
                    await _hotelFinalSetupExecutor.FinalizeSetupAsync(dto, this);
                }
            }
        }
    }
}
