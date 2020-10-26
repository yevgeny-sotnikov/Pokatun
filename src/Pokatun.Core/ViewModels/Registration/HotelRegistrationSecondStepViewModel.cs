using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmValidation;
using Pokatun.Core.Models;
using Pokatun.Core.Resources;

namespace Pokatun.Core.ViewModels.Registration
{
    public sealed class HotelRegistrationSecondStepViewModel : BaseViewModel<HotelRegistrationFirstData>
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IMvxNavigationService _navigationService;
        private ValidationHelper _validator;

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

        public IMvxCommand СreateAccountCommand
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

        public HotelRegistrationSecondStepViewModel(IUserDialogs userDialogs, IMvxNavigationService navigationService)
        {
            _userDialogs = userDialogs;
            _navigationService = navigationService;

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(FullCompanyName), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(FullCompanyName), Strings.CompanyNameRequiredMessage));

            _validator.AddRule(
                nameof(BankCardOrIban),
                () => RuleResult.Assert(
                    _viewInEditMode
                    || Regex.IsMatch(BankCardOrIban.Trim(), Constants.BankCardPattern)
                    || Regex.IsMatch(BankCardOrIban.Trim(), Constants.IbanPattern
                ),
                Strings.ValidBankCardOrIbanNotDefined)
            );

            _validator.AddRule(nameof(BankName), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(BankName), Strings.BankNameRequiredMessage));
            _validator.AddRule(nameof(USREOU), () => RuleResult.Assert(_viewInEditMode || Regex.IsMatch(USREOU.Trim(), Constants.UsreouPattern), Strings.InvalidUSREOU));
        }

        private Task DoСreateAccountCommandAsync()
        {
            _viewInEditMode = false;

            ValidationResult validationResult = _validator.ValidateAll();

            RaisePropertyChanged(nameof(IsUsreouInvalid));
            RaisePropertyChanged(nameof(IsBankCardOrIbanInvalid));
            RaisePropertyChanged(nameof(IsFullCompanyNameInvalid));
            RaisePropertyChanged(nameof(IsBankNameInvalid));

            if (validationResult.IsValid)
            {
                return Task.CompletedTask;
            }

            _userDialogs.Toast(validationResult.ErrorList[0].ErrorText);

            return Task.CompletedTask;
        }

        private bool CheckInvalid(string name)
        {
            return !_validator.Validate(name).IsValid;
        }
    }
}
