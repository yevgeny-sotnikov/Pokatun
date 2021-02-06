using System;
namespace Pokatun.Data
{
    public sealed class TokenInfoDto
    {
        public long AccountId { get; set; }

        public string Token { get; set; }

        public DateTime ExpirationTime { get; set; }

        public UserRole Role { get; set; }
    }
}
