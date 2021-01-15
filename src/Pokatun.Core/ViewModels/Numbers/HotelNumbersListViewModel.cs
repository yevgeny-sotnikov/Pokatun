using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Numbers
{
    public sealed class HotelNumbersListViewModel : BaseViewModel<List<HotelNumberDto>>
    {
        private readonly IMvxNavigationService _navigationService;

        private MvxObservableCollection<HotelNumberDto> _hotelNumbers = new MvxObservableCollection<HotelNumberDto>();
        public MvxObservableCollection<HotelNumberDto> HotelNumbers
        {
            get { return _hotelNumbers; }
            set { SetProperty(ref _hotelNumbers, value); }
        }

        private MvxAsyncCommand _addCommand;
        public IMvxAsyncCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new MvxAsyncCommand(DoAddCommandAsync));
            }
        }

        private MvxCommand<HotelNumberDto> _deleteCommand;
        public IMvxCommand<HotelNumberDto> DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new MvxCommand<HotelNumberDto>(DoDeleteHotelNumberCommand));
            }
        }

        public HotelNumbersListViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Prepare(List<HotelNumberDto> parameter)
        {
            HotelNumbers.AddRange(parameter);
        }

        private Task DoAddCommandAsync()
        {
            return _navigationService.Navigate<EditHotelNumberViewModel>();
        }

        private void DoDeleteHotelNumberCommand(HotelNumberDto obj)
        {
            HotelNumbers.Remove(obj);
        }
    }
}
