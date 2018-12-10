using OnlineFlightSearchAPI.Models;
using System.Collections.Generic;

namespace OnlineFlightSearchAPI.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        public List<AirportDetail> airportDetails { get; set; } = new List<AirportDetail>();

        public AirportRepository()
        {

        }
    }
}
