using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pokatun.API.Entities;
using Pokatun.API.Helpers;
using Pokatun.API.Models;
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
        [HttpPost("register")]
        public ActionResult<ServerResponce<string>> Register([FromBody] HotelDto value)
        {
            try
            {
                long id = _hotelsService.RegisterAsync(value);

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, id.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                string tokenString = tokenHandler.WriteToken(token);

                return Ok(new ServerResponce<string> { Data = tokenString });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForError(ex.ErrorCode));
            }
        }
    }
}
