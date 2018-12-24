using AutoFixture;
using Moq;
using OnlineFlightSearchAPI.DBModelsFolder;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OnlineFlightSearchAPI.UnitTests.RepositoryTests
{
    public class AirportRepositoryTests
    {
        private readonly IAirportRepository _airportRepository;

        private readonly FlightDBContext _flightDBContext;

        public AirportRepositoryTests()
        {

        }

        [Theory]
        [InlineData("BUD")]
        public void FetchAirportDetail_IfValidAirportCodeProvided_ReturnsAirportDetails(string airportCode)
        {
            var flightDbContextMock = new Mock<FlightDBContext>();
            //flightDbContextMock.Setup(x => x.Airports.Add(ExpectedAirportDetail(airportCode)));
            var airportRepository = new AirportRepository(flightDbContextMock.Object);
            var result = _airportRepository.FetchAirportDetail(airportCode);

            Assert.IsAssignableFrom<IEnumerable<AirportDetail>>(result);
            Assert.Equal(airportCode, result.FirstOrDefault().AirportCode);
        }

        private AirportDetail ExpectedAirportDetail(string airportCode)
        {
            var fixture = new Fixture();
            AirportDetail airportDetail = fixture.Build<AirportDetail>()
                                                        .With(x => x.AirportCode, airportCode)
                                                        .Create();
            return airportDetail;
        }

        [Theory]
        [InlineData("XYZ")]
        public void FetchAirportDetail_IfInvalidAirportCodeProvided_ReturnsAirportDetails(string airportCode)
        {
            var flightDbContextMock = new Mock<FlightDBContext>();
            var airportRepository = new AirportRepository(flightDbContextMock.Object);

            var expected = new NullReferenceException();
            var actual = airportRepository.FetchAirportDetail(airportCode);

            Assert.IsAssignableFrom<IEnumerable<AirportDetail>>(actual);
            Assert.Empty(actual);
        }
    }
}
