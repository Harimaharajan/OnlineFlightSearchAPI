using Microsoft.AspNetCore.Mvc;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
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
        public List<FlightDetail> SearchFlightDetails(string startLocation, string endDestination, DateTime departureDate)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ISearchFlightService, SearchFlightService>();
            container.RegisterType<IAirportServices, AirportServices>();

            SearchFlightService searchFlightService = container.Resolve<SearchFlightService>();
            return searchFlightService.SearchFlightDetails(startLocation, endDestination, departureDate);
        }
    }
}
