using System;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public sealed class AccountsService : IAccountsService
    {
        private readonly IRestService _restService;

        public AccountsService(IRestService restService)
        {
            _restService = restService;
        }

        public Task<ServerResponce<TokenInfoDto>> RegisterTouristAsync(string fullname, string email, string password, string phoneNumber)
        {
            return _restService.PostAsync<TokenInfoDto>("accounts/touristregistration",
                new TouristRegistrationDto
                {
                    FullName = fullname,
                    Email = email,
                    Password = password,
                    PhoneNumber = phoneNumber,
                },
                false
            );
        }
    }
}
