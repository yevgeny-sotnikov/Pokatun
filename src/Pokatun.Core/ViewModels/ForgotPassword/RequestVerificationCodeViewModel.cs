using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    public sealed class RequestVerificationCodeViewModel : BaseViewModel
    {
        private readonly ValidationHelper _validator;
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IHotelsService _hotelsService;

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

        public RequestVerificationCodeViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, IHotelsService hotelsService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _hotelsService = hotelsService;

            _validator = new ValidationHelper();

            _validator.AddRule(nameof(Email), () => RuleResult.Assert(Regex.IsMatch(Email.Trim(), DataPatterns.Email), "ok"));
        }

        private async Task DoRequestCodeCommandAsync()
        {
            ServerResponce responce = null;

            using (_userDialogs.Loading(Strings.ProcessingRequest))
            {
                responce = await _hotelsService.ForgotPasswordAsync(Email);

                if (responce.Success)
                {
                    await _navigationService.Navigate<СheckVerificationCodeViewModel>();

                    return;
                }
            }

            ISet<string> knownErrorCodes = new HashSet<string>
            {
                ErrorCodes.AccountDoesNotExistError
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
