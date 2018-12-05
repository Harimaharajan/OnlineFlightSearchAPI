using System;

namespace OnlineFlightSearchAPI.Models
{
    public class AirportDetail
    {
        public AirportDetail(string airportCode, string city, string country, DateTimeKind timeZone)
        {
            AirportCode = airportCode;
            City = city;
            Country = country;
            AirportTimeZone = timeZone;
        }

        public string AirportCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTimeKind AirportTimeZone { get; set; }
    }
}
