using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Pokatun.API.Entities;
using Pokatun.API.Services;
using Pokatun.Data;

namespace Pokatun.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelNumbersController : ControllerBase
    {
        private readonly IHotelNumbersApiService _hotelNumbersService;

        public HotelNumbersController(IHotelNumbersApiService hotelNumbersService)
        {
            _hotelNumbersService = hotelNumbersService;
        }

        [HttpGet]
        public ActionResult<ServerResponce<List<HotelDto>>> Get()
        {
            try
            {
                return Ok(new ServerResponce<List<HotelNumberDto>> { Data = _hotelNumbersService.GetAll(GetHotelId()) });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }

        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public ActionResult<ServerResponce> Post([FromBody] HotelNumberDto value)
        {
            try
            {
                _hotelNumbersService.AddNew(GetHotelId(), value);

                return Ok(new ServerResponce<string> { Data = "OK" });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        private long GetHotelId()
        {
            StringValues tokenStr = Request.Headers[HeaderNames.Authorization];

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.ReadJwtToken(((string)tokenStr).Replace("Bearer ", string.Empty));

            return long.Parse(token.Claims.First(c => c.Type == "unique_name").Value);
        }
    }
}
