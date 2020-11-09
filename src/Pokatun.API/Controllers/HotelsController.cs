using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IHotelsService _hotelsService;
        private readonly AppSettings _appSettings;

        public HotelsController(IHotelsService hotelsService, IOptions<AppSettings> appSettings)
        {
            _hotelsService = hotelsService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("{number}")]
        public IActionResult Get(int number)
        {
            return Ok(number * 2);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<ServerResponce<TokenInfoDto>> Login([FromBody] LoginDto value)
        {
            try
            {
                long id = _hotelsService.Login(value.Email, value.Password);

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

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<ServerResponce<TokenInfoDto>> Register([FromBody] HotelDto value)
        {
            try
            {
                long id = _hotelsService.Register(value);

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
