using AutoFixture;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Unity;
using Xunit;

namespace OnlineFlightSearchAPITestCases
{
    public class SearchFlightTests
    {
        private IUnityContainer Initialize()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ISearchFlightService, SearchFlightService>();
            container.RegisterType<IAirportServices, AirportServices>();
            return container;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SearchFlight_IfStartDestinationEmpty_ReturnsValidationException(string startLocation)
        {
            IUnityContainer container = Initialize();
            SearchFlightService searchFlightService = container.Resolve<SearchFlightService>();
            var expectedException = new ValidationException(ValidationMessages.StartLocationCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, "BUD", DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SearchFlight_IfDestinationNullOrEmpty_ReturnsValidationException(string endLocation)
        {
            IUnityContainer container = Initialize();
            SearchFlightService searchFlightService = container.Resolve<SearchFlightService>();
            var expectedException = new ValidationException(ValidationMessages.DestinationCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails("BUD", endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("ABC")]
        public void SearchFlight_IfStarLocationIsNotValid_ReturnsValidationException(string startLocation)
        {
            IUnityContainer container = Initialize();
            SearchFlightService searchFlightService = container.Resolve<SearchFlightService>();
            var expectedException = new ValidationException(ValidationMessages.InvalidStartLocation);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, "BUD", DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("XYZ")]
        public void SearchFlight_IfDestinationIsNotValid_ReturnsValidationException(string endLocation)
        {
            IUnityContainer container = Initialize();
            SearchFlightService searchFlightService = container.Resolve<SearchFlightService>();
            var expectedException = new ValidationException(ValidationMessages.InvalidDestination);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails("BUD", endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("BUD","BUD")]
        [InlineData("LTN", "LTN")]
        public void SearchFlight_IfBothStartAndEndLocationAreSame_ReturnsValidationException(string startLocation,string endLocation)
        {
            IUnityContainer container = Initialize();
            SearchFlightService searchFlightService = container.Resolve<SearchFlightService>();
            var expectedException = new ValidationException(ValidationMessages.StartandEndLocationCannotBeSame);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Fact]
        public void SearchFlight_IfDepartureDateIsNotValid_ReturnsValidationException()
        {
            IUnityContainer container = Initialize();
            SearchFlightService searchFlightService = container.Resolve<SearchFlightService>();

            var expectedException = new ValidationException(ValidationMessages.InvalidDepartureDate);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails("BUD", "LTN", DateTime.UtcNow.AddDays(-1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public void SearchFlight_IfStartAndEndDestinationAndDepartureDateValid_ReturnsFlightDetails(string startDestination, string endDestination)
        {
            IUnityContainer container = Initialize();
            SearchFlightService searchFlightService = container.Resolve<SearchFlightService>();

            var actualResult = searchFlightService.SearchFlightDetails(startDestination, endDestination, DateTime.UtcNow.AddDays(2));

            Assert.IsType<List<FlightDetail>>(actualResult);
        }

        [Theory]
        [InlineData("BUD", "IAD")]
        public void SearchFlight_IfNoMatchFoundForFlightSearch_ReturnsValidationException(string startLocation,string endLocation)
        {
            IUnityContainer container = Initialize();
            SearchFlightService searchFlightService = container.Resolve<SearchFlightService>();

            var expectedException = new ValidationException(ValidationMessages.NoFlightsAvailable);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.UtcNow.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }
    }
}
