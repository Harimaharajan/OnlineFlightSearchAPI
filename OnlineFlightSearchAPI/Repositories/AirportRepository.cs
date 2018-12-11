using System.Collections.Generic;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        public List<AirportDetail> airportDetails { get; set; } = new List<AirportDetail>();
    }
}
