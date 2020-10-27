using System;
using Microsoft.EntityFrameworkCore;
using Pokatun.Data;

namespace Pokatun.API.Models
{
    public class PokatunContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }

        public PokatunContext(DbContextOptions<PokatunContext> options) : base(options)
        {
        }
    }
}
