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
        private readonly IRequestContext _requestContext;

        public BidsController(IBidsApiService bidsService, IRequestContext requestContext)
        {
            _bidsService = bidsService;
            _requestContext = requestContext;
        }

        [HttpGet]
        public ActionResult<ServerResponce<List<BidDto>>> Get()
        {
            try
            {
                return Ok(new ServerResponce<List<BidDto>> { Data = _bidsService.GetAll(_requestContext.GetId(Request)) });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
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

        [HttpPut("{id}")]
        public ActionResult<ServerResponce> Put(long id, [FromBody] UpdateBidDto value)
        {
            try
            {
                _bidsService.UpdateExists(id, value);

                return Ok(new ServerResponce<string> { Data = "OK" });
            }
            catch (ApiException ex)
            {
                return BadRequest(ServerResponce.ForErrors(ex.ErrorCodes));
            }
        }
    }
}
