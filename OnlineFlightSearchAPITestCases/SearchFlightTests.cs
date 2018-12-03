using AutoFixture;
using OnlineFlightSearchAPI.Models;
using System;
using Xunit;

namespace OnlineFlightSearchAPITestCases
{
    public class SearchFlightTests
    {
        [Fact]
        public void SearchFlight_IfStartDestinationEmpty_ReturnsValidationException()
        {
            var fixture = new Fixture();
            FlightDetail searchFlightDetails = fixture.Build<FlightDetail>().With(x => x.StartDestination, string.Empty).Create();
        }

        [Fact]
        public void SearchFlight_IfEndDestinationEmpty_ReturnsValidationException()
        {
            var fixture = new Fixture();
            FlightDetail searchFlightDetails = fixture.Build<FlightDetail>().With(x => x.EndDestination, string.Empty).Create();
        }

        [Theory]
        [InlineData("BUD")]
        [InlineData("LTN")]
        public void SearchFlight_IfStartDestinationValid_ReturnsTrue(string startDestination)
        {
            var fixture = new Fixture();
            FlightDetail searchFlightDetails = fixture.Build<FlightDetail>().With(x => x.StartDestination, startDestination).Create();
        }

        [Theory]
        [InlineData("BUD")]
        [InlineData("IBZ")]
        public void SearchFlight_IfEndDestinationValid_ReturnsTrue(string endDestination)
        {
            var fixture = new Fixture();
            FlightDetail searchFlightDetails = fixture.Build<FlightDetail>().With(x => x.EndDestination, endDestination).Create();
        }

        [Theory]
        [InlineData("ABC")]
        public void SearchFlight_IfStartDestinationInValid_ReturnsValidationException(string startDestination)
        {
            var fixture = new Fixture();
            FlightDetail searchFlightDetails = fixture.Build<FlightDetail>().With(x => x.StartDestination, startDestination).Create();
        }

        [Theory]
        [InlineData("XYZ")]
        public void SearchFlight_IfEndDestinationInValid_ReturnsValidationException(string endDestination)
        {
            var fixture = new Fixture();
            FlightDetail searchFlightDetails = fixture.Build<FlightDetail>().With(x => x.EndDestination, endDestination).Create();
        }

        [Fact]
        public void SearchFlight_IfSearchDateInValid_ReturnsValidationException()
        {
            var fixture = new Fixture();
            FlightDetail searchFlightDetails = fixture.Build<FlightDetail>().With(x => x.DepartureDate, DateTime.Now.AddDays(-1)).Create();
        }

        [Fact]
        public void SearchFlight_IfSearchDateValid_ReturnsTrue()
        {
            var fixture = new Fixture();
            FlightDetail searchFlightDetails = fixture.Build<FlightDetail>().With(x => x.DepartureDate, DateTime.Now.AddDays(1)).Create();
        }

        [Theory]
        [InlineData("BUD", "IBZ")]
        public void SearchFlight_IfStartAndEndDestinationAndSearchDateValid_ReturnsFlightDetails(string startDestination, string endDestination)
        {
            var fixture = new Fixture();
            FlightDetail searchFlightDetails = fixture.Build<FlightDetail>()
                                                      .With(x => x.StartDestination, startDestination)
                                                      .With(x => x.EndDestination, endDestination)
                                                      .With(x => x.DepartureDate, DateTime.Now.AddDays(1))
                                                      .Create();
        }

        [Fact]
        public void SearchFlight_SortByTimeAscending_ReturnsFlightDetailsinAscendingOrderOfTime()
        {
            
        }

        [Fact]
        public void SearchFlight_SortByTimeDescending_ReturnsFlightDetailsinAscendingOrderOfTime()
        {
            
        }

        [Fact]
        public void SearchFlight_SortByPriceAscending_ReturnsFlightDetailsinAscendingOrderOfPrice()
        {
            
        }

        [Fact]
        public void SearchFlight_SortByPriceDescending_ReturnsFlightDetailsinAscendingOrderOfPrice()
        {
            
        }
    }
}
