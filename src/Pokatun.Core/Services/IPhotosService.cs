using System;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface IPhotosService
    {
        Task<ServerResponce<string>> UploadAsync(string photofilePath);
    }
}
