using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFlightSearchAPI.Models
{
    public class AirportDetails
    {
        //code;city;country;timezone
        public string AirportCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public TimeZoneInfo TimeZone { get; set; }
    }
}
