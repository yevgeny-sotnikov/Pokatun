using System;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public interface IAccountsApiService
    {
        TokenInfoDto RegisterNewTourist(TouristRegistrationDto value);
    }
}
