using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmValidation;
using Pokatun.Core.Models.Enums;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Core.ViewModels.Menu;
using Pokatun.Data;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel<UserRole>
    {
        private readonly ValidationHelper _validator;
        private readonly IUserDialogs _userDialogs;
        private readonly IMvxNavigationService _navigationService;
        private readonly IHotelsService _hotelsService;
        private readonly ISecureStorage _secureStorage;

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

        public LoginViewModel(IUserDialogs userDialogs, IMvxNavigationService navigationService, IHotelsService hotelsService, ISecureStorage secureStorage)
        {
            _userDialogs = userDialogs;
            _navigationService = navigationService;
            _hotelsService = hotelsService;
            _secureStorage = secureStorage;

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

            ServerResponce<TokenInfoDto> responce = null;

            using (_userDialogs.Loading(Strings.ProcessingRequest))
            {

                responce = await _hotelsService.LoginAsync(Email, Password);

                if (responce.Success)
                {
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

            ISet<string> knownErrorKodes = new HashSet<string>
            {
                ErrorCodes.AccountDoesNotExistError,
                ErrorCodes.IncorrectPasswordError
            };

            knownErrorKodes.IntersectWith(responce.ErrorCodes);

            if (knownErrorKodes.Count > 0)
            {
                _userDialogs.Toast(Strings.ResourceManager.GetString(knownErrorKodes.First()));
                return;
            }

            _userDialogs.Toast(Strings.UnexpectedError);
        }
    }
}
