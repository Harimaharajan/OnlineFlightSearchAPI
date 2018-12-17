using OnlineFlightSearchAPI.Repositories;
using System.Linq;

namespace OnlineFlightSearchAPI.FlightServices
{
    public class AirportServices : IAirportServices
    {
        private readonly IAirportRepository _airportRepository;

        public AirportServices(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public bool IsAirportValid(string airportCode)
        {
            if (!string.IsNullOrEmpty(airportCode))
            {
                var result = _airportRepository.FetchAirportDetail(airportCode);
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }
}
