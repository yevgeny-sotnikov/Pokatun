using Microsoft.EntityFrameworkCore;
using Pokatun.API.Entities;

namespace Pokatun.API.Models
{
    public class PokatunContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Phone> Phones { get; set; }

        public DbSet<SocialResource> SocialResources { get; set; }

        public PokatunContext(DbContextOptions<PokatunContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Hotel>().ToTable(nameof(Hotels));
            builder.Entity<Phone>().ToTable(nameof(Phones));
            builder.Entity<SocialResource>().ToTable(nameof(SocialResources));

            builder.Entity<Hotel>().HasIndex(h => h.Email).IsUnique();
            builder.Entity<Hotel>().HasIndex(h => h.USREOU).IsUnique();
            builder.Entity<Hotel>().HasIndex(h => h.IBAN).IsUnique();
            builder.Entity<Hotel>().HasIndex(h => h.ResetToken).IsUnique();
            builder.Entity<Hotel>().HasIndex(h => h.PhotoUrl).IsUnique();

            builder.Entity<SocialResource>().HasIndex(sr => sr.Link).IsUnique();

        }
    }
}
