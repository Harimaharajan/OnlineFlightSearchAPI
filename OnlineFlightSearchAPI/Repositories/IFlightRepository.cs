using System;
using System.Collections.Generic;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Repositories.FlightRepository
{
    public interface IFlightRepository
    {
        List<FlightDetail> FlightDetails { get; }

        List<FlightDetail> FetchFlightDetails(string startLocation, string endLocation, DateTime departureDate);
    }
}