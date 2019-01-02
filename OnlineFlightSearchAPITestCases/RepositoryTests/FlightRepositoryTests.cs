using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineFlightSearchAPI.DBContext;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories.FlightRepository;
using Xunit;

namespace OnlineFlightSearchAPI.UnitTests.RepositoryTests
{
    public class FlightRepositoryTests : MockDBContext
    {
        [Theory]
        [InlineData("BUD", "LTN")]
        public void FetchFlightDetails_IfValidSearchParametersProvided_ReturnsFlightList(string startLocation, string endLocation)
        {
            var mockFlightsDbSet = new Mock<DbSet<FlightDetail>>();
            var flightDetailsList = new List<FlightDetail>
            {
                new FlightDetail
                {
                    StartLocation=startLocation,
                    EndLocation=endLocation,
                    DepartureDate=DateTime.UtcNow.AddDays(1)
                }
            };
            mockFlightsDbSet = MockDbSet(flightDetailsList);

            var mockDBContext = new Mock<IFlightDBContext>();
            mockDBContext.Setup(x => x.Flights).Returns(mockFlightsDbSet.Object);

            var flightRepository = new FlightRepository(mockDBContext.Object);
            var actual = flightRepository.FetchFlightDetails(startLocation, endLocation, DateTime.Now.AddDays(1));

            Assert.IsType<List<FlightDetail>>(actual);
            Assert.True(actual.TrueForAll(x => x.StartLocation == startLocation));
            Assert.True(actual.TrueForAll(x => x.EndLocation == endLocation));
            Assert.True(actual.TrueForAll(x => x.DepartureDate.Date == DateTime.UtcNow.AddDays(1).Date));
        }

        [Theory]
        [InlineData("XYZ", "LTN")]
        public void FetchFlightDetails_IfInvalidSearchParametersProvided_ReturnsEmptyList(string startLocation, string endLocation)
        {
            var mockFlightsDbSet = new Mock<DbSet<FlightDetail>>();
            var flightDetailsList = new List<FlightDetail> { };
            mockFlightsDbSet = MockDbSet(flightDetailsList);

            var mockDBContext = new Mock<IFlightDBContext>();
            mockDBContext.Setup(x => x.Flights).Returns(mockFlightsDbSet.Object);
            var flightRepository = new FlightRepository(mockDBContext.Object);

            var actual = flightRepository.FetchFlightDetails(startLocation, endLocation, DateTime.Now.AddDays(1));

            Assert.Empty(actual);
        }
    }
}
