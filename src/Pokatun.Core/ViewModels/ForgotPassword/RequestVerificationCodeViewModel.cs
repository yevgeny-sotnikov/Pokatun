using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmValidation;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.ForgotPassword
{
    public sealed class RequestVerificationCodeViewModel : BaseViewModel
    {
        private readonly ValidationHelper _validator;
        private readonly IMvxNavigationService _navigationService;

        private string _email = string.Empty;
        public string Email
        {
            get { return _email; }
            set
            {
                if (!SetProperty(ref _email, value))
                    return;

                RaisePropertyChanged(nameof(IsRequestCodeButtonEnabled));
            }
        }

        public bool IsRequestCodeButtonEnabled => _validator.ValidateAll().IsValid;

        private MvxAsyncCommand _requestCodeCommand;
        public IMvxAsyncCommand RequestCodeCommand
        {
            get { return _requestCodeCommand ?? (_requestCodeCommand = new MvxAsyncCommand(DoRequestCodeCommandAsync)); }
        }

        public RequestVerificationCodeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(Email), () => RuleResult.Assert(Regex.IsMatch(Email.Trim(), DataPatterns.Email), "ok"));
        }

        private Task DoRequestCodeCommandAsync()
        {
            return _navigationService.Navigate<СheckVerificationCodeViewModel>();
        }
    }
}
