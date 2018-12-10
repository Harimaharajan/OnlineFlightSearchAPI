using Microsoft.Extensions.DependencyInjection;
using Moq;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories;
using OnlineFlightSearchAPI.UnitTests.Mocks;
using System;
using System.Collections.Generic;
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
            var mockAirportRepository = new AirportRepositoryMock().SetUp();
            var airportService = new AirportServices(mockAirportRepository.Object);

            var result = airportService.IsAirportValid(airportCode);

            Assert.False(result, "Airport Code is not valid");
        }

        [Theory]
        [InlineData("BUD")]
        [InlineData("IAD")]
        public void IsAirportValid_IfAirportCodeIsValid_ReturnsTrue(string airportCode)
        {
            var mockAirportRepository = new AirportRepositoryMock().SetUp();
            var airportService = new AirportServices(mockAirportRepository.Object);

            var actual = airportService.IsAirportValid(airportCode);

            Assert.True(actual);
        }

        [Theory]
        [InlineData("XYZ")]
        public void IsAirportValid_IfAirportCodeIsNotValid_ReturnsFalse(string airportCode)
        {
            var mockAirportRepository = new AirportRepositoryMock().SetUp();
            var airportService = new AirportServices(mockAirportRepository.Object);

            var result = airportService.IsAirportValid(airportCode);

            Assert.False(result);
        }
    }
}
