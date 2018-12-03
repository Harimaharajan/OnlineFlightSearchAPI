﻿using OnlineFlightSearchAPI.Models;
using System;
using System.Collections.Generic;

namespace OnlineFlightSearchAPI.FlightServices
{
    public interface ISearchFlightService
    {
        List<FlightDetail> SearchFlightDetails(string startLocation, string endLocation, DateTime departureDate);
    }
}