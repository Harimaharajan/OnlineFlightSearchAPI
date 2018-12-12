using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using OnlineFlightSearchAPI;
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
        public async Task SearchFlightDetails_ValidRequest_ReturnsHttpStatusNoContent204(string startLocation, string destination)
        {
            var client = server.CreateClient();
            var request = $"/api/SearchFlightDetails?startLocation={startLocation}&endDestination={destination}&departureDate={DateTime.UtcNow.Date.AddDays(1).ToString("dd-MM-yyyy")}";
            var response = await client.GetAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Theory]
        [InlineData("", "LTN")]
        [InlineData("", "")]
        public async Task SearchFlightDetails_InvalidStartandEndLocationRequest_ReturnsHttpStatusBadRequest400(string startLocation, string destination)
        {
            var client = server.CreateClient();
            var request = $"/api/SearchFlightDetails?startLocation={startLocation}&endDestination={destination}&departureDate={DateTime.UtcNow.Date.AddDays(1)}";

            var response = await client.GetAsync(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Theory]
        [InlineData("BUD", "LTN")]
        public async Task SearchFlightDetails_InvalidAPIRequest_ReturnsHttpStatusNotFound404(string startLocation, string destination)
        {
            var client = server.CreateClient();
            var url = $"/api/Search?startLocation={startLocation}&endDestination={destination}&departureDate={DateTime.UtcNow.Date.AddDays(1)}";

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
