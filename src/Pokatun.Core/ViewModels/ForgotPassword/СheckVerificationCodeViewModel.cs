using System.Collections.Generic;
using System.Linq;
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
    public sealed class СheckVerificationCodeViewModel : BaseViewModel
    {
        private readonly ValidationHelper _validator;
        private readonly IMvxNavigationService _navigationService;
        private readonly IHotelsService _hotelsService;
        private readonly INetworkRequestExecutor _networkRequestExecutor;

        public override string Title => Strings.PasswordRecovery;

        private string _verificationCode = string.Empty;
        public string VerificationCode
        {
            get { return _verificationCode; }
            set
            {
                if (!SetProperty(ref _verificationCode, value))
                    return;

                RaisePropertyChanged(nameof(IsMatchButtonEnabled));
            }
        }

        public bool IsMatchButtonEnabled => _validator.ValidateAll().IsValid;

        private MvxAsyncCommand _matchCodeCommand;
        public IMvxAsyncCommand MatchCodeCommand
        {
            get { return _matchCodeCommand ?? (_matchCodeCommand = new MvxAsyncCommand(DoMatchCodeCommandAsync)); }
        }

        public СheckVerificationCodeViewModel(IMvxNavigationService navigationService, IHotelsService hotelsService, INetworkRequestExecutor networkRequestExecutor)
        {
            _navigationService = navigationService;
            _hotelsService = hotelsService;
            _networkRequestExecutor = networkRequestExecutor;

            _validator = new ValidationHelper();

            _validator.AddRule(
                nameof(VerificationCode),
                () => RuleResult.Assert(
                    VerificationCode.Length == DataPatterns.VerificationCodeLenght,
                    "ok"
                )
            );
        }

        private async Task DoMatchCodeCommandAsync()
        {
            ServerResponce responce = await _networkRequestExecutor.MakeRequestAsync(
                () => _hotelsService.ValidateResetToken(VerificationCode),
                new HashSet<string>
                {
                    ErrorCodes.InvalidTokenError,
                    ErrorCodes.ExpiredTokenError
                }
            );

            if (responce == null) return;

            await _navigationService.Navigate<NewPasswordViewModel, string>(VerificationCode);
        }
    }
}
