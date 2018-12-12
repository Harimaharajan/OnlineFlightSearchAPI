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
            var mockAirportService = new Mock<IAirportServices>();
            mockAirportService.Setup(x => x.IsAirportValid(startLocation)).Returns(false);

            var mockFlightRepo = new Mock<IFlightRepository>();

            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService.Object);

            var expectedException = new ValidationException(ValidationMessages.StartLocationCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, "BUD", DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("BUD", null)]
        [InlineData("BUD", "")]
        public void SearchFlight_IfDestinationNullOrEmpty_ThrowsValidationException(string startLocation, string endLocation)
        {
            var mockAirportService = new Mock<IAirportServices>();
            mockAirportService.Setup(x => x.IsAirportValid(startLocation)).Returns(true);
            mockAirportService.Setup(x => x.IsAirportValid(endLocation)).Returns(false);

            var mockFlightRepo = new Mock<IFlightRepository>();
            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService.Object);

            var expectedException = new ValidationException(ValidationMessages.DestinationCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("ABC", "BUD")]
        public void SearchFlight_IfStartLocationIsNotValid_ThrowsValidationException(string startLocation, string endLocation)
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            mockAirportRepository.Setup(x => x.AirportDetails).Returns(GetAirportDetails());

            var mockAirportService = new Mock<AirportServices>(mockAirportRepository.Object);
            mockAirportService.Setup(x => x.IsAirportValid(startLocation)).Returns(false);
            mockAirportService.Setup(x => x.IsAirportValid(endLocation)).Returns(true);

            var mockFlightRepo = new Mock<IFlightRepository>();
            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService.Object);

            var expectedException = new ValidationException(ValidationMessages.InvalidStartLocation);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("BUD", "XYZ")]
        public void SearchFlight_IfDestinationIsNotValid_ThrowsValidationException(string startLocation, string endLocation)
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            mockAirportRepository.Setup(x => x.AirportDetails).Returns(GetAirportDetails());

            var mockAirportService = new Mock<AirportServices>(mockAirportRepository.Object);
            mockAirportService.Setup(x => x.IsAirportValid(startLocation)).Returns(true);
            mockAirportService.Setup(x => x.IsAirportValid(endLocation)).Returns(false);

            var mockFlightRepo = new Mock<IFlightRepository>();
            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService.Object);

            var expectedException = new ValidationException(ValidationMessages.InvalidDestination);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("BUD", "BUD")]
        [InlineData("LTN", "LTN")]
        public void SearchFlight_IfBothStartAndEndLocationAreSame_ThrowsValidationException(string startLocation, string endLocation)
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            mockAirportRepository.Setup(x => x.AirportDetails).Returns(GetAirportDetails());

            var mockAirportService = new Mock<AirportServices>(mockAirportRepository.Object);
            mockAirportService.Setup(x => x.IsAirportValid(startLocation)).Returns(true);
            mockAirportService.Setup(x => x.IsAirportValid(endLocation)).Returns(true);

            var mockFlightRepo = new Mock<IFlightRepository>();
            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService.Object);

            var expectedException = new ValidationException(ValidationMessages.StartandEndLocationCannotBeSame);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public void SearchFlight_IfDepartureDateIsNotValid_ThrowsValidationException(string startLocation, string endLocation)
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            mockAirportRepository.Setup(x => x.AirportDetails).Returns(GetAirportDetails());

            var mockAirportService = new Mock<AirportServices>(mockAirportRepository.Object);
            mockAirportService.Setup(x => x.IsAirportValid(startLocation)).Returns(true);
            mockAirportService.Setup(x => x.IsAirportValid(endLocation)).Returns(true);

            var mockFlightRepo = new Mock<IFlightRepository>();
            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService.Object);

            var expectedException = new ValidationException(ValidationMessages.InvalidDepartureDate);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.UtcNow.AddDays(-1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("BUD", "LTN")]
        public void SearchFlight_IfStartAndEndDestinationAndDepartureDateValid_ReturnsFlightDetails(string startLocation, string endLocation)
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            mockAirportRepository.Setup(x => x.AirportDetails).Returns(GetAirportDetails());

            var mockAirportService = new Mock<AirportServices>(mockAirportRepository.Object);
            mockAirportService.Setup(x => x.IsAirportValid(startLocation)).Returns(true);
            mockAirportService.Setup(x => x.IsAirportValid(endLocation)).Returns(true);

            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(x => x.FlightDetails).Returns(GetFlightDetails());
            mockFlightRepo.Setup(x => x.FetchFlightDetails(startLocation, endLocation, It.IsAny<DateTime>())).Returns(expectedFlightDetails().ToList());

            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService.Object);

            var actualResult = searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.UtcNow.AddDays(1));

            var expectedCount = expectedFlightDetails().Count;

            Assert.IsType<List<FlightDetail>>(actualResult);
            Assert.Equal(expectedCount, actualResult.Count);
            Assert.True(actualResult.TrueForAll(x => x.StartLocation == startLocation));
            Assert.True(actualResult.TrueForAll(x => x.Destination == endLocation));
            Assert.True(actualResult.TrueForAll(x => x.DepartureDate.Date == (DateTime.UtcNow.AddDays(1).Date)));
        }

        [Theory]
        [InlineData("BUD", "IAD")]
        public void SearchFlight_IfNoMatchFoundForFlightSearch_ThrowsValidationException(string startLocation, string endLocation)
        {
            var mockAirportRepository = new Mock<IAirportRepository>();
            mockAirportRepository.Setup(x => x.AirportDetails).Returns(GetAirportDetails());

            var mockAirportService = new Mock<AirportServices>(mockAirportRepository.Object);
            mockAirportService.Setup(x => x.IsAirportValid(startLocation)).Returns(true);
            mockAirportService.Setup(x => x.IsAirportValid(endLocation)).Returns(true);

            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(x => x.FlightDetails).Returns(GetFlightDetails());
            mockFlightRepo.Setup(x => x.FetchFlightDetails(startLocation, endLocation, It.IsAny<DateTime>())).Throws<ValidationException>();

            ISearchFlightService searchFlightService = new FlightService(mockFlightRepo.Object, mockAirportService.Object);

            var expectedException = new ValidationException();
            var actualException = Assert.Throws<ValidationException>(() => searchFlightService.SearchFlightDetails(startLocation, endLocation, DateTime.UtcNow.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        private List<FlightDetail> expectedFlightDetails()
        {
            List<string> validAirportCodes = new List<string> { "BUD", "LTN", "IAD" };
            Random random = new Random();

            var fixture = new Fixture();
            List<FlightDetail> flightDetails = fixture.Build<FlightDetail>()
                                         .With(x => x.StartLocation, validAirportCodes[0])
                                         .With(x => x.Destination, validAirportCodes[1])
                                         .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(1))
                                         .With(x => x.TravelTime, "2:35").CreateMany(4).ToList();

            return flightDetails;
        }

        private List<FlightDetail> GetFlightDetails()
        {
            List<string> validAirportCodes = new List<string> { "BUD", "LTN", "IAD" };
            Random random = new Random();

            var fixture = new Fixture();
            List<FlightDetail> flightDetails = new List<FlightDetail>();

            flightDetails.AddRange(fixture.Build<FlightDetail>()
                                         .With(x => x.StartLocation, validAirportCodes[0])
                                         .With(x => x.Destination, validAirportCodes[1])
                                         .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(1))
                                         .With(x => x.TravelTime, "2:35").CreateMany(4).ToList());

            flightDetails.AddRange(fixture.Build<FlightDetail>()
                                         .With(x => x.StartLocation, validAirportCodes[random.Next(validAirportCodes.Count)])
                                         .With(x => x.Destination, validAirportCodes[random.Next(validAirportCodes.Count)])
                                         .With(x => x.DepartureDate, DateTime.UtcNow.AddDays(2))
                                         .With(x => x.TravelTime, "10:35").CreateMany(2).ToList());
            flightDetails.AddRange(fixture.Build<FlightDetail>()
                                         .With(x => x.StartLocation, validAirportCodes[random.Next(validAirportCodes.Count)])
                                         .With(x => x.Destination, validAirportCodes[random.Next(validAirportCodes.Count)])
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
