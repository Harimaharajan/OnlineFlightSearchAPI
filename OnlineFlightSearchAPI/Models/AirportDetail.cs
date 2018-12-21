﻿using System;

namespace OnlineFlightSearchAPI.Models
{
    public class AirportDetail
    {
        public string AirportCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTimeKind AirportTimeZone { get; set; }
    }
}
