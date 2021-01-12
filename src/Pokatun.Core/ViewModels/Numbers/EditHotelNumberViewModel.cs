using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Pokatun.Core.Resources;

namespace Pokatun.Core.ViewModels.Numbers
{
    public sealed class EditHotelNumberViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public override string Title => Strings.NewHotelNumber;

        private short? _roomNumber;
        public short? RoomNumber
        {
            get { return _roomNumber; }
            set { SetProperty(ref _roomNumber, value); }
        }

        private MvxAsyncCommand _closeCommand;
        public IMvxAsyncCommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new MvxAsyncCommand(DoCloseCommandAsync));
            }
        }

        public EditHotelNumberViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private Task DoCloseCommandAsync()
        {
            return _navigationService.Close(this);
        }
    }
}
