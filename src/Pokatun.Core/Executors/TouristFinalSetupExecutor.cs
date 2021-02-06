using System;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Pokatun.Core.Services;
using Pokatun.Core.ViewModels.Menu;
using Pokatun.Core.ViewModels.Registration;
using Pokatun.Data;

namespace Pokatun.Core.Executors
{
    public sealed class TouristFinalSetupExecutor : ITouristFinalSetupExecutor
    {
        private readonly ITouristsService _touristService;
        private readonly IMvxNavigationService _navigationService;

        public TouristFinalSetupExecutor(ITouristsService touristService, IMvxNavigationService navigationService)
        {
            _touristService = touristService;
            _navigationService = navigationService;
        }

        public async Task FinalizeSetupAsync(TokenInfoDto dto, IMvxViewModel closeViewModel, bool needLoadShortInfo = true)
        {
            if (closeViewModel == null)
            {
                throw new ArgumentNullException(nameof(closeViewModel));
            }

            TouristShortInfoDto shortInfo = null;

            if (needLoadShortInfo)
            {
                ServerResponce<TouristShortInfoDto> serverResponce;

                do
                {
                    serverResponce = await _touristService.GetShortInfoAsync(dto.AccountId);
                }
                while (!serverResponce.Success);

                shortInfo = serverResponce.Data;
            }

            await _navigationService.Close(closeViewModel);
            await _navigationService.Navigate<TouristMenuViewModel, TouristShortInfoDto>(shortInfo);
        }
    }
}
