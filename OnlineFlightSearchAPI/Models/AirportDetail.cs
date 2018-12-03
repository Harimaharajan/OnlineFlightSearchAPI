using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFlightSearchAPI.Models
{
    public class AirportDetail
    {
        public AirportDetail(string airportCode, string city, string country, DateTimeKind timeZone)
        {
            AirportCode = airportCode;
            City = city;
            Country = country;
            TimeZone = timeZone;
        }

        public string AirportCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTimeKind TimeZone { get; set; }
    }
}
