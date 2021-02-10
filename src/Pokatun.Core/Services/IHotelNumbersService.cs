using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface IHotelNumbersService
    {
        Task<ServerResponce> AddNewAsync(
            short number,
            RoomLevel level,
            byte roomsAmount,
            byte visitorsAmount,
            string description,
            bool cleaningNeeded,
            bool nutritionNeeded,
            bool breakfastIncluded,
            bool dinnerIncluded,
            bool supperIncluded
        );

        Task<ServerResponce> UpdateExistsAsync(
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
            bool supperIncluded);

        Task<ServerResponce<List<HotelNumberDto>>> GetAllAsync(bool withBids = false);

        Task<ServerResponce> DeleteAsync(long id);
    }
}
