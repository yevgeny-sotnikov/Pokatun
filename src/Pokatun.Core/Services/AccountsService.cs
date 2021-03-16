using System;
using System.Collections.Generic;
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

        public Task<ServerResponce<TokenInfoDto>> RegisterHotelAsync(
            string hotelName,
            string fullCompanyName,
            string email,
            string password,
            string phoneNumber,
            string bankName,
            string IBAN,
            long? bankCard,
            int USREOU)
        {
            return _restService.PostAsync<TokenInfoDto>("accounts/hotelregistration",
                new HotelRegistrationDto
                {
                    HotelName = hotelName,
                    FullCompanyName = fullCompanyName,
                    Email = email,
                    Password = password,
                    Phones = new List<PhoneDto> { new PhoneDto { Number = phoneNumber } },
                    BankName = bankName,
                    IBAN = IBAN,
                    BankCard = bankCard,
                    USREOU = USREOU
                },
                false
            );
        }

        public Task<ServerResponce<TokenInfoDto>> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(email));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(password));
            }

            return _restService.PostAsync<TokenInfoDto>("accounts/login", new LoginDto { Email = email, Password = password }, false);
        }

        public async Task<ServerResponce> ForgotPasswordAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(email));
            }

            return await _restService.PostAsync<object>("accounts/forgot-password", new ForgotPasswordRequest { Email = email }, false);
        }

        public async Task<ServerResponce> ValidateResetToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(token));
            }

            return await _restService.PostAsync<object>("accounts/validate-reset-token", new ValidateResetTokenRequest { Token = token }, false);
        }

        public Task<ServerResponce<TokenInfoDto>> ResetPassword(string token, string password)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(token));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(Constants.InvalidValueExceptionMessage, nameof(password));
            }

            return _restService.PostAsync<TokenInfoDto>("accounts/reset-password", new ResetPasswordRequest { Token = token, Password = password }, false);
        }
    }
}
