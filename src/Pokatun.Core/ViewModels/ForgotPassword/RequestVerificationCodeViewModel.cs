using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Pokatun.Core.ViewModels.ForgotPassword
{
    public sealed class RequestVerificationCodeViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private MvxAsyncCommand _requestCodeCommand;
        public IMvxAsyncCommand RequestCodeCommand
        {
            get { return _requestCodeCommand ?? (_requestCodeCommand = new MvxAsyncCommand(DoRequestCodeCommandAsync)); }
        }

        public RequestVerificationCodeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private Task DoRequestCodeCommandAsync()
        {
            return _navigationService.Navigate<СheckVerificationCodeViewModel>();
        }
    }
}
