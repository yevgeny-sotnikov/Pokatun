using Microsoft.AspNetCore.Mvc;
using Pokatun.API.Services;
using Pokatun.Data;

namespace Pokatun.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TouristsController : ControllerBase
    {
        private readonly ITouristsApiService _touristsService;

        public TouristsController(ITouristsApiService touristsService)
        {
            _touristsService = touristsService;
        }

        [HttpGet("{id}")]
        public ActionResult<ServerResponce<TouristDto>> Get(long id)
        {
            try
            {
                return Ok(new ServerResponce<TouristDto> { Data = _touristsService.GetById(id) });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<ServerResponce<TouristShortInfoDto>> ShortInfo(long id)
        {
            try
            {
                return Ok(new ServerResponce<TouristShortInfoDto> { Data = _touristsService.GetShortInfo(id) });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }
    }
}
