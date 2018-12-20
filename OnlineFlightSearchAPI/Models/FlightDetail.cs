using System;

namespace OnlineFlightSearchAPI.Models
{
    public class FlightDetail
    {
        public string FlightCode { get; set; }

        public string StartLocation { get; set; }

        public string Destination { get; set; }

        public DateTime DepartureDate { get; set; }

        public string TravelTime { get; set; }

        public decimal TicketFare { get; set; }

        public int AvailabilityCount { get; set; }

    }
}
