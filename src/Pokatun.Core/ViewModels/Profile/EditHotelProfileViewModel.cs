using System;
using MvvmCross.Navigation;

namespace Pokatun.Core.ViewModels.Profile
{
    public sealed class EditHotelProfileViewModel : BaseViewModelResult<bool>
    {
        public IMvxNavigationService NavigationService { get; private set; }




        public EditHotelProfileViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
        }
    }
}
