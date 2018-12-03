using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFlightSearchAPI.Models
{
    public class FlightDetail
    {
        public string FlightCode { get; set; }

        public string StartDestination { get; set; }

        public string EndDestination { get; set; }

        public DateTime DepartureDate { get; set; }

        public TimeSpan TravelTime { get; set; }

        public int TicketFare { get; set; }

        public int AvailabilityCount { get; set; }

    }
}
