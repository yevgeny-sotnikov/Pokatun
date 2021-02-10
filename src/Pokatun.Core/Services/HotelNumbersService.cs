using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokatun.Data;

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
            bool supperIncluded)
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
                    SupperIncluded = supperIncluded
                }
            );
        }

        public async Task<ServerResponce> UpdateExistsAsync(
            long id,
            short number,
            RoomLevel level,
            byte roomsAmount,
            byte visitorsAmount,
            string description,
            bool cleaningNeeded,
            bool nutritionNeeded,
            bool breakfastIncluded,
            bool dinnerIncluded,
            bool supperIncluded)
        {
            return await _restService.PutAsync<object>(
                "hotelnumbers", id,
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
                    SupperIncluded = supperIncluded
                }
            );
        }

        public async Task<ServerResponce> DeleteAsync(long id)
        {
            return await _restService.DeleteAsync<object>("hotelnumbers", id);
        }

        public Task<ServerResponce<List<HotelNumberDto>>> GetAllAsync(bool withBids = false)
        {
            return _restService.GetAsync<List<HotelNumberDto>>("hotelnumbers?withBids=" + withBids);
        }
    }
}
