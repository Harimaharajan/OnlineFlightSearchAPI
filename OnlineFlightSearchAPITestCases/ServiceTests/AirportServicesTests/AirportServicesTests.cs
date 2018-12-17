using System.Collections.Generic;
using AutoFixture;
using Moq;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories;
using Xunit;

namespace OnlineFlightSearchAPITestCases
{
    public class AirportServicesTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void IsAirportValid_IfAirportCodeIsNullOrEmpty_ReturnsFalse(string airportCode)
        {
            var airportService = new AirportServices(new Mock<IAirportRepository>().Object);

            var result = airportService.IsAirportValid(airportCode);

            Assert.False(result, "Airport Code is not valid");
        }

        [Theory]
        [InlineData("BUD")]
        [InlineData("IAD")]
        public void IsAirportValid_IfAirportCodeIsValid_ReturnsTrue(string airportCode)
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            mockAirportRepository.Setup(x => x.FetchAirportDetail(airportCode)).Returns(ExpectedAirportDetail(airportCode));
            var airportService = new AirportServices(mockAirportRepository.Object);

            var actual = airportService.IsAirportValid(airportCode);

            Assert.True(actual);
        }

        [Theory]
        [InlineData("XYZ")]
        public void IsAirportValid_IfAirportCodeIsNotValid_ReturnsFalse(string airportCode)
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            mockAirportRepository.Setup(x => x.FetchAirportDetail(airportCode)).Returns<IEnumerable<AirportDetail>>(null);
            var airportService = new AirportServices(mockAirportRepository.Object);

            var result = airportService.IsAirportValid(airportCode);

            Assert.False(result);
        }

        private List<AirportDetail> ExpectedAirportDetail(string airportCode)
        {
            var fixture = new Fixture();
            List<AirportDetail> airportDetails = new List<AirportDetail>{
                                                         fixture.Build<AirportDetail>()
                                                        .With(x => x.AirportCode, airportCode)
                                                        .Create()
                                                     };

            return airportDetails;
        }
    }
}
