using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using OnlineFlightSearchAPI;
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

        // Will Ideally return StatusCode 200 OK. Since No Data is present ValidationMessage is thrown hence the BadRequest
        [Theory]
        [InlineData("BUD", "LTN")]
        public async Task SearchFlightDetails_ValidRequest_ReturnsHttpStatusBadRequest400(string startLocation, string destination)
        {
            var client = server.CreateClient();
            var request = TestConstants.ValidFlightSearchRequest + 
                                        "?startLocation=" + startLocation +
                                        "&endDestination=" + destination + 
                                        "&departureDate=" + DateTime.UtcNow.Date.AddDays(1);
            var response = await client.GetAsync(request);

            // Retained this so that once data is available can ensure Success Code
            // response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("", "LTN")]
        [InlineData("", "")]
        public async Task SearchFlightDetails_InvalidStartandEndLocationRequest_ReturnsHttpStatusBadRequest400(string startLocation, string destination)
        {
            var client = server.CreateClient();
            var request = TestConstants.ValidFlightSearchRequest +
                                        "?startLocation=" + startLocation +
                                        "&endDestination=" + destination +
                                        "&departureDate=" + DateTime.UtcNow.Date.AddDays(1);

            var response = await client.GetAsync(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public async Task SearchFlightDetails_InvalidAPIRequest_ReturnsHttpStatusNotFound404(string startLocation, string destination)
        {
            var client = server.CreateClient();
            var request = TestConstants.InvalidFlightSearchRequest +
                                        "?startLocation=" + startLocation +
                                        "&endDestination=" + destination +
                                        "&departureDate=" + DateTime.UtcNow.Date.AddDays(1);

            var response = await client.GetAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
