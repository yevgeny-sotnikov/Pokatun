using Pokatun.API.Entities;
using Pokatun.Data;

namespace Pokatun.API.Services
{
    public interface IHotelsApiService
    {
        Hotel GetById(long id);

        HotelShortInfoDto GetShortInfo(long id);

        void Update(HotelDto hotel);

        long Register(HotelRegistrationDto value);

        long Login(string email, string password);

        void ForgotPassword(string email);

        Hotel ValidateResetToken(string token);

        long ResetPassword(string token, string password);
    }
}
