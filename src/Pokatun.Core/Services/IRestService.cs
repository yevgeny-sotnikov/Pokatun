using System;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface IRestService
    {
        Task<ServerResponce<T>> PostAsync<T>(string path, object body, bool needAuth = true);
    }
}
