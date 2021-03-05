using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmValidation;
using Pokatun.Core.Executors;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Registration
{
    public sealed class TouristRegistrationViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IAuthExecutor _authExecutor;
        private readonly IAccountsService _accountsService;
        private readonly ITouristFinalSetupExecutor _touristFinalSetupExecutor;
        private readonly ValidationHelper _validator;

        private bool _viewInEditMode = true;

        private string _fullname;
        public string FullName
        {
            get { return _fullname; }
            set
            {
                if (!SetProperty(ref _fullname, value))
                    return;

                _viewInEditMode = true;

                RaisePropertyChanged(nameof(IsFullNameInvalid));
            }
        }

        private string _phoneNumber = string.Empty;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (!SetProperty(ref _phoneNumber, value))
                    return;

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

        public bool IsFullNameInvalid => CheckInvalid(nameof(FullName));

        private MvxAsyncCommand _createAccountCommand;
        public IMvxAsyncCommand СreateAccountCommand
        {
            get
            {
                return _createAccountCommand ?? (_createAccountCommand = new MvxAsyncCommand(DoCreateAccountCommandAsync));
            }
        }

        public TouristRegistrationViewModel(
            IUserDialogs userDialogs,
            IAuthExecutor authExecutor,
            IAccountsService accountsService,
            ITouristFinalSetupExecutor touristFinalSetupExecutor)
        {
            _userDialogs = userDialogs;
            _authExecutor = authExecutor;
            _accountsService = accountsService;
            _touristFinalSetupExecutor = touristFinalSetupExecutor;

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(FullName), () => RuleResult.Assert(_viewInEditMode || !string.IsNullOrWhiteSpace(FullName), Strings.FullnameRequiredMessage));
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

        private async Task DoCreateAccountCommandAsync()
        {
            _viewInEditMode = false;

            ValidationResult validationResult = _validator.ValidateAll();

            await Task.WhenAll(
                RaisePropertyChanged(nameof(IsFullNameInvalid)),
                RaisePropertyChanged(nameof(IsPhoneNumberInvalid)),
                RaisePropertyChanged(nameof(IsEmailInvalid)),
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
                    () => _accountsService.RegisterTouristAsync(
                        FullName,
                        Email,
                        Password,
                        PhoneNumber
                    ),
                    new HashSet<string> { ErrorCodes.AccountAllreadyExistsError }
                );

                if (dto == null)
                    return;

                await _touristFinalSetupExecutor.FinalizeSetupAsync(dto, this);
            }
        }
    }
}
