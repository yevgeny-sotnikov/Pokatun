using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmValidation;
using Pokatun.Core.Executors;
using Pokatun.Core.Models;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Registration
{
    public sealed class HotelRegistrationSecondStepViewModel : BaseViewModel<HotelRegistrationFirstData>
    {
        private readonly IAuthExecutor _authExecutor;
        private readonly IUserDialogs _userDialogs;
        private readonly ValidationHelper _validator;
        private readonly IAccountsService _accountsService;
        private readonly IHotelFinalSetupExecutor _hotelFinalSetupExecutor;

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

        public HotelRegistrationSecondStepViewModel(IAuthExecutor authExecutor, IUserDialogs userDialogs, IAccountsService accountsService, IHotelFinalSetupExecutor hotelFinalSetupExecutor)
        {
            _authExecutor = authExecutor;
            _userDialogs = userDialogs;
            _accountsService = accountsService;
            _hotelFinalSetupExecutor = hotelFinalSetupExecutor;

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

        public override void Prepare(HotelRegistrationFirstData parameter)
        {
            _firstData = parameter;
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

            using (_userDialogs.Loading(Strings.ProcessingRequest))
            {
                TokenInfoDto dto = await _authExecutor.MakeAuthAsync(
                    () => _accountsService.RegisterHotelAsync(
                        _firstData.HotelName,
                        FullCompanyName,
                        _firstData.Email,
                        _firstData.Password,
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

                if (dto == null)
                    return;

                await _hotelFinalSetupExecutor.FinalizeSetupAsync(dto, this, false);
            }
        }

        private bool CheckInvalid(string name)
        {
            return !_validator.Validate(name).IsValid;
        }
    }
}
