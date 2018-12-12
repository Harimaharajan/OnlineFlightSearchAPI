using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Repositories.FlightRepository
{
    public class FlightRepository : IFlightRepository
    {
        public List<FlightDetail> FlightDetails { get; set; } = new List<FlightDetail>();

        public List<FlightDetail> FetchFlightDetails(string startLocation, string endLocation, DateTime departureDate)
        {
            var searchResult = FlightDetails.Where(x => (x.StartLocation == startLocation) &&
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
