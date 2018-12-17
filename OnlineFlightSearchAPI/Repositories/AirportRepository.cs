using System.Linq;
using OnlineFlightSearchAPI.Models;
using System.Collections.Generic;

namespace OnlineFlightSearchAPI.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly List<AirportDetail> airportDetail = new List<AirportDetail>();

        public IEnumerable<AirportDetail> FetchAirportDetail(string airportCode)
        {
            if (airportDetail.Count != 0)
            {
                return airportDetail.Where(x => x.AirportCode == airportCode);
            }
            else
            {
                return null;
            }
        }
    }
}