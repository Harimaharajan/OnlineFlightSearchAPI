using OnlineFlightSearchAPI.Controllers;
using OnlineFlightSearchAPI.FlightServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Unity;
using Xunit;

namespace OnlineFlightSearchAPITestCases.ControllerTests
{
    public class SearchFlightsControllerTests
    {
        private IUnityContainer Initialize()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<SearchFlightsController>();
            container.RegisterType<ISearchFlightService, SearchFlightService>();
            container.RegisterType<IAirportServices, AirportServices>();
            return container;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SearchFlight_IfStartDestinationEmpty_ThrowsValidationException(string startLocation)
        {
            IUnityContainer container = Initialize();
            var searchFlightController = container.Resolve<SearchFlightsController>();
            var expectedException = new ValidationException(ValidationMessages.StartLocationCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightController.SearchFlightDetails(startLocation, "BUD", DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SearchFlight_IfDestinationNullOrEmpty_ThrowsValidationException(string endLocation)
        {
            IUnityContainer container = Initialize();
            var searchFlightController = container.Resolve<SearchFlightsController>();
            var expectedException = new ValidationException(ValidationMessages.DestinationCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => searchFlightController.SearchFlightDetails("BUD", endLocation, DateTime.Now.AddDays(1)));

            Assert.Equal(expectedException.Message, actualException.Message);
        }
    }
}
