using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OnlineFlightSearchAPI.Controllers;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Repositories;
using OnlineFlightSearchAPI.Repositories.FlightRepository;
using System;
using System.Net;
using Xunit;

namespace OnlineFlightSearchAPITestCases.ControllerTests
{
    public class SearchFlightsControllerTests
    {
        private readonly SearchFlightsController searchFlightsController;

        public SearchFlightsControllerTests()
        {
            var services = new ServiceCollection();
            services.AddScoped<SearchFlightsController>();
            services.AddScoped<ISearchFlightService, FlightService>();
            services.AddScoped<IAirportServices, AirportServices>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IAirportRepository, AirportRepository>();
            var serviceProvider = services.BuildServiceProvider();

            searchFlightsController = serviceProvider.GetService<SearchFlightsController>();
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public void SearchFlight_IfAllSearchParametersAreValid_ReturnsStatusCodeOK200(string startLocation, string endLocation)
        {
            var actualResult = searchFlightsController.SearchFlight(startLocation, endLocation, DateTime.Now.AddDays(1)) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, actualResult.StatusCode);
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public void SearchFlight_IfDestinationNullOrEmpty_ThrowsValidationException(string startLocation, string endLocation)
        {
            var result = searchFlightsController.SearchFlight(startLocation, null, DateTime.Now.AddDays(1)) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
