using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineFlightSearchAPI.FlightServices;

namespace OnlineFlightSearchAPI.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class SearchFlightsController : ControllerBase
    {
        private readonly ISearchFlightService _searchFlightService;

        public SearchFlightsController(ISearchFlightService searchFlightService)
        {
            _searchFlightService = searchFlightService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchFlightDetails(string startLocation, string endDestination, string departureDate)
        {
            try
            {
                var flightDetails = _searchFlightService.SearchFlightDetails(startLocation, endDestination, Convert.ToDateTime(departureDate));

                if (flightDetails == null)
                {
                    return NotFound();
                }

                return Ok(flightDetails);
            }
            catch (ValidationException ex)
            {
                if (ex.Message == ValidationMessages.StartLocationCannotBeEmpty ||
                    ex.Message == ValidationMessages.DestinationCannotBeEmpty)
                {
                    return BadRequest(ex.Message);
                }
                return NoContent();
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}