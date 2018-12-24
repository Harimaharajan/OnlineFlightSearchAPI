using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineFlightSearchAPI.Models
{
    public class AirportDetail
    {
        [Key]
        public string AirportCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime AirportTimeZone { get; set; }
    }
}
