using OnlineFlightSearchAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFlightSearchAPI.FlightServices
{
    public class AirportServices : IAirportServices
    {
        private List<AirportDetail> airportDetails = new List<AirportDetail>();

        public AirportServices()
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
            if(!string.IsNullOrEmpty(airportCode))
            {
                var result = airportDetails.Where(x => x.AirportCode == airportCode).Count();
                if (result >= 1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
