using System.Collections.Generic;
using Moq;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories.FlightRepository;

namespace OnlineFlightSearchAPI.UnitTests.Mocks
{
    class FlightRepositoryMock : Mock<IFlightRepository>
    {
        public FlightRepositoryMock SetUp()
        {
            Setup(x => x.flightDetails).Returns(GetFlightDetails());
            return this;
        }

        public FlightRepositoryMock SetUp(IAirportServices airportService)
        {
            Setup(x => x.flightDetails).Returns(GetFlightDetails());
            return this;
        }

        private List<FlightDetail> GetFlightDetails()
        {
            List<FlightDetail> flightDetails = new List<FlightDetail>();

            FlightDetail flight1 = new FlightDetail("CM 2001", "BUD", "LTN", "2018-12-13 10:00", "2:35", 80, 20);
            FlightDetail flight2 = new FlightDetail("CM 2002", "BUD", "LTN", "2018-12-13 17:00", "10:00", 100, 19);
            FlightDetail flight3 = new FlightDetail("CM 2003", "BUD", "LTN", "2018-12-13 18:00", "3:35", 80, 10);
            FlightDetail flight4 = new FlightDetail("CM 2004", "LTN", "IAD", "2018-12-12 06:00", "2:05", 200, 2);
            FlightDetail flight5 = new FlightDetail("CM 2005", "LTN", "BUD", "2018-12-12 05:00", "2:00", 75, 60);
            FlightDetail flight6 = new FlightDetail("CM 2006", "LTN", "IAD", "2018-12-12 08:00", "12:35", 45, 20);
            FlightDetail flight7 = new FlightDetail("CM 2007", "BUD", "LTN", "2018-12-12 13:00", "12:30", 40, 33);
            FlightDetail flight8 = new FlightDetail("CM 2008", "LTN", "BUD", "2018-12-11 09:00", "2:05", 78, 0);
            FlightDetail flight9 = new FlightDetail("CM 2009", "BUD", "LTN", "2018-12-11 10:00", "2:00", 90, 0);
            FlightDetail flight10 = new FlightDetail("CM 2010", "IAD", "LTN", "2018-12-11 2:00", "14:35", 190, 1);

            flightDetails.Add(flight1);
            flightDetails.Add(flight2);
            flightDetails.Add(flight3);
            flightDetails.Add(flight4);
            flightDetails.Add(flight5);
            flightDetails.Add(flight6);
            flightDetails.Add(flight7);
            flightDetails.Add(flight8);
            flightDetails.Add(flight9);
            flightDetails.Add(flight10);

            return flightDetails;
        }
    }
}
