using System;
using System.ComponentModel.DataAnnotations;
using OnlineFlightSearchAPI.FlightServices;
using Unity;
using Xunit;

namespace OnlineFlightSearchAPITestCases
{
    public class AirportServicesTestCases
    {

        private IUnityContainer Initialize()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IAirportServices, AirportServices>();
            return container;
        }

        [Theory]
        [InlineData("BUD")]
        [InlineData("IAD")]
        public void AirportValidation_CheckIfAirportCodeIsValid_ReturnsTrue(string airportCode)
        {
            IUnityContainer container = Initialize();
            AirportServices airportServices= container.Resolve<AirportServices>();
            
            var result =  airportServices.CheckIfAirportIsValid(airportCode);

            Assert.True(result);
        }

        [Theory]
        [InlineData("XYZ")]
        public void AirportValidation_IfAirportCodeIsNotValid_ReturnsFalse(string airportCode)
        {
            IUnityContainer container = Initialize();
            AirportServices airportServices = container.Resolve<AirportServices>();

            var result = airportServices.CheckIfAirportIsValid(airportCode);

            Assert.False(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void AirportValidation_IfAirportCodeIsNullOrEmpty_ReturnsFalse(string airportCode)
        {
            IUnityContainer container = Initialize();
            AirportServices airportServices = container.Resolve<AirportServices>();

            var result = airportServices.CheckIfAirportIsValid(airportCode);

            Assert.False(result);
        }
    }
}
