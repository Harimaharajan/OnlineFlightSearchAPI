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

        private FlightDBContext _flightDBContext = new FlightDBContext();

        public List<FlightDetail> FetchFlightDetails(string startLocation, string endLocation, DateTime departureDate)
        {
            var searchResult = _flightDBContext.Flights.Where(x => (x.StartLocation == startLocation) &&
                                                        (x.EndLocation == endLocation) &&
                                                        (x.DepartureDate.Date == departureDate.Date))
                                                        .Select(x => new FlightDetail
                                                        {
                                                            FlightCode = x.FlightCode,
                                                            StartLocation = x.StartLocation,
                                                            Destination = x.EndLocation,
                                                            DepartureDate = x.DepartureDate,
                                                            TravelTime = x.Length.ToString(),
                                                            TicketFare = x.TicketFare,
                                                            AvailabilityCount = x.AvailabilityCount
                                                        }).ToList();
            if (searchResult.Count == 0)
            {
                throw new ValidationException(ValidationMessages.NoFlightsAvailable);
            }

            return searchResult;
        }
    }
}
