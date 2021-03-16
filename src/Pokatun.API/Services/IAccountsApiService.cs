using System;
using Pokatun.API.Entities;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public interface IAccountsApiService
    {
        TokenInfoDto RegisterNewTourist(TouristRegistrationDto value);

        TokenInfoDto RegisterNewHotel(HotelRegistrationDto value);

        TokenInfoDto Login(string email, string password);

        void ForgotPassword(string email);

        Account ValidateResetToken(string token);

        TokenInfoDto ResetPassword(string token, string password);
    }
}
