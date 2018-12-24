using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using OnlineFlightSearchAPI.DBModelsFolder;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Repositories.FlightRepository
{
    public class FlightRepository : IFlightRepository
    {
        //private readonly List<FlightDetail> _flightDetails = new List<FlightDetail>();

        private readonly FlightDBContext _flightDBContext;

        public FlightRepository(FlightDBContext flightDBContext)
        {
            _flightDBContext = flightDBContext;
        }

        public List<FlightDetail> FetchFlightDetails(string startLocation, string endLocation, DateTime departureDate)
        {
            var searchResult = _flightDBContext.Flights.Where(x => (x.StartLocation == startLocation) &&
                                                        (x.EndLocation == endLocation) &&
                                                        (x.DepartureDate.Date == departureDate.Date)).ToList();
            if (searchResult.Count == 0)
            {
                throw new ValidationException(ValidationMessages.NoFlightsAvailable);
            }

            return searchResult;
        }
    }
}
