using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineFlightSearchAPI.DBModelsFolder;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories.FlightRepository;
using Xunit;

namespace OnlineFlightSearchAPI.UnitTests.RepositoryTests
{
    public class FlightRepositoryTests
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
            mockFlightsDbSet = FlightDetailsMock(flightDetailsList);

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
        public void FetchFlightDetails_IfInvalidSearchParametersProvided_ReturnsEmpty(string startLocation, string endLocation)
        {
            var mockFlightsDbSet = new Mock<DbSet<FlightDetail>>();
            var flightDetailsList = new List<FlightDetail> { };
            mockFlightsDbSet = FlightDetailsMock(flightDetailsList);

            var mockDBContext = new Mock<IFlightDBContext>();
            mockDBContext.Setup(x => x.Flights).Returns(mockFlightsDbSet.Object);
            var flightRepository = new FlightRepository(mockDBContext.Object);

            var actual = flightRepository.FetchFlightDetails(startLocation, endLocation, DateTime.Now.AddDays(1));

            Assert.Empty(actual);
        }

        private Mock<DbSet<FlightDetail>> FlightDetailsMock(List<FlightDetail> flightDetailsList)
        {
            var flightDetail = flightDetailsList.AsQueryable();
            var mockFlightDbSet = new Mock<DbSet<FlightDetail>>();

            mockFlightDbSet.As<IQueryable<FlightDetail>>().Setup(m => m.Provider).Returns(flightDetail.Provider);
            mockFlightDbSet.As<IQueryable<FlightDetail>>().Setup(m => m.Expression).Returns(flightDetail.Expression);
            mockFlightDbSet.As<IQueryable<FlightDetail>>().Setup(m => m.ElementType).Returns(flightDetail.ElementType);
            mockFlightDbSet.As<IQueryable<FlightDetail>>().Setup(m => m.GetEnumerator()).Returns(flightDetail.GetEnumerator());

            return mockFlightDbSet;
        }
    }
}
