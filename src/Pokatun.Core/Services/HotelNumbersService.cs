using System;
using System.Threading.Tasks;
using Pokatun.Data;
using RestSharp;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.Services
{
    public sealed class HotelNumbersService : IHotelNumbersService
    {
        private readonly IRestService _restService;

        public HotelNumbersService(IRestService restService)
        {
            _restService = restService;
        }

        public async Task<ServerResponce> AddNewAsync(
            short number,
            RoomLevel level,
            byte roomsAmount,
            byte visitorsAmount,
            string description,
            bool cleaningNeeded,
            bool nutritionNeeded,
            bool breakfastIncluded,
            bool dinnerIncluded,
            bool supperIncluded,
            long price
        )
        {
            return await _restService.PostAsync<object>(
                "hotelnumbers",
                new HotelNumberDto
                {
                    Number = number,
                    Level = level,
                    RoomsAmount = roomsAmount,
                    VisitorsAmount = visitorsAmount,
                    Description = description,
                    CleaningNeeded = cleaningNeeded,
                    NutritionNeeded = nutritionNeeded,
                    BreakfastIncluded = breakfastIncluded,
                    DinnerIncluded = dinnerIncluded,
                    SupperIncluded = supperIncluded,
                    Price = price
                }
            );
        }
    }
}
