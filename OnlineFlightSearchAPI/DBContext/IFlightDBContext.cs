using Microsoft.EntityFrameworkCore;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.DBContext
{
    public interface IFlightDBContext
    {
        DbSet<AirportDetail> Airports { get; set; }
        DbSet<FlightDetail> Flights { get; set; }
        DbSet<Users> Users { get; set; }
    }
}