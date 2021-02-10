using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Pokatun.API
{
    public class RequestContext : IRequestContext
    {
        public long GetId(HttpRequest request)
        {
            StringValues tokenStr = request.Headers[HeaderNames.Authorization];

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.ReadJwtToken(((string)tokenStr).Replace("Bearer ", string.Empty));

            return long.Parse(token.Claims.First(c => c.Type == "unique_name").Value);
        }
    }
}
