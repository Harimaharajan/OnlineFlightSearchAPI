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
        private readonly List<FlightDetail> flightDetails = new List<FlightDetail>();

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
