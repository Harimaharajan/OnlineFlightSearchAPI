using Microsoft.AspNetCore.Mvc;
using OnlineFlightSearchAPI.FlightServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace OnlineFlightSearchAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchFlightsController
    {
        [HttpGet]
        public bool SearchFlightDetails(string startLocation, string endDestination, DateTime departureDate)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ISearchFlightService, SearchFlightService>();
            container.RegisterType<IAirportService, AirportService>();

            SearchFlightService searchFlightService = container.Resolve<SearchFlightService>();
            return searchFlightService.SearchFlightDetails(startLocation, endDestination, departureDate);
        }
    }
}
