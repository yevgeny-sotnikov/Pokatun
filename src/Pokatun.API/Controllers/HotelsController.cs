using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pokatun.API.Entities;
using Pokatun.API.Helpers;
using Pokatun.API.Services;
using Pokatun.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pokatun.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelsApiService _hotelsService;
        private readonly AppSettings _appSettings;

        public HotelsController(IHotelsApiService hotelsService, IOptions<AppSettings> appSettings)
        {
            _hotelsService = hotelsService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("{id}")]
        public ActionResult<ServerResponce<HotelDto>> Get(long id)
        {
            try
            {
                Hotel hotel = _hotelsService.GetById(id);
                return Ok(new ServerResponce<HotelDto>
                {
                    Data = new HotelDto
                    {
                        Id = hotel.Id,
                        HotelName = hotel.HotelName,
                        Phones = new List<PhoneDto>(hotel.Phones.Select(p => new PhoneDto { Id = p.Id, Number = p.Number })),
                        SocialResources = new List<SocialResourceDto>(hotel.SocialResources.Select(sr => new SocialResourceDto { Id = sr.Id, Link = sr.Link })),
                        Email = hotel.Email,
                        FullCompanyName = hotel.FullCompanyName,
                        BankCard = hotel.BankCard,
                        IBAN = hotel.IBAN,
                        BankName = hotel.BankName,
                        USREOU = hotel.USREOU,
                        CheckInTime = hotel.CheckInTime,
                        CheckOutTime = hotel.CheckOutTime,
                        HotelDescription = hotel.HotelDescription,
                        WithinTerritoryDescription = hotel.WithinTerritoryDescription,
                        Address = hotel.Address,
                        Longtitude = hotel.Longtitude,
                        Latitude = hotel.Latitude,
                        PhotoUrl = hotel.PhotoUrl
                    }
                });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<ServerResponce<HotelShortInfoDto>> ShortInfo(long id)
        {
            try
            {
                return Ok(new ServerResponce<HotelShortInfoDto>
                {
                    Data = _hotelsService.GetShortInfo(id)
                });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }

        [HttpPost]
        public ActionResult<ServerResponce> Post([FromBody] HotelDto hotel)
        {
            try
            {
                _hotelsService.Update(hotel);
                return Ok(new ServerResponce<string> { Data = "OK" });
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
            return HandleAuthorizationRequest(() => _hotelsService.Login(value.Email, value.Password));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<ServerResponce<TokenInfoDto>> Register([FromBody] HotelRegistrationDto value)
        {
            return HandleAuthorizationRequest(() => _hotelsService.Register(value));
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public ActionResult<ServerResponce<string>> ForgotPassword(ForgotPasswordRequest model)
        {
            try
            {
                _hotelsService.ForgotPassword(model.Email);
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
                _hotelsService.ValidateResetToken(model.Token);
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
            return HandleAuthorizationRequest(() => _hotelsService.ResetPassword(model.Token, model.Password));
        }

        private ActionResult HandleAuthorizationRequest(Func<long> serviceFunc)
        {
            try
            {
                long id = serviceFunc();

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                DateTime expireTime = DateTime.UtcNow.AddDays(_appSettings.TokenExpirationDays);

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, id.ToString()) }),
                    Expires = expireTime,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                string tokenString = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

                return Ok(new ServerResponce<TokenInfoDto>
                {
                    Data = new TokenInfoDto
                    {
                        AccountId = id,
                        Token = tokenString,
                        ExpirationTime = expireTime
                    }
                });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }
    }
}
