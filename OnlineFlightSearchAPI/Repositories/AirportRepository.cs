using OnlineFlightSearchAPI.Models;
using System;
using System.Collections.Generic;

namespace OnlineFlightSearchAPI.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        public List<AirportDetail> airportDetails { get; set; } = new List<AirportDetail>();

        public AirportRepository()
        {
            AirportDetail airportBUD = new AirportDetail("BUD", "Budapest", "Hungary", DateTime.UtcNow.AddHours(1).Kind);
            AirportDetail airportLTN = new AirportDetail("LTN", "London Luton", "UK", DateTime.UtcNow.Kind);
            AirportDetail airportIAD = new AirportDetail("IAD", "Washington", "USA", DateTime.UtcNow.AddHours(-5).Kind);
            airportDetails.Add(airportBUD);
            airportDetails.Add(airportLTN);
            airportDetails.Add(airportIAD);
        }
    }
}
