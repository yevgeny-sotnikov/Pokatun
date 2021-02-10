using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pokatun.API.Services;
using Pokatun.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pokatun.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidsController : Controller
    {
        private readonly IBidsApiService _bidsService;

        public BidsController(IBidsApiService bidsService)
        {
            _bidsService = bidsService;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<ServerResponce> Post([FromBody] CreateBidsDto value)
        {
            try
            {
                _bidsService.AddNew(value);

                return Ok(new ServerResponce<string> { Data = "OK" });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }
    }
}
