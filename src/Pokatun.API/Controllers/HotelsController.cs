using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pokatun.API.Models;
using Pokatun.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pokatun.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly PokatunContext _dbContext;

        public HotelsController(PokatunContext dbContext)
        {
            _dbContext = dbContext;
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost("register")]
        public async Task<ActionResult<ServerResponce<string>>> PostAsync([FromBody] Hotel value)
        {
            //value.Email = null;
            string email = value.Email.ToLower();

            if (_dbContext.Hotels.Any(hotel => hotel.Email == email))
            {
                return BadRequest(ServerResponce.ForError(ErrorCodes.AccountAllreadyExistsError));
            }

            _dbContext.Hotels.Add(value);
            await _dbContext.SaveChangesAsync();
            
            return Ok(new ServerResponce<string> { Success = true, Data = "ddsff" });
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}
    }
}
