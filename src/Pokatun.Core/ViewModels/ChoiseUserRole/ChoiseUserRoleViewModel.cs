using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Pokatun.Core.Models.Enums;
using Pokatun.Core.ViewModels.PreEntrance;

namespace Pokatun.Core.ViewModels.ChoiseUserRole
{
    public sealed class ChoiseUserRoleViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private IMvxCommand<UserRole> _roleChoosedCommand;
        public IMvxCommand<UserRole> RoleChoosedCommand
        {
            get { return _roleChoosedCommand ?? (_roleChoosedCommand = new MvxAsyncCommand<UserRole>(OnRoleChoosedCommandAsync)); }
        }

        public ChoiseUserRoleViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private Task OnRoleChoosedCommandAsync(UserRole arg)
        {
            return _navigationService.Navigate<PreEntranceViewModel, UserRole>(arg);
        }
    }
}
