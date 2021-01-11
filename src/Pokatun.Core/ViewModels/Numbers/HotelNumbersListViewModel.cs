using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Pokatun.Core.ViewModels.Numbers
{
    public sealed class HotelNumbersListViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private MvxAsyncCommand _addCommand;

        public IMvxAsyncCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new MvxAsyncCommand(DoAddCommandAsync));
            }
        }

        public HotelNumbersListViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private Task DoAddCommandAsync()
        {
            return _navigationService.Navigate<EditHotelNumberViewModel>();
        }
    }
}
