using System.Collections.Generic;
using System.Linq;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly IEnumerable<AirportDetail> _airportDetail = new List<AirportDetail>();

        public IEnumerable<AirportDetail> FetchAirportDetail(string airportCode)
        {
            return _airportDetail.Where(x => x.AirportCode == airportCode);  
        }
    }
}