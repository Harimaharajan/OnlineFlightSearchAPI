using System;
using System.Collections.Generic;

namespace OnlineFlightSearchAPI.DBModelsFolder
{
    public partial class Flights
    {
        public string FlightCode { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime DepartureDate { get; set; }
        public decimal? Length { get; set; }
        public int TicketFare { get; set; }
        public int AvailabilityCount { get; set; }
    }
}
