using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmValidation;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Core.ViewModels.Menu;
using Pokatun.Data;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.ViewModels.ForgotPassword
{
    public sealed class NewPasswordViewModel : BaseViewModel<string>
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IHotelsService _hotelsService;
        private readonly ISecureStorage _secureStorage;
        private readonly IMvxNavigationService _navigationService;

        private readonly ValidationHelper _validator;

        private bool _viewInEditMode = true;

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
        private string _token;

        public IMvxAsyncCommand SavePasswordCommand
        {
            get
            {
                return _savePasswordCommand ?? (_savePasswordCommand = new MvxAsyncCommand(DoSavePasswordCommandAsync));
            }
        }

        public NewPasswordViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, IHotelsService hotelsService, ISecureStorage secureStorage)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _hotelsService = hotelsService;
            _secureStorage = secureStorage;

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

            ServerResponce<TokenInfoDto> responce = null;

            using (_userDialogs.Loading(Strings.ProcessingRequest))
            {
                responce = await _hotelsService.ResetPassword(_token, Password);

                if (responce.Success)
                {
                    await _secureStorage.SetAsync(Constants.Keys.AccountId, responce.Data.AccountId.ToString(CultureInfo.InvariantCulture));
                    await _secureStorage.SetAsync(Constants.Keys.Token, responce.Data.Token);
                    await _secureStorage.SetAsync(
                        Constants.Keys.TokenExpirationTime,
                        responce.Data.ExpirationTime.ToUniversalTime().ToString(CultureInfo.InvariantCulture)
                    );

                    await _navigationService.Close(this);
                    await _navigationService.Navigate<HotelMenuViewModel>();

                    return;
                }
            }

            ISet<string> knownErrorCodes = new HashSet<string>
            {
                ErrorCodes.InvalidTokenError,
                ErrorCodes.ExpiredTokenError
            };

            knownErrorCodes.IntersectWith(responce.ErrorCodes);

            if (knownErrorCodes.Count > 0)
            {
                _userDialogs.Toast(Strings.ResourceManager.GetString(knownErrorCodes.First()));
                return;
            }

            _userDialogs.Toast(Strings.UnexpectedError);
        }
    }
}
