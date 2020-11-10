using System;
using System.Threading.Tasks;
using Pokatun.API.Entities;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public interface IHotelsService
    {
        Hotel GetById(long id);

        long Register(HotelDto value);

        long Login(string email, string password);

        void ForgotPassword(string email);
    }
}
