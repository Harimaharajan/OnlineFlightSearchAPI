using Moq;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFlightSearchAPI.UnitTests.Mocks
{
    class AirportServiceMock : Mock<IAirportServices>
    {
        public AirportServiceMock SetUp(IAirportRepository airportRerpository)
        {
            var mockAirportRepository = new AirportRepositoryMock().SetUp();

            var mockAirportServices = new AirportServices(airportRerpository);
            return this;
        }
    }
}
