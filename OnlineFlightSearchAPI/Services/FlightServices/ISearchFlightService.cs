﻿using System;
using System.Collections.Generic;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.FlightServices
{
    public interface ISearchFlightService
    {
        List<FlightDetail> SearchFlightDetails(string startLocation, string endLocation, DateTime departureDate);
    }
}