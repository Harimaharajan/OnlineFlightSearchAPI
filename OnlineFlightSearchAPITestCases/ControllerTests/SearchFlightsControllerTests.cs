using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using OnlineFlightSearchAPI;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
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
        [InlineData("/api/SearchFlights/SearchFlightDetails?startLocation=BUD&endDestination=LTN&departureDate=2018-12-07")]
        public async Task SearchController_ValidRequest_ReturnsHttpStatusOK200(string url)
        {
            var client = server.CreateClient();

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/SearchFlights/Search?startLocation=BUD&endDestination=LTN&departureDate=2018-12-06")]
        public async Task SearchController_InValidActionRequest_ReturnsHttpStatusNotFound404(string url)
        {
            var client = server.CreateClient();

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/SearchFlights/SearchFlightDetails?startLocation=BUD&endDestination=XYZ&departureDate=2018-12-07")]
        public async Task SearchController_InValidSearchParameterRequest_ThrowsValidationException(string url)
        {
            var client = server.CreateClient();

            var response = await Assert.ThrowsAsync<ValidationException>(() => client.GetAsync(url));

            Assert.IsType<ValidationException>(response);
        }
    }
}
