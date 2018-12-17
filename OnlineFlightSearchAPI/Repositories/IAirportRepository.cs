using OnlineFlightSearchAPI.Models;
using System.Collections.Generic;

namespace OnlineFlightSearchAPI.Repositories
{
    public interface IAirportRepository
    {
        IEnumerable<AirportDetail> FetchAirportDetail(string airportCode);
    }
}