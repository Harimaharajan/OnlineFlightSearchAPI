using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.DBModelsFolder
{
    public partial class FlightDBContext : DbContext, IFlightDBContext
    {
        public FlightDBContext(DbContextOptions<FlightDBContext> options)
            : base(options)
        {
        }

        public DbSet<AirportDetail> Airports { get; set; }
        public DbSet<FlightDetail> Flights { get; set; }
        public DbSet<UserLoginModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:SqlConnectionString"]);
        }
    }
}
