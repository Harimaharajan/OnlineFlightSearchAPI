using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Moq;
using OnlineFlightSearchAPI;
using OnlineFlightSearchAPI.Controllers;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
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
        [InlineData("BUD", "LTN", "2018-12-12")]
        public async Task SearchFlightDetails_ValidRequest_ReturnsHttpStatusOK200(string startLocation, string destination, string departureDate)
        {
            var client = server.CreateClient();
            var url = $"/api/SearchFlights/SearchFlightDetails?startLocation={startLocation}&endDestination={destination}&departureDate={departureDate}";

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("BUD", "LTN", "2018-12-12")]
        public async Task SearchFlightDetails_InvalidAPIRequest_ReturnsHttpStatusNotFound404(string startLocation, string destination, string departureDate)
        {
            var client = server.CreateClient();
            var url = $"/api/SearchFlights/Search?startLocation={startLocation}&endDestination={destination}&departureDate={departureDate}";

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("BUD", "", "2018-12-12")]
        public async Task SearchFlightDetails_InValidSearchParameterRequest_ThrowsValidationException(string startLocation, string destination, string departureDate)
        {
            var client = server.CreateClient();
            var url = $"/api/SearchFlights/SearchFlightDetails?startLocation={startLocation}&endDestination={destination}&departureDate={departureDate}";

            var response = await Assert.ThrowsAsync<ValidationException>(() => client.GetAsync(url));

            Assert.IsType<ValidationException>(response);
        }
    }
}
