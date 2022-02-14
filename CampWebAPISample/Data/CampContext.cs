using Microsoft.EntityFrameworkCore;
using CampWebAPISample.Data.Entities;

namespace CampWebAPISample.Data
{
    public class CampContext : DbContext
    {
        private readonly IConfiguration _config;

        public CampContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public DbSet<Camp> Camps { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Talk> Talks { get; set; }
        public DbSet<Location> Location { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("CodeCamp"));
        }

        protected override void OnModelCreating(ModelBuilder bldr)
        {
            bldr.Entity<Camp>()
                .HasData(new
                {
                    CampId = 1,
                    Moniker = "Hatchery2022-2",
                    Name = ".NET Hatchery  2022",
                    EventDate = new DateTime(2022, 2, 10),
                    LocationId = 1,
                    Length = 1
                });
            bldr.Entity<Location>()
                .HasData(new
                {
                    LocationId = 1,
                    Address = "Adresa 123",
                    City = "Praha",
                    Country = "Czech Republic",
                    PostalCode = "111 50"
                });

            bldr.Entity<Talk>()
                .HasData(new
                {
                    TalkId = 1,
                    Title = "ASP.NET Core WebAPI",
                    Abstract = "bla bla bla",
                    CampId = 1,
                    SpeakerId = 1
                },
                new
                {
                    TalkId = 2,
                    Title = "C# Fundamentals",
                    Abstract = "bla bla bla",
                    CampId = 1,
                    SpeakerId = 2

                });

            bldr.Entity<Speaker>()
                .HasData(new
                {
                    SpeakerId = 1,
                    FirstName = "Radek",
                    LastName = "Garzina",
                    Company = "Unicorn",
                    Email = "radek.garzina@gmail.com"
                },
                new
                {
                    SpeakerId = 2,
                    FirstName = "Bill",
                    LastName = "Gates",
                    Company = "Microsoft",
                    Email = "bill.gates@microsoft.com"
                });
        }
    }
}
