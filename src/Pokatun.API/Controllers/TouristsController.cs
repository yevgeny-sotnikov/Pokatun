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

        [HttpGet("[action]/{id}")]
        public ActionResult<ServerResponce<TouristShortInfoDto>> ShortInfo(long id)
        {
            try
            {
                return Ok(new ServerResponce<TouristShortInfoDto>
                {
                    Data = _touristsService.GetShortInfo(id)
                });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }
    }
}
