using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Moq;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories.FlightRepository;
using OnlineFlightSearchAPI.Validator;
using Xunit;
using ValidationException = FluentValidation.ValidationException;

namespace OnlineFlightSearchAPITestCases
{
    public class FlightServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SearchFlight_IfStartDestinationNullOrEmpty_ThrowsValidationException(string startLocation)
        {
            var mockAirportService = Mock.Of<IAirportServices>();
            Mock.Get(mockAirportService)
                .Setup(x => x.IsAirportValid(startLocation))
                .Returns(false);

            var searchFlightService = new FlightService(Mock.Of<IFlightRepository>(), new FlightValidator(mockAirportService));

            var expectedException = new ValidationException(ValidationMessages.StartLocationCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, "BUD", DateTime.UtcNow.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("BUD", null)]
        [InlineData("BUD", "")]
        public void SearchFlight_IfDestinationNullOrEmpty_ThrowsValidationException(string startLocation, string endLocation)
        {
            var mockAirportService = Mock.Of<IAirportServices>();
            Mock.Get(mockAirportService)
                .Setup(x => x.IsAirportValid(startLocation)).Returns(true);
            Mock.Get(mockAirportService)
                .Setup(x => x.IsAirportValid(endLocation)).Returns(false);

            var searchFlightService = new FlightService(Mock.Of<IFlightRepository>(), new FlightValidator(mockAirportService));

            var expectedException = new ValidationException(ValidationMessages.DestinationCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.UtcNow.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("ABC", "BUD")]
        public void SearchFlight_IfStartLocationIsNotValid_ThrowsValidationException(string startLocation, string endLocation)
        {
            var mockAirportService = Mock.Of<IAirportServices>();
            Mock.Get(mockAirportService)
                .Setup(x => x.IsAirportValid(startLocation)).Returns(false);
            Mock.Get(mockAirportService)
                .Setup(x => x.IsAirportValid(endLocation)).Returns(true);

            var searchFlightService = new FlightService(Mock.Of<IFlightRepository>(), new FlightValidator(mockAirportService));

            var expectedException = new ValidationException(ValidationMessages.InvalidStartLocation);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.UtcNow.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("BUD", "XYZ")]
        public void SearchFlight_IfDestinationIsNotValid_ThrowsValidationException(string startLocation, string endLocation)
        {
            var mockAirportService = Mock.Of<IAirportServices>();
            Mock.Get(mockAirportService)
                .Setup(x => x.IsAirportValid(startLocation)).Returns(true);
            Mock.Get(mockAirportService)
                .Setup(x => x.IsAirportValid(endLocation)).Returns(false);

            var searchFlightService = new FlightService(Mock.Of<IFlightRepository>(), new FlightValidator(mockAirportService));

            var expectedException = new ValidationException(ValidationMessages.InvalidDestination);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.UtcNow.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("BUD", "BUD")]
        [InlineData("LTN", "LTN")]
        public void SearchFlight_IfBothStartAndEndLocationAreSame_ThrowsValidationException(string startLocation, string endLocation)
        {
            var mockAirportService = Mock.Of<IAirportServices>();
            Mock.Get(mockAirportService)
                .Setup(x => x.IsAirportValid(startLocation)).Returns(true);
            Mock.Get(mockAirportService)
                .Setup(x => x.IsAirportValid(endLocation)).Returns(true);

            var searchFlightService = new FlightService(Mock.Of<IFlightRepository>(), new FlightValidator(mockAirportService));

            var expectedException = new ValidationException(ValidationMessages.StartandEndLocationCannotBeSame);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.UtcNow.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public void SearchFlight_IfDepartureDateIsNotValid_ThrowsValidationException(string startLocation, string endLocation)
        {
            var mockAirportService = Mock.Of<IAirportServices>();
            Mock.Get(mockAirportService)
                .Setup(x => x.IsAirportValid(startLocation)).Returns(true);
            Mock.Get(mockAirportService)
                .Setup(x => x.IsAirportValid(endLocation)).Returns(true);

            var searchFlightService = new FlightService(Mock.Of<IFlightRepository>(), new FlightValidator(mockAirportService));

            var expectedException = new ValidationException(ValidationMessages.InvalidDepartureDate);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.UtcNow.AddDays(-1)));

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public void SearchFlight_IfStartAndEndDestinationAndDepartureDateValid_ReturnsFlightDetails(string startLocation, string endLocation)
        {
            var mockAirportService = Mock.Of<IAirportServices>();
            Mock.Get(mockAirportService)
                .Setup(x => x.IsAirportValid(startLocation)).Returns(true);
            Mock.Get(mockAirportService)
                .Setup(x => x.IsAirportValid(endLocation)).Returns(true);

            var mockFlightRepo = Mock.Of<IFlightRepository>();
            Mock.Get(mockFlightRepo)
                .Setup(x => x.FetchFlightDetails(startLocation, endLocation, It.IsAny<DateTime>()))
                .Returns(ExpectedFlightDetails().ToList());
            var searchFlightService = new FlightService(mockFlightRepo, new FlightValidator(mockAirportService));

            var expectedCount = ExpectedFlightDetails().Count;
            var actualResult = searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.UtcNow.AddDays(1));

            Assert.IsType<List<FlightDetail>>(actualResult);
            Assert.Equal(expectedCount, actualResult.Count);
            Assert.True(actualResult.TrueForAll(x => x.StartLocation == startLocation));
            Assert.True(actualResult.TrueForAll(x => x.EndLocation == endLocation));
            Assert.True(actualResult.TrueForAll(x => x.DepartureDate.Date == (DateTime.UtcNow.AddDays(1).Date)));
        }

        [Theory]
        [InlineData("BUD", "IAD")]
        public void SearchFlight_IfNoMatchFoundForFlightSearch_ThrowsValidationException(string startLocation, string endLocation)
        {
            var mockAirportService = Mock.Of<IAirportServices>();
            Mock.Get(mockAirportService)
                .Setup(x => x.IsAirportValid(startLocation)).Returns(true);
            Mock.Get(mockAirportService)
                .Setup(x => x.IsAirportValid(endLocation)).Returns(true);

            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(x => x.FetchFlightDetails(startLocation, endLocation, It.IsAny<DateTime>()))
                .Throws<System.ComponentModel.DataAnnotations.ValidationException>();

            var searchFlightService = new FlightService(mockFlightRepo.Object, new FlightValidator(mockAirportService));

            var expectedException = new System.ComponentModel.DataAnnotations.ValidationException();
            var actualException = Assert.Throws<System.ComponentModel.DataAnnotations.ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.UtcNow.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        private List<FlightDetail> ExpectedFlightDetails()
        {
            List<string> validAirportCodes = new List<string> { "BUD", "LTN", "IAD" };

            var fixture = new Fixture();
            List<FlightDetail> flightDetails = fixture.Build<FlightDetail>()
                                         .With(x => x.StartLocation, validAirportCodes[0])
                                         .With(x => x.EndLocation, validAirportCodes[1])
                                         .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(1))
                                         .With(x => x.Length, Convert.ToDecimal("2.35")).CreateMany(4).ToList();

            return flightDetails;
        }
    }
}
