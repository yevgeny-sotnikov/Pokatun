using System;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Pokatun.Core.Services;
using Pokatun.Core.ViewModels.Menu;
using Pokatun.Data;

namespace Pokatun.Core.Executors
{
    public sealed class HotelFinalSetupExecutor : IHotelFinalSetupExecutor
    {
        private readonly IHotelsService _hotelsService;
        private readonly IMvxNavigationService _navigationService;

        public HotelFinalSetupExecutor(IHotelsService hotelsService, IMvxNavigationService navigationService)
        {
            _hotelsService = hotelsService;
            _navigationService = navigationService;
        }

        public async Task FinalizeSetupAsync(TokenInfoDto dto, IMvxViewModel closeViewModel)
        {
            if (closeViewModel == null)
            {
                throw new ArgumentNullException(nameof(closeViewModel));
            }

            ServerResponce<HotelShortInfoDto> serverResponce;

            do
            {
                serverResponce = await _hotelsService.GetShortInfoAsync(dto.AccountId);
            }
            while (!serverResponce.Success);

            await _navigationService.Close(closeViewModel);
            await _navigationService.Navigate<HotelMenuViewModel, HotelShortInfoDto>(serverResponce.Data);
        }
    }
}
