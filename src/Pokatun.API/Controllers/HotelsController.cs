using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Pokatun.API.Entities;
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

        public HotelsController(IHotelsApiService hotelsService)
        {
            _hotelsService = hotelsService;
        }

        [HttpGet("{id}")]
        public ActionResult<ServerResponce<HotelDto>> Get(long id)
        {
            try
            {
                return Ok(new ServerResponce<HotelDto> { Data = _hotelsService.GetById(id) });
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
    }
}
