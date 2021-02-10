using Microsoft.AspNetCore.Http;

namespace Pokatun.API
{
    public interface IRequestContext
    {
        public long GetId(HttpRequest request);
    }
}
