using System.Collections.Generic;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.FlightServices
{
    public interface IAirportServices
    {
        bool IsAirportValid(string airportCode);
    }
}