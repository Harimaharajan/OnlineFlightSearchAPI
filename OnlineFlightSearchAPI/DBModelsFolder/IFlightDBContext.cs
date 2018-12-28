using Microsoft.EntityFrameworkCore;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.DBModelsFolder
{
    public interface IFlightDBContext
    {
        DbSet<AirportDetail> Airports { get; set; }
        DbSet<FlightDetail> Flights { get; set; }
        DbSet<UserLoginModel> Users { get; set; }
    }
}