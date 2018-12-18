using System.Linq;
using OnlineFlightSearchAPI.Repositories;

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
            if (string.IsNullOrEmpty(airportCode))
            {
                return false;
            }

            if (_airportRepository.FetchAirportDetail(airportCode).Any())
            {
                return true;
            }

            return false;
        }
    }
}
