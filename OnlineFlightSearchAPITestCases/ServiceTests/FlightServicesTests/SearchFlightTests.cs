using Microsoft.Extensions.DependencyInjection;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories;
using OnlineFlightSearchAPI.Repositories.FlightRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace OnlineFlightSearchAPITestCases
{
    public class SearchFlightTests
    {
        private readonly ISearchFlightService searchFlightService;

        public SearchFlightTests()
        {
            var services = new ServiceCollection();
            services.AddScoped<ISearchFlightService, FlightService>();
            services.AddScoped<IAirportServices, AirportServices>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IAirportRepository, AirportRepository>();
            var serviceProvider = services.BuildServiceProvider();

            searchFlightService = serviceProvider.GetService<ISearchFlightService>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SearchFlight_IfStartDestinationEmpty_ThrowsValidationException(string startLocation)
        {
            var expectedException = new ValidationException(ValidationMessages.StartLocationCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, "BUD", DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SearchFlight_IfDestinationNullOrEmpty_ThrowsValidationException(string endLocation)
        {
            var expectedException = new ValidationException(ValidationMessages.DestinationCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails("BUD", endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("ABC")]
        public void SearchFlight_IfStarLocationIsNotValid_ThrowsValidationException(string startLocation)
        {
            var expectedException = new ValidationException(ValidationMessages.InvalidStartLocation);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, "BUD", DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("XYZ")]
        public void SearchFlight_IfDestinationIsNotValid_ThrowsValidationException(string endLocation)
        {
            var expectedException = new ValidationException(ValidationMessages.InvalidDestination);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails("BUD", endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("BUD", "BUD")]
        [InlineData("LTN", "LTN")]
        public void SearchFlight_IfBothStartAndEndLocationAreSame_ThrowsValidationException(string startLocation, string endLocation)
        {
            var expectedException = new ValidationException(ValidationMessages.StartandEndLocationCannotBeSame);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Fact]
        public void SearchFlight_IfDepartureDateIsNotValid_ThrowsValidationException()
        {
            var expectedException = new ValidationException(ValidationMessages.InvalidDepartureDate);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails("BUD", "LTN", DateTime.UtcNow.AddDays(-1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public void SearchFlight_IfStartAndEndDestinationAndDepartureDateValid_ReturnsFlightDetails(string startDestination, string endDestination)
        {
            var actualResult = searchFlightService.SearchFlightDetails(startDestination, endDestination, DateTime.UtcNow.AddDays(1));

            Assert.IsType<List<FlightDetail>>(actualResult);
            Assert.True(actualResult.TrueForAll(x => x.StartLocation == startDestination));
            Assert.True(actualResult.TrueForAll(x => x.Destination == endDestination));
            Assert.True(actualResult.TrueForAll(x => x.DepartureDate.Date == (DateTime.UtcNow.AddDays(1).Date)));
        }

        [Theory]
        [InlineData("BUD", "IAD")]
        public void SearchFlight_IfNoMatchFoundForFlightSearch_ThrowsValidationException(string startLocation, string endLocation)
        {
            var expectedException = new ValidationException(ValidationMessages.NoFlightsAvailable);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.UtcNow.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }
    }
}
