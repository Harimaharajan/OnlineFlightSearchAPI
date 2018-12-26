using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineFlightSearchAPI.DBModelsFolder;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories;
using Xunit;

namespace OnlineFlightSearchAPI.UnitTests.RepositoryTests
{
    public class AirportRepositoryTests
    {
        [Theory]
        [InlineData("BUD")]
        public void FetchAirportDetail_IfValidAirportCodeProvided_ReturnsAirportDetails(string airportCode)
        {
            var mockAirportDbSet = new Mock<DbSet<AirportDetail>>();
            var airportDetailsList = new List<AirportDetail>
            {
                new AirportDetail { AirportCode = airportCode },
            };
            mockAirportDbSet = AirportDetailDbSetMock(airportDetailsList);

            var flightDbContextMock = new Mock<IFlightDBContext>();
            flightDbContextMock.Setup(x => x.Airports).Returns(mockAirportDbSet.Object);

            var airportRepository = new AirportRepository(flightDbContextMock.Object);
            var result = airportRepository.FetchAirportDetail(airportCode);

            Assert.IsAssignableFrom<IEnumerable<AirportDetail>>(result);
            Assert.Equal(airportCode, result.FirstOrDefault().AirportCode);
        }

        [Theory]
        [InlineData(null)]
        public void FetchAirportDetail_IfNullIsProvidedForAirportCode_ReturnsEmpty(string airportCode)
        {
            var mockAirportDbSet = new Mock<DbSet<AirportDetail>>();
            var airportDetailsList = new List<AirportDetail> { };

            mockAirportDbSet = AirportDetailDbSetMock(airportDetailsList);

            var flightDbContextMock = new Mock<IFlightDBContext>();
            flightDbContextMock.Setup(x => x.Airports).Returns(mockAirportDbSet.Object);

            var airportRepository = new AirportRepository(flightDbContextMock.Object);
            var actual = airportRepository.FetchAirportDetail(airportCode);

            Assert.Empty(actual);
        }

        [Theory]
        [InlineData("XYZ")]
        public void FetchAirportDetail_IfInvalidAirportCodeProvided_ReturnsEmpty(string airportCode)
        {
            var mockAirportDbSet = new Mock<DbSet<AirportDetail>>();
            var airportDetailsList = new List<AirportDetail> { };

            mockAirportDbSet = AirportDetailDbSetMock(airportDetailsList);

            var flightDbContextMock = new Mock<IFlightDBContext>();
            flightDbContextMock.Setup(x => x.Airports).Returns(mockAirportDbSet.Object);

            var airportRepository = new AirportRepository(flightDbContextMock.Object);
            var actual = airportRepository.FetchAirportDetail(airportCode);

            Assert.Empty(actual);
        }

        private Mock<DbSet<AirportDetail>> AirportDetailDbSetMock(IEnumerable<AirportDetail> airportDetails)
        {
            var airportDetail = airportDetails.AsQueryable();
            var mockAirportDbSet = new Mock<DbSet<AirportDetail>>();

            mockAirportDbSet.As<IQueryable<AirportDetail>>().Setup(m => m.Provider).Returns(airportDetail.Provider);
            mockAirportDbSet.As<IQueryable<AirportDetail>>().Setup(m => m.Expression).Returns(airportDetail.Expression);
            mockAirportDbSet.As<IQueryable<AirportDetail>>().Setup(m => m.ElementType).Returns(airportDetail.ElementType);
            mockAirportDbSet.As<IQueryable<AirportDetail>>().Setup(m => m.GetEnumerator()).Returns(airportDetail.GetEnumerator());

            return mockAirportDbSet;
        }

    }
}
