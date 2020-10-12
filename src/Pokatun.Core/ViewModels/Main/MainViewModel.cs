using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Navigation;

namespace Pokatun.Core.ViewModels.Main
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IMvxNavigationService navigationService)
        {
            RUn(navigationService);
        }

        private async void RUn(IMvxNavigationService navigationService)
        {
            await Task.Delay(3000);

            //await navigationService.Navigate<NewViewModel>();
        }
    }
}
