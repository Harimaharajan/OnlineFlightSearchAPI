using OnlineFlightSearchAPI.Models;
using System.Collections.Generic;

namespace OnlineFlightSearchAPI.Repositories
{
    public interface IAirportRepository
    {
        List<AirportDetail> airportDetails { get; set; }
    }
}