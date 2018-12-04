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
        [InlineData("")]
        [InlineData(null)]
        public void IsAirportValid_IfAirportCodeIsNullOrEmpty_ReturnsFalse(string airportCode)
        {
            IUnityContainer container = Initialize();
            AirportServices airportServices = container.Resolve<AirportServices>();

            var result = airportServices.IsAirportValid(airportCode);

            Assert.False(result);
        }

        [Theory]
        [InlineData("BUD")]
        [InlineData("IAD")]
        public void IsAirportValid_IfAirportCodeIsValid_ReturnsTrue(string airportCode)
        {
            IUnityContainer container = Initialize();
            AirportServices airportServices = container.Resolve<AirportServices>();

            var result = airportServices.IsAirportValid(airportCode);

            Assert.True(result);
        }

        [Theory]
        [InlineData("XYZ")]
        public void IsAirportValid_IfAirportCodeIsNotValid_ReturnsFalse(string airportCode)
        {
            IUnityContainer container = Initialize();
            AirportServices airportServices = container.Resolve<AirportServices>();

            var result = airportServices.IsAirportValid(airportCode);

            Assert.False(result);
        }

    }
}
