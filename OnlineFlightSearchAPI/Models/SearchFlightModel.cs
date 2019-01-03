using System;

namespace OnlineFlightSearchAPI.Models
{
    public class SearchFlightModel
    {
        public string StartLocation { get; set; }

        public string EndLocation { get; set; }

        public DateTime DepartureDate { get; set; }
    }
}
