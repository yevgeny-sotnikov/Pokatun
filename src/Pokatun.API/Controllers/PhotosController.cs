using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Pokatun.Data;

namespace Pokatun.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhotosController : ControllerBase
    {
        private const string PhotosDirName = "photos";
        private const string HotelsDirName = "hotels";

        private static readonly FileExtensionContentTypeProvider ContentTypeProvider = new FileExtensionContentTypeProvider();

        private readonly IWebHostEnvironment _environment;
        private readonly IFileSystem _fileSystem;

        public PhotosController(IWebHostEnvironment hostingEnvironment, IFileSystem fileSystem)
        {
            _environment = hostingEnvironment;
            _fileSystem = fileSystem;
        }

        [HttpGet]
        public ActionResult Get([FromQuery] string name)
        {
            string fullName = _fileSystem.Path.Combine(_environment.WebRootPath, PhotosDirName, HotelsDirName, name);

            string contentType;

            if (!ContentTypeProvider.TryGetContentType(name, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return File(_fileSystem.FileStream.Create(fullName, FileMode.Open), contentType);
        }

        [HttpPost]
        public ActionResult<ServerResponce<string>> Post(IFormFile file)
        {
            string photosDir = _fileSystem.Path.Combine(_environment.WebRootPath, PhotosDirName, HotelsDirName);

            _fileSystem.Directory.CreateDirectory(photosDir);

            if (file.Length == 0)
            {
                return BadRequest(ServerResponce.ForErrors(ErrorCodes.InvalidFileError));
            }

            string rndName;
            string newFilename;

            do
            {
                rndName = _fileSystem.Path.GetRandomFileName() + _fileSystem.Path.GetExtension(file.FileName);
                newFilename = _fileSystem.Path.Combine(photosDir, rndName);
            }
            while (_fileSystem.File.Exists(newFilename));

            using (Stream fileStream = _fileSystem.FileStream.Create(newFilename, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return Ok(new ServerResponce<string> { Data = rndName });
        }
    }
}
