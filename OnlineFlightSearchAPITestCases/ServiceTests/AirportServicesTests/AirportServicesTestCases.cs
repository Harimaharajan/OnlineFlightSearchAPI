using Microsoft.Extensions.DependencyInjection;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Repositories;
using Xunit;

namespace OnlineFlightSearchAPITestCases
{
    public class AirportServicesTestCases
    {
        private readonly IAirportServices airportServices;

        public AirportServicesTestCases()
        {
            var services = new ServiceCollection();
            services.AddScoped<IAirportServices, AirportServices>();
            services.AddScoped<IAirportRepository, AirportRepository>();
            var serviceProvider = services.BuildServiceProvider();

            airportServices = serviceProvider.GetService<IAirportServices>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void IsAirportValid_IfAirportCodeIsNullOrEmpty_ReturnsFalse(string airportCode)
        {
            var result = airportServices.IsAirportValid(airportCode);

            Assert.False(result);
        }

        [Theory]
        [InlineData("BUD")]
        [InlineData("IAD")]
        public void IsAirportValid_IfAirportCodeIsValid_ReturnsTrue(string airportCode)
        {
            var result = airportServices.IsAirportValid(airportCode);

            Assert.True(result);
        }

        [Theory]
        [InlineData("XYZ")]
        public void IsAirportValid_IfAirportCodeIsNotValid_ReturnsFalse(string airportCode)
        {
            var result = airportServices.IsAirportValid(airportCode);

            Assert.False(result);
        }
    }
}
