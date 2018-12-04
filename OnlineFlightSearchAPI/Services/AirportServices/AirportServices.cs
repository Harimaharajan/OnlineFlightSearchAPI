using OnlineFlightSearchAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineFlightSearchAPI.FlightServices
{
    public class AirportServices : IAirportServices
    {
        public List<AirportDetail> airportDetails { get; set; } = new List<AirportDetail>();

        public AirportServices()
        {
            AirportDetail airportBUD = new AirportDetail("BUD", "Budapest", "Hungary", DateTime.UtcNow.AddHours(1).Kind);
            AirportDetail airportLTN = new AirportDetail("LTN", "London Luton", "UK", DateTime.UtcNow.Kind);
            AirportDetail airportIAD = new AirportDetail("IAD", "Washington", "USA", DateTime.UtcNow.AddHours(-5).Kind);
            airportDetails.Add(airportBUD);
            airportDetails.Add(airportLTN);
            airportDetails.Add(airportIAD);
        }

        public bool IsAirportValid(string airportCode)
        {
            if (!string.IsNullOrEmpty(airportCode))
            {
                var result = airportDetails.Where(x => x.AirportCode == airportCode).Any();
                if (result)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
