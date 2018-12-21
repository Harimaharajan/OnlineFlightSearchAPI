using System;
using System.Collections.Generic;

namespace OnlineFlightSearchAPI.DBModelsFolder
{
    public partial class Airports
    {
        public string AirportCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime? TimeZone { get; set; }
    }
}
