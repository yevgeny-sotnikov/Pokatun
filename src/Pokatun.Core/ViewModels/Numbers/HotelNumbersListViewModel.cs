using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Pokatun.Core.Executors;
using Pokatun.Core.Services;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Numbers
{
    public sealed class HotelNumbersListViewModel : BaseViewModel<List<HotelNumberDto>>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IHotelNumbersService _hotelNumbersService;
        private readonly INetworkRequestExecutor _networkRequestExecutor;
        private MvxObservableCollection<HotelNumberDto> _hotelNumbers = new MvxObservableCollection<HotelNumberDto>();
        public MvxObservableCollection<HotelNumberDto> HotelNumbers
        {
            get { return _hotelNumbers; }
            set { SetProperty(ref _hotelNumbers, value); }
        }

        private MvxAsyncCommand _openHotelNumberCommand;

        public IMvxAsyncCommand OpenHotelNumberCommand
        {
            get
            {
                return _openHotelNumberCommand ?? (_openHotelNumberCommand = new MvxAsyncCommand(DoOpenHotelNumberCommandAsync));
            }
        }

        private MvxAsyncCommand _addCommand;
        public IMvxAsyncCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new MvxAsyncCommand(DoAddCommandAsync));
            }
        }

        private MvxAsyncCommand<int> _deleteCommand;
        public IMvxAsyncCommand<int> DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new MvxAsyncCommand<int>(DoDeleteHotelNumberCommandAsync));
            }
        }

        public HotelNumbersListViewModel(IMvxNavigationService navigationService, IHotelNumbersService hotelNumbersService, INetworkRequestExecutor networkRequestExecutor)
        {
            _navigationService = navigationService;
            _hotelNumbersService = hotelNumbersService;
            _networkRequestExecutor = networkRequestExecutor;
        }

        public override void Prepare(List<HotelNumberDto> parameter)
        {
            HotelNumbers.AddRange(parameter);
        }

        private Task DoAddCommandAsync()
        {
            return _navigationService.Navigate<EditHotelNumberViewModel>();
        }

        private Task DoOpenHotelNumberCommandAsync()
        {
            return _navigationService.Navigate<ShowHotelNumberViewModel>();
        }

        public async Task DoDeleteHotelNumberCommandAsync(int index)
        {
            HotelNumberDto hotelNumber = HotelNumbers[index];

            HotelNumbers.RemoveAt(index);

            ServerResponce responce = await _networkRequestExecutor.MakeRequestAsync(
                () => _hotelNumbersService.DeleteAsync(hotelNumber.Id), new HashSet<string>()
            );

            if (responce != null)
                return;

            HotelNumbers.Insert(index, hotelNumber);
        }
    }
}
