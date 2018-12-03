using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFlightSearchAPI.Models
{
    public class FlightDetail
    {
        public FlightDetail(string flightCode, string startLocation, string destination, string departureDate, string travelTime, decimal ticketFare, int availabilityCount)
        {
            FlightCode = flightCode;
            StartLocation = startLocation;
            Destination = destination;
            DepartureDate = Convert.ToDateTime(departureDate);
            TravelTime= travelTime;
            TicketFare = ticketFare;
            AvailabilityCount = availabilityCount;
        }

        public string FlightCode { get; set; }

        public string StartLocation { get; set; }

        public string Destination { get; set; }

        public DateTime DepartureDate { get; set; }

        public string TravelTime { get; set; }

        public decimal TicketFare { get; set; }

        public int AvailabilityCount { get; set; }

    }
}
