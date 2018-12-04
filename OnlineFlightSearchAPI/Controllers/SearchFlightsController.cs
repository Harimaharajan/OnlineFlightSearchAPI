using Microsoft.AspNetCore.Mvc;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Unity;

namespace OnlineFlightSearchAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchFlightsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<FlightDetail>> SearchFlightDetails(string startLocation, string endDestination, DateTime departureDate)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ISearchFlightService, SearchFlightService>();
            container.RegisterType<IAirportServices, AirportServices>();

            SearchFlightService searchFlightService = container.Resolve<SearchFlightService>();
            List<FlightDetail> flightDetails = searchFlightService.SearchFlightDetails(startLocation, endDestination, departureDate);

            return flightDetails;
        }

        public ObjectResult SearchFlight(string startLocation, string endDestination, DateTime departureDate)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ISearchFlightService, SearchFlightService>();
            container.RegisterType<IAirportServices, AirportServices>();
            SearchFlightService searchFlightService = container.Resolve<SearchFlightService>();
            List<FlightDetail> flightDetails = searchFlightService.SearchFlightDetails(startLocation, endDestination, departureDate);

            if (flightDetails != null)
            {
                return Ok(HttpStatusCode.OK);
            }
            return Ok(HttpStatusCode.BadRequest);
        }
    }
}
