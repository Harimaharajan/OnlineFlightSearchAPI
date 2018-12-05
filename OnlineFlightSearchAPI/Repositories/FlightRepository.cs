﻿using OnlineFlightSearchAPI.FlightServices;
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

            FlightDetail flight1 = new FlightDetail("CM 2001", "BUD", "LTN", "2018-12-06 10:00", "2:35", 80, 20);
            FlightDetail flight2 = new FlightDetail("CM 2002", "BUD", "LTN", "2018-12-06 17:00", "10:00", 100, 19);
            FlightDetail flight3 = new FlightDetail("CM 2003", "BUD", "LTN", "2018-12-06 18:00", "3:35", 80, 10);
            FlightDetail flight4 = new FlightDetail("CM 2004", "LTN", "IAD", "2018-12-06 06:00", "2:05", 200, 2);
            FlightDetail flight5 = new FlightDetail("CM 2005", "LTN", "BUD", "2018-12-06 05:00", "2:00", 75, 60);
            FlightDetail flight6 = new FlightDetail("CM 2006", "LTN", "IAD", "2018-12-06 08:00", "12:35", 45, 20);
            FlightDetail flight7 = new FlightDetail("CM 2007", "IAD", "LTN", "2018-12-06 13:00", "12:30", 40, 33);
            FlightDetail flight8 = new FlightDetail("CM 2008", "LTN", "BUD", "2018-12-06 09:00", "2:05", 78, 0);
            FlightDetail flight9 = new FlightDetail("CM 2009", "BUD", "LTN", "2018-12-06 10:00", "2:00", 90, 0);
            FlightDetail flight10 = new FlightDetail("CM 2010", "IAD", "LTN", "2018-12-06 2:00", "14:35", 190, 1);

            flightDetails.Add(flight1);
            flightDetails.Add(flight2);
            flightDetails.Add(flight3);
            flightDetails.Add(flight4);
            flightDetails.Add(flight5);
            flightDetails.Add(flight6);
            flightDetails.Add(flight7);
            flightDetails.Add(flight8);
            flightDetails.Add(flight9);
            flightDetails.Add(flight10);
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
