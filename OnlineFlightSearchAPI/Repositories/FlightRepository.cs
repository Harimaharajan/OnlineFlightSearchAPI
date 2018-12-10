using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnlineFlightSearchAPI.Repositories.FlightRepository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IAirportServices _airportService;

        public List<FlightDetail> flightDetails { get; set; } = new List<FlightDetail>();

        public FlightRepository(IAirportServices airportService)
        {
            _airportService = airportService;
        }

        public List<FlightDetail> FetchFlightDetails(string startLocation, string endLocation, DateTime departureDate)
        {
            var searchResult = flightDetails.Where(x => (x.StartLocation == startLocation) &&
                                                        (x.Destination == endLocation) &&
                                                        (x.DepartureDate.Date == departureDate.Date))
                                                        .ToList();
            if (searchResult.Count == 0)
            {
                throw new ValidationException(ValidationMessages.NoFlightsAvailable);
            }

            return searchResult;
        }
    }
}
