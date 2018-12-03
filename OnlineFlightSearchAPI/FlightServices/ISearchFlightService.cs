using System;

namespace OnlineFlightSearchAPI.FlightServices
{
    public interface ISearchFlightService
    {
        bool SearchFlightDetails(string startLocation, string endLocation, DateTime departureDate);
    }
}