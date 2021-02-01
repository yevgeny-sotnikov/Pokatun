using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Pokatun.Core.Executors;
using Pokatun.Core.Resources;
using Pokatun.Core.Services;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Numbers
{
    public sealed class HotelNumbersListViewModel : BaseViewModel<List<HotelNumberDto>>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IHotelNumbersService _hotelNumbersService;
        private readonly INetworkRequestExecutor _networkRequestExecutor;

        public override string Title => Strings.MyHotelNumbers;

        public string Subtitle => string.Format(Strings.AddedHotelNumbersCounter, HotelNumbers.Count);

        private MvxObservableCollection<HotelNumberDto> _hotelNumbers = new MvxObservableCollection<HotelNumberDto>();
        public MvxObservableCollection<HotelNumberDto> HotelNumbers
        {
            get { return _hotelNumbers; }
            set { SetProperty(ref _hotelNumbers, value); }
        }

        private MvxAsyncCommand<HotelNumberDto> _openHotelNumberCommand;
        public IMvxAsyncCommand<HotelNumberDto> OpenHotelNumberCommand
        {
            get
            {
                return _openHotelNumberCommand ?? (_openHotelNumberCommand = new MvxAsyncCommand<HotelNumberDto>(DoOpenHotelNumberCommandAsync));
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
            RaisePropertyChanged(nameof(Subtitle));
        }

        private async Task DoAddCommandAsync()
        {
            HotelNumberDto result = await _navigationService.Navigate<EditHotelNumberViewModel, HotelNumberDto, HotelNumberDto>(null);

            if (result == null)
                return;

            HotelNumbers.Add(result);
            await RaisePropertyChanged(nameof(Subtitle));
        }

        private async Task DoOpenHotelNumberCommandAsync(HotelNumberDto hotelNumberDto)
        {
            HotelNumberDto result = await _navigationService.Navigate<ShowHotelNumberViewModel, HotelNumberDto, HotelNumberDto>(hotelNumberDto);

            if (result == null)
                return;

            int index = HotelNumbers.IndexOf(hotelNumberDto);
            HotelNumbers.RemoveAt(index);
            HotelNumbers.Insert(index, result);
        }

        public async Task DoDeleteHotelNumberCommandAsync(int index)
        {
            HotelNumberDto hotelNumber = HotelNumbers[index];

            HotelNumbers.RemoveAt(index);

            await RaisePropertyChanged(nameof(Subtitle));

            ServerResponce responce = await _networkRequestExecutor.MakeRequestAsync(
                () => _hotelNumbersService.DeleteAsync(hotelNumber.Id), new HashSet<string>()
            );

            if (responce != null)
                return;

            HotelNumbers.Insert(index, hotelNumber);
            await RaisePropertyChanged(nameof(Subtitle));
        }
    }
}
