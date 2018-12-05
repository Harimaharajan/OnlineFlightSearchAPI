using System;
using System.Collections.Generic;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Repositories.FlightRepository
{
    public interface IFlightRepository
    {
        List<FlightDetail> flightDetails { get; set; }

        List<FlightDetail> FetchFlightDetails(string startLocation, string endLocation, DateTime departureDate);
    }
}