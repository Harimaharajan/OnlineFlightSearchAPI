using System;
using System.Collections.Generic;
using Moq;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories;

namespace OnlineFlightSearchAPI.UnitTests.Mocks
{
    public class AirportRepositoryMock : Mock<IAirportRepository>
    {
        public AirportRepositoryMock SetUp()
        {
            Setup(x => x.airportDetails).Returns(GetAirportDetails());
            return this;
        }

        private List<AirportDetail> GetAirportDetails()
        {
            List<AirportDetail> airportDetails = new List<AirportDetail>();
            AirportDetail airportBUD = new AirportDetail("BUD", "Budapest", "Hungary", DateTime.UtcNow.AddHours(1).Kind);
            AirportDetail airportLTN = new AirportDetail("LTN", "London Luton", "UK", DateTime.UtcNow.Kind);
            AirportDetail airportIAD = new AirportDetail("IAD", "Washington", "USA", DateTime.UtcNow.AddHours(-5).Kind);
            airportDetails.Add(airportBUD);
            airportDetails.Add(airportLTN);
            airportDetails.Add(airportIAD);

            return airportDetails;
        }
    }
}
