using Microsoft.EntityFrameworkCore;
using Pokatun.API.Entities;

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
            builder.Entity<Hotel>().HasIndex(h => h.USREOU).IsUnique();
            builder.Entity<Hotel>().HasIndex(h => h.IBAN).IsUnique();
            builder.Entity<Hotel>().HasIndex(h => h.ResetToken).IsUnique();
        }
    }
}
