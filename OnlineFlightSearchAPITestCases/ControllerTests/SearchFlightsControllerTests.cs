using Microsoft.AspNetCore.Mvc;
using OnlineFlightSearchAPI.Controllers;
using OnlineFlightSearchAPI.FlightServices;
using System;
using System.Net;
using Unity;
using Xunit;

namespace OnlineFlightSearchAPITestCases.ControllerTests
{
    public class SearchFlightsControllerTests
    {
        private IUnityContainer Initialize()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<SearchFlightsController>();
            container.RegisterType<ISearchFlightService, SearchFlightService>();
            container.RegisterType<IAirportServices, AirportServices>();
            return container;
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public void SearchFlight_IfAllSearchParametersAreValid_ReturnsStatusCodeOK200(string startLocation, string endLocation)
        {
            IUnityContainer container = Initialize();
            var searchFlightController = container.Resolve<SearchFlightsController>();
            var actualResult = searchFlightController.SearchFlight(startLocation, endLocation, DateTime.Now.AddDays(1)) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, actualResult.StatusCode);
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public void SearchFlight_IfDestinationNullOrEmpty_ThrowsValidationException(string startLocation, string endLocation)
        {
            IUnityContainer container = Initialize();
            var searchFlightController = container.Resolve<SearchFlightsController>();
            var result = searchFlightController.SearchFlight(startLocation, null, DateTime.Now.AddDays(1)) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
