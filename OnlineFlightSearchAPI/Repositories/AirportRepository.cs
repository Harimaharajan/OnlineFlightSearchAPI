using OnlineFlightSearchAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineFlightSearchAPI.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly List<AirportDetail> airportDetails = new List<AirportDetail>();

        public List<AirportDetail> FetchAirportDetail(string airportCode)
        {
            if (airportDetails != null)
            {
                return airportDetails.Where(x => x.AirportCode == airportCode).ToList();
            }
            else
            {
                return null;
            }
        }
    }
}