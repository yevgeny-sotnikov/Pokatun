using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Pokatun.API.Services;
using Pokatun.Data;

namespace Pokatun.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelNumbersController : ControllerBase
    {
        private readonly IHotelNumbersApiService _hotelNumbersService;
        private readonly IRequestContext _requestContext;

        public HotelNumbersController(IHotelNumbersApiService hotelNumbersService, IRequestContext requestContext)
        {
            _hotelNumbersService = hotelNumbersService;
            _requestContext = requestContext;
        }

        [HttpGet]
        public ActionResult<ServerResponce<List<HotelDto>>> Get()
        {
            try
            {
                return Ok(new ServerResponce<List<HotelNumberDto>> { Data = _hotelNumbersService.GetAll(_requestContext.GetId(Request)) });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }

        // POST api/values
        [HttpPost]
        public ActionResult<ServerResponce> Post([FromBody] HotelNumberDto value)
        {
            try
            {
                _hotelNumbersService.AddNew(_requestContext.GetId(Request), value);

                return Ok(new ServerResponce<string> { Data = "OK" });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }

        [HttpPut("{id}")]
        public ActionResult<ServerResponce> Put(long id, [FromBody] HotelNumberDto value)
        {
            try
            {
                _hotelNumbersService.UpdateExists(_requestContext.GetId(Request), id, value);

                return Ok(new ServerResponce<string> { Data = "OK" });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            try
            {
                _hotelNumbersService.Delete(id);

                return Ok(new ServerResponce<string> { Data = "OK" });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }
    }
}
