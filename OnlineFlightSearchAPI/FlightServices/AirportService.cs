using OnlineFlightSearchAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFlightSearchAPI.FlightServices
{
    public class AirportService : IAirportService
    {
        private List<AirportDetail> airportDetails = new List<AirportDetail>();

        public AirportService()
        {
            AirportDetail airportBUD = new AirportDetail("BUD", "Budapest", "Hungary", DateTime.UtcNow.AddHours(1).Kind);
            AirportDetail airportLTN = new AirportDetail("LTN", "London Luton", "UK", DateTime.UtcNow.Kind);
            AirportDetail airportIAD = new AirportDetail("IAD", "Washington", "USA", DateTime.UtcNow.AddHours(-5).Kind);
            airportDetails.Add(airportBUD);
            airportDetails.Add(airportLTN);
            airportDetails.Add(airportIAD);
        }

        public bool CheckIfAirportIsValid(string airportCode)
        {
            var result = airportDetails.Where(x => x.AirportCode == airportCode).Count();
            if (result >= 1)
            {
                return true;
            }

            return false;
        }
    }
}
