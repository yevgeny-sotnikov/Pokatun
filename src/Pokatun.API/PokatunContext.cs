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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Hotel>().ToTable(nameof(Hotels));

            builder.Entity<Hotel>().HasIndex(h => h.Email).IsUnique();
        }
    }
}
