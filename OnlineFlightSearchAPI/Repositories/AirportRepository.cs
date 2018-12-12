using System.Collections.Generic;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        public List<AirportDetail> AirportDetails { get; set; } = new List<AirportDetail>();
    }
}
