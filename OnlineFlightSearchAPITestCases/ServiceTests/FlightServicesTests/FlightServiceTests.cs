using Microsoft.Extensions.DependencyInjection;
using Moq;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories;
using OnlineFlightSearchAPI.Repositories.FlightRepository;
using OnlineFlightSearchAPI.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace OnlineFlightSearchAPITestCases
{
    public class FlightServiceTests
    {
        private ISearchFlightService searchFlightService;

        private readonly IAirportServices airportService;

        private readonly IAirportRepository airportRepository;

        private FlightRepositoryMock flightRepositoryMocking()
        {
            var mockFlightRepository = new FlightRepositoryMock().SetUp(new AirportServiceMock().Object);

            return mockFlightRepository;
        }

        private ISearchFlightService Initialize()
        {
            var flightRepositoryMock = new FlightRepositoryMock().SetUp(airportService);
            var airportServiceMock = new AirportServiceMock().SetUp(airportRepository);

            return new FlightService(flightRepositoryMock.Object, airportServiceMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SearchFlight_IfStartDestinationEmpty_ThrowsValidationException(string startLocation)
        {
            searchFlightService = Initialize();

            var expectedException = new ValidationException(ValidationMessages.StartLocationCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, "BUD", DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SearchFlight_IfDestinationNullOrEmpty_ThrowsValidationException(string endLocation)
        {
            searchFlightService = Initialize();

            var expectedException = new ValidationException(ValidationMessages.DestinationCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails("BUD", endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("ABC")]
        public void SearchFlight_IfStarLocationIsNotValid_ThrowsValidationException(string startLocation)
        {
            searchFlightService = Initialize();

            var expectedException = new ValidationException(ValidationMessages.InvalidStartLocation);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, "BUD", DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("XYZ")]
        public void SearchFlight_IfDestinationIsNotValid_ThrowsValidationException(string endLocation)
        {
            searchFlightService = Initialize();

            var expectedException = new ValidationException(ValidationMessages.InvalidDestination);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails("BUD", endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("BUD", "BUD")]
        [InlineData("LTN", "LTN")]
        public void SearchFlight_IfBothStartAndEndLocationAreSame_ThrowsValidationException(string startLocation, string endLocation)
        {
            searchFlightService = Initialize();

            var expectedException = new ValidationException(ValidationMessages.StartandEndLocationCannotBeSame);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Fact]
        public void SearchFlight_IfDepartureDateIsNotValid_ThrowsValidationException()
        {
            searchFlightService = Initialize();

            var expectedException = new ValidationException(ValidationMessages.InvalidDepartureDate);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails("BUD", "LTN", DateTime.UtcNow.AddDays(-1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public void SearchFlight_IfStartAndEndDestinationAndDepartureDateValid_ReturnsFlightDetails(string startDestination, string endDestination)
        {
            searchFlightService = Initialize();

            var actualResult = searchFlightService.SearchFlightDetails(startDestination, endDestination, DateTime.UtcNow.AddDays(2));

            Assert.IsType<List<FlightDetail>>(actualResult);
            Assert.True(actualResult.TrueForAll(x => x.StartLocation == startDestination));
            Assert.True(actualResult.TrueForAll(x => x.Destination == endDestination));
            Assert.True(actualResult.TrueForAll(x => x.DepartureDate.Date == (DateTime.UtcNow.AddDays(2).Date)));
        }

        [Theory]
        [InlineData("BUD", "IAD")]
        public void SearchFlight_IfNoMatchFoundForFlightSearch_ThrowsValidationException(string startLocation, string endLocation)
        {
            searchFlightService = Initialize();

            var expectedException = new ValidationException(ValidationMessages.NoFlightsAvailable);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.UtcNow.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }
    }
}
