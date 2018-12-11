using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoFixture;
using Moq;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories;
using OnlineFlightSearchAPI.Repositories.FlightRepository;
using Xunit;

namespace OnlineFlightSearchAPITestCases
{
    public class FlightServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SearchFlight_IfStartDestinationEmpty_ThrowsValidationException(string startLocation)
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            var mockAirportService = new AirportServices(mockAirportRepository.Object);

            var mockFlightRepo = new Mock<IFlightRepository>();

            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService);

            var expectedException = new ValidationException(ValidationMessages.StartLocationCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, "BUD", DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SearchFlight_IfDestinationNullOrEmpty_ThrowsValidationException(string endLocation)
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            mockAirportRepository.Setup(x => x.airportDetails).Returns(GetAirportDetails());

            var mockAirportService = new AirportServices(mockAirportRepository.Object);

            var mockFlightRepo = new Mock<IFlightRepository>();
            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService);

            var expectedException = new ValidationException(ValidationMessages.DestinationCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails("BUD", endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("ABC")]
        public void SearchFlight_IfStartLocationIsNotValid_ThrowsValidationException(string startLocation)
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            mockAirportRepository.Setup(x => x.airportDetails).Returns(GetAirportDetails());

            var mockAirportService = new AirportServices(mockAirportRepository.Object);

            var mockFlightRepo = new Mock<IFlightRepository>();
            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService);

            var expectedException = new ValidationException(ValidationMessages.InvalidStartLocation);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, "BUD", DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("XYZ")]
        public void SearchFlight_IfDestinationIsNotValid_ThrowsValidationException(string endLocation)
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            mockAirportRepository.Setup(x => x.airportDetails).Returns(GetAirportDetails());

            var mockAirportService = new AirportServices(mockAirportRepository.Object);

            var mockFlightRepo = new Mock<IFlightRepository>();
            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService);

            var expectedException = new ValidationException(ValidationMessages.InvalidDestination);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails("BUD", endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("BUD", "BUD")]
        [InlineData("LTN", "LTN")]
        public void SearchFlight_IfBothStartAndEndLocationAreSame_ThrowsValidationException(string startLocation, string endLocation)
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            mockAirportRepository.Setup(x => x.airportDetails).Returns(GetAirportDetails());

            var mockAirportService = new AirportServices(mockAirportRepository.Object);

            var mockFlightRepo = new Mock<IFlightRepository>();
            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService);

            var expectedException = new ValidationException(ValidationMessages.StartandEndLocationCannotBeSame);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Fact]
        public void SearchFlight_IfDepartureDateIsNotValid_ThrowsValidationException()
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            mockAirportRepository.Setup(x => x.airportDetails).Returns(GetAirportDetails());

            var mockAirportService = new AirportServices(mockAirportRepository.Object);

            var mockFlightRepo = new Mock<IFlightRepository>();
            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService);

            var expectedException = new ValidationException(ValidationMessages.InvalidDepartureDate);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails("BUD", "LTN", DateTime.UtcNow.AddDays(-1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public void SearchFlight_IfStartAndEndDestinationAndDepartureDateValid_ReturnsFlightDetails(string startDestination, string endDestination)
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            mockAirportRepository.Setup(x => x.airportDetails).Returns(GetAirportDetails());

            var mockAirportService = new AirportServices(mockAirportRepository.Object);

            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(x => x.flightDetails).Returns(GetFlightDetails());
            mockFlightRepo.Setup(x => x.FetchFlightDetails(startDestination, endDestination, It.IsAny<DateTime>())).Returns(expectedFlightDetails().ToList());

            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService);

            var actualResult = searchFlightService.SearchFlightDetails(startDestination, endDestination, DateTime.UtcNow.AddDays(1));

            var expectedCount = expectedFlightDetails().Count;

            Assert.IsType<List<FlightDetail>>(actualResult);
            Assert.Equal(expectedCount, actualResult.Count);
            Assert.True(actualResult.TrueForAll(x => x.StartLocation == startDestination));
            Assert.True(actualResult.TrueForAll(x => x.Destination == endDestination));
            Assert.True(actualResult.TrueForAll(x => x.DepartureDate.Date == (DateTime.UtcNow.AddDays(1).Date)));
        }

        [Theory]
        [InlineData("BUD", "IAD")]
        public void SearchFlight_IfNoMatchFoundForFlightSearch_ThrowsValidationException(string startLocation, string endLocation)
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            mockAirportRepository.Setup(x => x.airportDetails).Returns(GetAirportDetails());

            var mockAirportService = new AirportServices(mockAirportRepository.Object);

            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(x => x.flightDetails).Returns(GetFlightDetails());
            mockFlightRepo.Setup(x => x.FetchFlightDetails(startLocation, endLocation, It.IsAny<DateTime>())).Throws<ValidationException>();

            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService);

            var expectedException = new ValidationException();
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.UtcNow.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        private List<FlightDetail> expectedFlightDetails()
        {
            var fixture = new Fixture();
            List<FlightDetail> flightDetails = fixture.Build<FlightDetail>()
                             .With(x => x.StartLocation, "BUD")
                             .With(x => x.Destination, "LTN")
                             .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(1))
                             .With(x => x.TravelTime, "2:35").CreateMany(4).ToList();

            return flightDetails;
        }


        private List<FlightDetail> GetFlightDetails()
        {
            var fixture = new Fixture();
            List<FlightDetail> flightDetails = new List<FlightDetail> {
                fixture.Build<FlightDetail>().With(x => x.StartLocation, "BUD")
                                             .With(x => x.Destination, "LTN")
                                             .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(1))
                                             .With(x => x.TravelTime, "2:35")
                                             .Create(),
                fixture.Build<FlightDetail>().With(x => x.StartLocation, "BUD")
                                             .With(x => x.Destination, "LTN")
                                             .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(2))
                                             .With(x => x.TravelTime, "3:35")
                                             .Create(),
                fixture.Build<FlightDetail>().With(x => x.StartLocation, "BUD")
                                             .With(x => x.Destination, "IAD")
                                             .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(3))
                                             .With(x => x.TravelTime, "2:35")
                                             .Create(),
            };

            flightDetails.AddRange(fixture.Build<FlightDetail>()
                                         .With(x => x.StartLocation, "BUD")
                                         .With(x => x.Destination, "LTN")
                                         .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(1))
                                         .With(x => x.TravelTime, "2:35").CreateMany(3).ToList());
            flightDetails.AddRange(fixture.Build<FlightDetail>()
                                         .With(x => x.StartLocation, "BUD")
                                         .With(x => x.Destination, "IAD")
                                         .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(2))
                                         .With(x => x.TravelTime, "10:35").CreateMany(2).ToList());
            flightDetails.AddRange(fixture.Build<FlightDetail>()
                                         .With(x => x.StartLocation, "LTN")
                                         .With(x => x.Destination, "BUD")
                                         .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(3))
                                         .With(x => x.TravelTime, "3:35").CreateMany(2).ToList());

            return flightDetails;
        }

        private List<AirportDetail> GetAirportDetails()
        {
            List<AirportDetail> airportDetails = new List<AirportDetail>
            {
                new AirportDetail("BUD", "Budapest", "Hungary", DateTime.UtcNow.AddHours(1).Kind),
                new AirportDetail("LTN", "London Luton", "UK", DateTime.UtcNow.Kind),
                new AirportDetail("IAD", "Washington", "USA", DateTime.UtcNow.AddHours(-5).Kind)
            };

            return airportDetails;
        }
    }
}
