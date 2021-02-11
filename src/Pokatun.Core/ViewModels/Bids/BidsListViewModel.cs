using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Pokatun.Core.Resources;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Bids
{
    public sealed class BidsListViewModel : BaseViewModel<List<HotelNumberDto>>
    {
        private IDictionary<long, HotelNumberDto> _hotelNumbers;

        public override string Title => Strings.MyBids;

        public string Subtitle => string.Format(Strings.BidsCounter, Bids.Count);

        private MvxObservableCollection<EditBidParameter> _bids = new MvxObservableCollection<EditBidParameter>();
        public MvxObservableCollection<EditBidParameter> Bids
        {
            get { return _bids; }
            set { SetProperty(ref _bids, value); }
        }

        private MvxAsyncCommand<EditBidParameter> _openBidCommand;
        private readonly IMvxNavigationService _navigationService;

        public IMvxAsyncCommand<EditBidParameter> OpenBidCommand
        {
            get
            {
                return _openBidCommand ?? (_openBidCommand = new MvxAsyncCommand<EditBidParameter>(DoOpenBidCommandAsync));
            }
        }

        public BidsListViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Prepare(List<HotelNumberDto> parameter)
        {
            _hotelNumbers = parameter.ToDictionary(x => x.Id);
            Bids.AddRange(parameter.SelectMany(x => x.Bids).Select(x => new EditBidParameter { Bid = x, HotelNumber = _hotelNumbers[x.HotelNumberId] }));
            RaisePropertyChanged(nameof(Subtitle));
        }

        private Task DoOpenBidCommandAsync(EditBidParameter arg)
        {
            return _navigationService.Navigate<ShowBidViewModel, EditBidParameter>(arg);
        }
    }
}
