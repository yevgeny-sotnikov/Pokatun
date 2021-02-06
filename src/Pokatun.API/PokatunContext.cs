using Microsoft.EntityFrameworkCore;
using Pokatun.API.Entities;

namespace Pokatun.API.Models
{
    public class PokatunContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Phone> Phones { get; set; }

        public DbSet<SocialResource> SocialResources { get; set; }

        public DbSet<HotelNumber> HotelNumbers { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Tourist> Tourists { get; set; }


        public PokatunContext(DbContextOptions<PokatunContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Hotel>().ToTable(nameof(Hotels));
            builder.Entity<Phone>().ToTable(nameof(Phones));
            builder.Entity<SocialResource>().ToTable(nameof(SocialResources));
            builder.Entity<HotelNumber>().ToTable(nameof(HotelNumbers));
            builder.Entity<Account>().ToTable(nameof(Accounts));
            builder.Entity<Tourist>().ToTable(nameof(Tourists));

            builder.Entity<Account>().HasIndex(a => a.Email).IsUnique();
            builder.Entity<Account>().HasIndex(a => a.ResetToken).IsUnique();
            builder.Entity<Account>().HasIndex(h => h.PhotoName).IsUnique();

            builder.Entity<Hotel>().HasIndex(h => h.USREOU).IsUnique();
            builder.Entity<Hotel>().HasIndex(h => h.IBAN).IsUnique();

            builder.Entity<Account>().HasIndex(h => h.Email).IsUnique();

            builder.Entity<SocialResource>().HasIndex(sr => sr.Link).IsUnique();

            builder.Entity<HotelNumber>().HasIndex(sr => sr.Number).IsUnique();
        }
    }
}
