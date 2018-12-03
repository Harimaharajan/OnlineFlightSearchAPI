using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFlightSearchAPI.Models
{
    public class FlightDetail
    {
        public string FlightCode { get; set; }

        public string StartLocation { get; set; }

        public string EndLocation { get; set; }

        public DateTime DepartureDate { get; set; }

        public TimeSpan TravelTime { get; set; }

        public int TicketFare { get; set; }

        public int AvailabilityCount { get; set; }

    }
}
