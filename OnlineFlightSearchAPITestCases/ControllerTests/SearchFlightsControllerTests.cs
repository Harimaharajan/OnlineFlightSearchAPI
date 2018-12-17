using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Moq;
using OnlineFlightSearchAPI;
using OnlineFlightSearchAPI.Controllers;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.UnitTests;
using Xunit;

namespace OnlineFlightSearchAPITestCases.ControllerTests
{
    public class SearchFlightsControllerTests : IClassFixture<WebApplicationFactory<OnlineFlightSearchAPI.Startup>>
    {
        private readonly TestServer server;

        public SearchFlightsControllerTests()
        {
            server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public void SearchFlightDetails_ValidRequest_ReturnsHttpStatusOK200(string startLocation, string destination)
        {
            var mockSearchFlightService = new Mock<ISearchFlightService>();
            mockSearchFlightService.Setup(x => x.SearchFlightDetails(startLocation, destination, DateTime.UtcNow.Date.AddDays(1))).Returns(ExpectedFlightDetails());

            var searchFlightsController = new SearchFlightsController(mockSearchFlightService.Object);
            var actualResult = searchFlightsController.SearchFlightDetails(startLocation, destination, DateTime.UtcNow.Date.AddDays(1).ToString()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, actualResult.StatusCode);
        }

        [Theory]
        [InlineData("", "LTN")]
        [InlineData("", "")]
        public async Task SearchFlightDetails_InvalidStartandEndLocationRequest_ReturnsHttpStatusBadRequest400(string startLocation, string destination)
        {
            var client = server.CreateClient();
            var request = String.Format(TestConstants.ValidFlightSearchRequest, startLocation, destination, DateTime.UtcNow.Date.AddDays(1).ToString());

            var response = await client.GetAsync(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public async Task SearchFlightDetails_InvalidAPIRequest_ReturnsHttpStatusNotFound404(string startLocation, string destination)
        {
            var client = server.CreateClient();
            var request = String.Format(TestConstants.InvalidFlightSearchRequest, startLocation, destination, DateTime.UtcNow.Date.AddDays(1).ToString());

            var response = await client.GetAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        private List<FlightDetail> ExpectedFlightDetails()
        {
            List<string> validAirportCodes = new List<string> { "BUD", "LTN", "IAD" };

            var fixture = new Fixture();
            List<FlightDetail> flightDetails = fixture.Build<FlightDetail>()
                                         .With(x => x.StartLocation, validAirportCodes[0])
                                         .With(x => x.Destination, validAirportCodes[1])
                                         .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(1))
                                         .With(x => x.TravelTime, "2:35").CreateMany(4).ToList();

            return flightDetails;
        }
    }
}
