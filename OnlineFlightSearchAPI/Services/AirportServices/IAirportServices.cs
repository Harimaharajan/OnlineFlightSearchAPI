using System.Collections.Generic;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.FlightServices
{
    public interface IAirportServices
    {
        List<AirportDetail> airportDetails { get; set; }

        bool IsAirportValid(string airportCode);
    }
}