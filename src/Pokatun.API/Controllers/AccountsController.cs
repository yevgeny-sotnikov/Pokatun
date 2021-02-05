using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
