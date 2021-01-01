using System;
using System.IO;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface IPhotosService
    {
        Task<Stream> GetAsync(string photoName);

        Task<ServerResponce<string>> UploadAsync(string photofilePath);
    }
}
