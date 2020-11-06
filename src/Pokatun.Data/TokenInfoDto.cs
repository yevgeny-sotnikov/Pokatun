using System;
namespace Pokatun.Data
{
    public sealed class TokenInfoDto
    {
        public string Token { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}
