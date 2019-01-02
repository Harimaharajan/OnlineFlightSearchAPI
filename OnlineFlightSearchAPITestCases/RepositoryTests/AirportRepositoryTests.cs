using System.Collections.Generic;
using System.Linq;
using Moq;
using OnlineFlightSearchAPI.DBContext;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories;
using Xunit;

namespace OnlineFlightSearchAPI.UnitTests.RepositoryTests
{
    public class AirportRepositoryTests : MockDBContext
    {
        [Theory]
        [InlineData("BUD")]
        public void FetchAirportDetail_IfValidAirportCodeProvided_ReturnsListOfAirportDetails(string airportCode)
        {
            var airportDetailsList = new List<AirportDetail>
            {
                new AirportDetail { AirportCode = airportCode },
            };

            var mockAirportDbSet = MockDbSet(airportDetailsList);

            var flightDbContextMock = new Mock<IFlightDBContext>();
            flightDbContextMock.Setup(x => x.Airports).Returns(mockAirportDbSet.Object);

            var airportRepository = new AirportRepository(flightDbContextMock.Object);
            var result = airportRepository.FetchAirportDetail(airportCode);

            Assert.IsAssignableFrom<IEnumerable<AirportDetail>>(result);
            Assert.Equal(airportCode, result.FirstOrDefault().AirportCode);
        }

        [Fact]
        public void FetchAirportDetail_IfNullIsProvidedForAirportCode_ReturnsEmptyList()
        {
            string airportCode = null;
            List<AirportDetail> airportDetailsEmptyList = new List<AirportDetail> { };

            var mockAirportDbSet = MockDbSet(airportDetailsEmptyList);

            var flightDbContextMock = new Mock<IFlightDBContext>();
            flightDbContextMock.Setup(x => x.Airports).Returns(mockAirportDbSet.Object);

            var airportRepository = new AirportRepository(flightDbContextMock.Object);
            var actual = airportRepository.FetchAirportDetail(airportCode);

            Assert.Empty(actual);
        }

        [Theory]
        [InlineData("XYZ")]
        public void FetchAirportDetail_IfInvalidAirportCodeProvided_ReturnsEmptyList(string airportCode)
        {
            var airportDetailsEmptyList = new List<AirportDetail> { };
            var mockAirportDbSet = MockDbSet(airportDetailsEmptyList);

            var flightDbContextMock = new Mock<IFlightDBContext>();
            flightDbContextMock.Setup(x => x.Airports).Returns(mockAirportDbSet.Object);

            var airportRepository = new AirportRepository(flightDbContextMock.Object);
            var actual = airportRepository.FetchAirportDetail(airportCode);

            Assert.Empty(actual);
        }
    }
}
