using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmValidation;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.ForgotPassword
{
    public sealed class СheckVerificationCodeViewModel : BaseViewModel
    {
        private readonly ValidationHelper _validator;
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IHotelsService _hotelsService;

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

        public СheckVerificationCodeViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, IHotelsService hotelsService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _hotelsService = hotelsService;

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
            ServerResponce responce = null;

            using (_userDialogs.Loading(Strings.ProcessingRequest))
            {
                responce = await _hotelsService.ValidateResetToken(VerificationCode);

                if (responce.Success)
                {
                    await _navigationService.Navigate<NewPasswordViewModel, string>(VerificationCode);

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
