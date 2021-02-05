using System;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface IAccountsService
    {
        Task<ServerResponce<TokenInfoDto>> RegisterTouristAsync(string fullname, string email, string password, string phoneNumber);
    }
}
