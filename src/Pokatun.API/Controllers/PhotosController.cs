using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokatun.Data;

namespace Pokatun.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhotosController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public PhotosController(IWebHostEnvironment hostingEnvironment)
        {
            _environment = hostingEnvironment;
        }

        [HttpPost]
        public ActionResult<ServerResponce<string>> Post(IFormFile file)
        {
            string photosDir = Path.Combine(_environment.WebRootPath, "photos", "hotels");
            Directory.CreateDirectory(photosDir);

            if (file.Length == 0)
            {
                return BadRequest(ServerResponce.ForErrors(ErrorCodes.InvalidFileError));
            }

            string newFilename = Path.Combine(photosDir, Path.GetRandomFileName());

            using (FileStream fileStream = new FileStream(newFilename, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return Ok(new ServerResponce<string> { Data = newFilename });
        }
    }
}
