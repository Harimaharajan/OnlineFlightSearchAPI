using OnlineFlightSearchAPI.Models;
using System;
using System.Collections.Generic;

namespace OnlineFlightSearchAPI.Repositories.FlightRepository
{
    public interface IFlightRepository
    {
        List<FlightDetail> flightDetails { get; set; }

        List<FlightDetail> FetchFlightDetails(string startLocation, string endLocation, DateTime departureDate);
    }
}