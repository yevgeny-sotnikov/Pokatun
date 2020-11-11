using Pokatun.API.Entities;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public interface IHotelsApiService
    {
        Hotel GetById(long id);

        long Register(HotelDto value);

        long Login(string email, string password);

        void ForgotPassword(string email);

        Hotel ValidateResetToken(string token);

        long ResetPassword(string token, string password);
    }
}
