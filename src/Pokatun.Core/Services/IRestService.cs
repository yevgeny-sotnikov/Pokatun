using System;
using System.Threading.Tasks;
using Pokatun.Data;

namespace Pokatun.Core.Services
{
    public interface IRestService
    {
        Task<ServerResponce<T>> GetAsync<T>(string path, bool needAuth = true);

        Task<ServerResponce<T>> PostAsync<T>(string path, object body, bool needAuth = true);

        Task<ServerResponce<T>> DeleteAsync<T>(string path, long id, bool needAuth = true);

        Task<ServerResponce<T>> PutAsync<T>(string path, long id, object body, bool needAuth = true);
    }
}
