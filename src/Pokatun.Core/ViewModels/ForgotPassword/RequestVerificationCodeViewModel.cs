using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Pokatun.Core.ViewModels.ForgotPassword
{
    public sealed class RequestVerificationCodeViewModel : BaseViewModel
    {
        private readonly ValidationHelper _validator;
        private readonly IMvxNavigationService _navigationService;
        private readonly IHotelsService _hotelsService;
        private readonly INetworkRequestExecutor _networkRequestExecutor;

        public override string Title => Strings.PasswordRecovery;

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

        public RequestVerificationCodeViewModel(IMvxNavigationService navigationService, IHotelsService hotelsService, INetworkRequestExecutor networkRequestExecutor)
        {
            _navigationService = navigationService;
            _hotelsService = hotelsService;
            _networkRequestExecutor = networkRequestExecutor;

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(Email), () => RuleResult.Assert(Regex.IsMatch(Email.Trim(), DataPatterns.Email), "ok"));
        }

        private async Task DoRequestCodeCommandAsync()
        {
            ServerResponce responce = await _networkRequestExecutor.MakeRequestAsync(
                () => _hotelsService.ForgotPasswordAsync(Email),
                new HashSet<string>
                {
                    ErrorCodes.AccountDoesNotExistError
                }
            );

            if (responce == null) return;

            await _navigationService.Navigate<СheckVerificationCodeViewModel>();
        }
    }
}
