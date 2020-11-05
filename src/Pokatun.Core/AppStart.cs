using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Pokatun.Core.ViewModels.ChoiseUserRole;

namespace Pokatun.Core
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication app, IMvxNavigationService navigationService) : base(app, navigationService)
        {

        }

        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            await NavigationService.Navigate<ChoiseUserRoleViewModel>();
        }
    }
}