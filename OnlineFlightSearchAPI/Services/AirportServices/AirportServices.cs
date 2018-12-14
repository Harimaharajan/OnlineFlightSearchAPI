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
            if (!string.IsNullOrEmpty(airportCode))
            {
                var result = _airportRepository.IsAirportValid(airportCode);
                return result;
            }

            return false;
        }
    }
}
