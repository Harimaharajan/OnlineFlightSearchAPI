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

            var result = _airportRepository.FetchAirportDetail(airportCode);
            if (result.Where(s => string.Compare(s.AirportCode, airportCode, true) == 0).Count() > 0)
            {
                return true;
            }

            return false;
        }
    }
}
