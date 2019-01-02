using System;
using System.Collections.Generic;
using System.Linq;
using OnlineFlightSearchAPI.DBContext;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Repositories.FlightRepository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IFlightDBContext _flightDBContext;

        public FlightRepository(IFlightDBContext flightDBContext)
        {
            _flightDBContext = flightDBContext;
        }

        public List<FlightDetail> FetchFlightDetails(string startLocation, string endLocation, DateTime departureDate)
        {
            return _flightDBContext.Flights.Where(x => (x.StartLocation == startLocation) &&
                                                        (x.EndLocation == endLocation) &&
                                                        (x.DepartureDate.Date == departureDate.Date)).ToList();
        }
    }
}
