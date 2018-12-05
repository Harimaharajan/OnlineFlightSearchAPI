﻿using Microsoft.AspNetCore.Mvc;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using System;
using System.Collections.Generic;
using System.Net;

namespace OnlineFlightSearchAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchFlightsController : ControllerBase
    {
        private readonly ISearchFlightService _searchFlightService;

        public SearchFlightsController(ISearchFlightService searchFlightService)
        {
            _searchFlightService = searchFlightService;
        }

        [HttpGet]
        public ActionResult<List<FlightDetail>> SearchFlightDetails(string startLocation, string endDestination, DateTime departureDate)
        {
            List<FlightDetail> flightDetails = _searchFlightService.SearchFlightDetails(startLocation, endDestination, departureDate);
            return flightDetails;
        }

        public ObjectResult SearchFlight(string startLocation, string endDestination, DateTime departureDate)
        {
            List<FlightDetail> flightDetails = _searchFlightService.SearchFlightDetails(startLocation, endDestination, departureDate);

            if (flightDetails != null)
            {
                return Ok(HttpStatusCode.OK);
            }
            return Ok(HttpStatusCode.BadRequest);
        }
    }
}
