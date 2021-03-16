using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokatun.API.Services;
using Pokatun.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pokatun.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsApiService _accountsService;

        public AccountsController(IAccountsApiService hotelsService)
        {
            _accountsService = hotelsService;
        }

        [AllowAnonymous]
        [HttpPost("touristregistration")]
        public ActionResult<ServerResponce<TokenInfoDto>> RegisterNewTourist([FromBody] TouristRegistrationDto value)
        {
            try
            {
                return Ok(new ServerResponce<TokenInfoDto> { Data = _accountsService.RegisterNewTourist(value) });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }

        [AllowAnonymous]
        [HttpPost("hotelregistration")]
        public ActionResult<ServerResponce<TokenInfoDto>> RegisterNewHotel([FromBody] HotelRegistrationDto value)
        {
            try
            {
                return Ok(new ServerResponce<TokenInfoDto> { Data = _accountsService.RegisterNewHotel(value) });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<ServerResponce<TokenInfoDto>> Login([FromBody] LoginDto value)
        {
            try
            {
                return Ok(new ServerResponce<TokenInfoDto> { Data = _accountsService.Login(value.Email, value.Password) });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public ActionResult<ServerResponce<string>> ForgotPassword(ForgotPasswordRequest model)
        {
            try
            {
                _accountsService.ForgotPassword(model.Email);
                return Ok(new ServerResponce<string> { Data = "OK" });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }

        [AllowAnonymous]
        [HttpPost("validate-reset-token")]
        public IActionResult ValidateResetToken(ValidateResetTokenRequest model)
        {
            try
            {
                _accountsService.ValidateResetToken(model.Token);
                return Ok(new ServerResponce<string> { Data = "OK" });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public ActionResult<ServerResponce<TokenInfoDto>> ResetPassword(ResetPasswordRequest model)
        {
            try
            {
                return Ok(new ServerResponce<TokenInfoDto> { Data = _accountsService.ResetPassword(model.Token, model.Password) });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }
    }
}
