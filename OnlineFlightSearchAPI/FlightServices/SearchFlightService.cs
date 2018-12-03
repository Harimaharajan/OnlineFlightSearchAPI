using OnlineFlightSearchAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFlightSearchAPI.FlightServices
{
    public class SearchFlightService : ISearchFlightService
    {
        private readonly IAirportService _iAirportService;

        public SearchFlightService(IAirportService airportService)
        {
            _iAirportService = airportService;
        }

        public bool SearchFlightDetails(string startLocation, string endLocation, DateTime departureDate)
        {
            if(ValidateStartLocation(startLocation))
            {
                if(ValidateDestination(endLocation))
                {
                    if(ValidateDepartureDate(departureDate))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void FetchFlightDetails(string startLocation, string endLocation, DateTime departureDate)
        {
            throw new NotImplementedException();
        }

        private bool ValidateDepartureDate(DateTime departureDate)
        {
            if (DateTime.UtcNow > departureDate)
            {
                throw new ValidationException(ValidationMessages.InvalidDepartureDate);
            }

            return true;
        }

        private bool ValidateDestination(string endLocation)
        {
            if (string.IsNullOrEmpty(endLocation))
            {
                throw new ValidationException(ValidationMessages.DestinationCannotBeEmpty);
            }

            if(!_iAirportService.CheckIfAirportIsValid(endLocation))
            {
                throw new ValidationException(ValidationMessages.InvalidDestination);
            }

            return true;
        }

        private bool ValidateStartLocation(string startLocation)
        {
            if(string.IsNullOrEmpty(startLocation))
            {
                throw new ValidationException(ValidationMessages.StartLocationCannotBeEmpty);
            }

            if(!_iAirportService.CheckIfAirportIsValid(startLocation))
            {
                throw new ValidationException(ValidationMessages.InvalidStartLocation);
            }

            return true;
        }
    }
}
