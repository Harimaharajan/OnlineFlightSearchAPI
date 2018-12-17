using System.Linq;
using OnlineFlightSearchAPI.Models;
using System.Collections.Generic;

namespace OnlineFlightSearchAPI.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly List<AirportDetail> airportDetails = new List<AirportDetail>();

        public bool IsAirportValid(string airportCode)
        {
            if (airportDetails != null)
            {
                return airportDetails.Where(x => x.AirportCode == airportCode).Any();
            }
            else
            {
                return false;
            }
        }
    }
}