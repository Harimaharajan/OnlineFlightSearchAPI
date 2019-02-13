using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineFlightSearchAPI.Models
{
    public class FlightDetail
    {
        [Key]
        public string FlightCode { get; set; }

        public string StartLocation { get; set; }

        public string EndLocation { get; set; }

        public DateTime DepartureDate { get; set; }

        public decimal Length { get; set; }

        public decimal TicketFare { get; set; }

        public int AvailabilityCount { get; set; }

    }
}
