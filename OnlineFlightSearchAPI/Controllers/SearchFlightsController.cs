using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OnlineFlightSearchAPI.FlightServices;

namespace OnlineFlightSearchAPI.Controllers
{
    [Route("api/Flight/[action]")]
    [ApiController]
    public class SearchFlightsController : ControllerBase
    {
        private readonly ISearchFlightService _searchFlightService;

        public SearchFlightsController(ISearchFlightService searchFlightService)
        {
            _searchFlightService = searchFlightService;
        }

        [HttpGet]
        [ActionName("Search")]
        public IActionResult SearchFlightDetails([FromQuery]string startLocation, [FromQuery]string endDestination, [FromQuery]string departureDate)
        {
            try
            {
                var flightDetails = _searchFlightService.SearchFlightDetails(startLocation, endDestination, Convert.ToDateTime(departureDate));

                if (flightDetails == null)
                {
                    return NoContent();
                }

                return Ok(flightDetails);
            }
            catch (ValidationException validationException)
            {
                if (validationException.Message == ValidationMessages.NoFlightsAvailable)
                {
                    return NoContent();
                }

                return BadRequest(validationException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}