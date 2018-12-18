using System.Collections.Generic;
using System.Linq;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly IEnumerable<AirportDetail> airportDetail = new List<AirportDetail>();

        public IEnumerable<AirportDetail> FetchAirportDetail(string airportCode)
        {
            if (airportDetail.Any())
            {
                return airportDetail.Where(x => x.AirportCode == airportCode);
            }
            else
            {
                return airportDetail;
            }
        }
    }
}