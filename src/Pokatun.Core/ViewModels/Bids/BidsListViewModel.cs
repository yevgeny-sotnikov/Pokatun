using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Pokatun.Data;

namespace Pokatun.Core.ViewModels.Bids
{
    public sealed class BidsListViewModel : BaseViewModel<List<BidDto>>
    {
        private MvxObservableCollection<BidDto> _bids = new MvxObservableCollection<BidDto>();
        public MvxObservableCollection<BidDto> Bids
        {
            get { return _bids; }
            set { SetProperty(ref _bids, value); }
        }

        private MvxAsyncCommand<BidDto> _openBidCommand;
        public IMvxAsyncCommand<BidDto> OpenBidCommand
        {
            get
            {
                return _openBidCommand ?? (_openBidCommand = new MvxAsyncCommand<BidDto>(DoOpenBidCommandAsync));
            }
        }

        public BidsListViewModel()
        {
        }

        public override void Prepare(List<BidDto> parameter)
        {

        }

        private Task DoOpenBidCommandAsync(BidDto arg)
        {
            throw new NotImplementedException();
        }

    }
}
