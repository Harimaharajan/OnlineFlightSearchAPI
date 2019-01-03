using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoFixture;
using Moq;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Validator;
using Xunit;

namespace OnlineFlightSearchAPI.UnitTests.ValidatorTests
{
    public class FlightValidatorTests
    {
        [Theory]
        [InlineData("", "BUD")]
        [InlineData(null, "BUD")]
        public void FlightValidatorTests_WhenStartLocationIsNullOrEmpty_ThrowsValidationException(string startLocation, string destination)
        {
            var mockAirportServices = Mock.Of<IAirportServices>();

            FlightValidator flightValidator = new FlightValidator(Mock.Of<IAirportServices>());
            var fixture = new Fixture();
            SearchFlightModel flightDetail = fixture.Build<SearchFlightModel>()
                                         .With(x => x.StartLocation, startLocation)
                                         .With(x => x.EndLocation, destination)
                                         .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(1))
                                         .Create();

            var expectedException = new ValidationException(ValidationMessages.StartLocationCannotBeEmpty);
            var actualException = flightValidator.Validate(flightDetail);

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("BUD", "")]
        [InlineData("BUD", null)]
        public void FlightValidatorTests_WhenDestinationIsNullOrEmpty_ThrowsValidationException(string startLocation, string destination)
        {
            var mockAirportServices = Mock.Of<IAirportServices>();
            Mock.Get(mockAirportServices)
                .Setup(x => x.IsAirportValid(startLocation))
                .Returns(true);

            FlightValidator flightValidator = new FlightValidator(mockAirportServices);
            var fixture = new Fixture();
            SearchFlightModel flightDetail = fixture.Build<SearchFlightModel>()
                                         .With(x => x.StartLocation, startLocation)
                                         .With(x => x.EndLocation, destination)
                                         .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(1))
                                         .Create();

            var expectedException = new ValidationException(ValidationMessages.DestinationCannotBeEmpty);
            var actualException = flightValidator.Validate(flightDetail);

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("BUD", "BUD")]
        public void FlightValidatorTests_WhenStartAndEndLocationAreSame_ThrowsValidationException(string startLocation, string destination)
        {
            var mockAirportServices = Mock.Of<IAirportServices>();
            Mock.Get(mockAirportServices)
                .Setup(x => x.IsAirportValid(startLocation))
                .Returns(true);
            Mock.Get(mockAirportServices)
                .Setup(x => x.IsAirportValid(destination))
                .Returns(true);

            FlightValidator flightValidator = new FlightValidator(mockAirportServices);
            var fixture = new Fixture();
            SearchFlightModel flightDetail = fixture.Build<SearchFlightModel>()
                                         .With(x => x.StartLocation, startLocation)
                                         .With(x => x.EndLocation, destination)
                                         .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(1))
                                         .Create();

            var expectedException = new ValidationException(ValidationMessages.StartandEndLocationCannotBeSame);
            var actualException = flightValidator.Validate(flightDetail);

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("IAD", "BUD")]
        public void FlightValidatorTests_WhenStartLocationIsInvalid_ThrowsValidationException(string startLocation, string destination)
        {
            var mockAirportServices = Mock.Of<IAirportServices>();
            Mock.Get(mockAirportServices)
                .Setup(x => x.IsAirportValid(startLocation))
                .Returns(false);
            Mock.Get(mockAirportServices)
                .Setup(x => x.IsAirportValid(destination))
                .Returns(true);

            FlightValidator flightValidator = new FlightValidator(mockAirportServices);
            var fixture = new Fixture();
            SearchFlightModel flightDetail = fixture.Build<SearchFlightModel>()
                                         .With(x => x.StartLocation, startLocation)
                                         .With(x => x.EndLocation, destination)
                                         .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(1))
                                         .Create();

            var expectedException = new ValidationException(ValidationMessages.InvalidStartLocation);
            var actualException = flightValidator.Validate(flightDetail);

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("BUD", "IAD")]
        public void FlightValidatorTests_WhenDestinationIsInvalid_ThrowsValidationException(string startLocation, string destination)
        {
            var mockAirportServices = Mock.Of<IAirportServices>();
            Mock.Get(mockAirportServices)
                .Setup(x => x.IsAirportValid(startLocation))
                .Returns(true);
            Mock.Get(mockAirportServices)
                .Setup(x => x.IsAirportValid(destination))
                .Returns(false);

            FlightValidator flightValidator = new FlightValidator(mockAirportServices);
            var fixture = new Fixture();
            SearchFlightModel flightDetail = fixture.Build<SearchFlightModel>()
                                         .With(x => x.StartLocation, startLocation)
                                         .With(x => x.EndLocation, destination)
                                         .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(1))
                                         .Create();

            var expectedException = new ValidationException(ValidationMessages.InvalidDestination);
            var actualException = flightValidator.Validate(flightDetail);

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public void FlightValidatorTests_WhenDepartureDateIsInPast_ThrowsValidationException(string startLocation, string destination)
        {
            var mockAirportServices = Mock.Of<IAirportServices>();
            Mock.Get(mockAirportServices)
                .Setup(x => x.IsAirportValid(startLocation))
                .Returns(true);
            Mock.Get(mockAirportServices)
                .Setup(x => x.IsAirportValid(destination))
                .Returns(true);

            FlightValidator flightValidator = new FlightValidator(mockAirportServices);
            var fixture = new Fixture();
            SearchFlightModel flightDetail = fixture.Build<SearchFlightModel>()
                                         .With(x => x.StartLocation, startLocation)
                                         .With(x => x.EndLocation, destination)
                                         .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(-1))
                                         .Create();

            var expectedException = new ValidationException(ValidationMessages.InvalidDepartureDate);
            var actualException = flightValidator.Validate(flightDetail);

            Assert.Equal(expectedException.Message, actualException.Errors.FirstOrDefault().ToString());
        }
    }
}
