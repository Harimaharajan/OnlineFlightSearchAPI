using System.Collections.Generic;
using System.Linq;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly List<AirportDetail> airportDetails = new List<AirportDetail>();

        public bool IsAirportValid(string airportCode)
        {
            var result = airportDetails.Where(x => x.AirportCode == airportCode).Any();
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}