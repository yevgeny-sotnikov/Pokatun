using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmValidation;
using Pokatun.Core.Executors;
using Pokatun.Core.Models;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Core.ViewModels.Menu;
using Pokatun.Data;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.ViewModels.Registration
{
    public sealed class HotelRegistrationSecondStepViewModel : BaseViewModel<HotelRegistrationFirstData>
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IMvxNavigationService _navigationService;
        private readonly ValidationHelper _validator;
        private readonly IHotelsService _hotelsService;
        private readonly INetworkRequestExecutor _networkRequestExecutor;
        private readonly ISecureStorage _secureStorage;
        private bool _viewInEditMode = true;

        private HotelRegistrationFirstData _firstData;

        public override string Title => Strings.Registration;

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

        public bool IsUsreouInvalid => CheckInvalid(nameof(USREOU));

        public bool IsBankNameInvalid => CheckInvalid(nameof(BankName));

        public bool IsBankCardOrIbanInvalid => CheckInvalid(nameof(BankCardOrIban));

        public bool IsFullCompanyNameInvalid => CheckInvalid(nameof(FullCompanyName));

        private MvxAsyncCommand _createAccountCommand;

        public IMvxAsyncCommand СreateAccountCommand
        {
            get
            {
                return _createAccountCommand ?? (_createAccountCommand = new MvxAsyncCommand(DoСreateAccountCommandAsync));
            }
        }

        public override void Prepare(HotelRegistrationFirstData parameter)
        {
            _firstData = parameter;
        }

        public HotelRegistrationSecondStepViewModel(
            IUserDialogs userDialogs,
            IMvxNavigationService navigationService,
            IHotelsService hotelsService,
            INetworkRequestExecutor networkRequestExecutor,
            ISecureStorage secureStorage)
        {
            _userDialogs = userDialogs;
            _navigationService = navigationService;
            _hotelsService = hotelsService;
            _networkRequestExecutor = networkRequestExecutor;
            _secureStorage = secureStorage;

            _validator = new ValidationHelper();

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
        }

        private async Task DoСreateAccountCommandAsync()
        {
            _viewInEditMode = false;

            ValidationResult validationResult = _validator.ValidateAll();

            await Task.WhenAll(
                RaisePropertyChanged(nameof(IsUsreouInvalid)),
                RaisePropertyChanged(nameof(IsBankCardOrIbanInvalid)),
                RaisePropertyChanged(nameof(IsFullCompanyNameInvalid)),
                RaisePropertyChanged(nameof(IsBankNameInvalid))
            );

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

            ServerResponce<TokenInfoDto> responce = await _networkRequestExecutor.MakeRequestAsync(
                () => _hotelsService.RegisterAsync(
                    _firstData.HotelName,
                    FullCompanyName,
                    _firstData.Email,
                    _firstData.PhoneNumber,
                    BankName, IBAN,
                    bankCard,
                    int.Parse(USREOU)
                ),
                new HashSet<string>
                {
                    ErrorCodes.AccountAllreadyExistsError,
                    ErrorCodes.IbanAllreadyRegisteredError,
                    ErrorCodes.UsreouAllreadyRegisteredError
                }
            );

            if (responce == null) return;

            await _secureStorage.SetAsync(Constants.Keys.AccountId, responce.Data.AccountId.ToString(CultureInfo.InvariantCulture));
            await _secureStorage.SetAsync(Constants.Keys.Token, responce.Data.Token);
            await _secureStorage.SetAsync(
                Constants.Keys.TokenExpirationTime,
                responce.Data.ExpirationTime.ToUniversalTime().ToString(CultureInfo.InvariantCulture)
            );

            await _navigationService.Close(this);
            await _navigationService.Navigate<HotelMenuViewModel>();
        }

        private bool CheckInvalid(string name)
        {
            return !_validator.Validate(name).IsValid;
        }
    }
}
