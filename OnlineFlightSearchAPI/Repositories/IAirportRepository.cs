using System.Collections.Generic;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Repositories
{
    public interface IAirportRepository
    {
        List<AirportDetail> AirportDetails { get; }
    }
}