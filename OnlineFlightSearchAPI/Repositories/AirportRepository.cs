using System.Collections.Generic;
using System.Linq;
using OnlineFlightSearchAPI.DBModelsFolder;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        //private readonly IEnumerable<AirportDetail> _airportDetail = new List<AirportDetail>();

        private readonly IFlightDBContext _flightDBContext;
        
        public AirportRepository(IFlightDBContext flightDBContext)
        {
            _flightDBContext = flightDBContext;
        }

        public IEnumerable<AirportDetail> FetchAirportDetail(string airportCode)
        {
            return _flightDBContext.Airports.Where(x => x.AirportCode == airportCode)
                                        .Select(x => new AirportDetail
                                        {
                                            AirportCode = x.AirportCode
                                        });
        }
    }
}