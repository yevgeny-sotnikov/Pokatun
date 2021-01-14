using System;
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
            bool supperIncluded,
            long price
        );
    }
}
