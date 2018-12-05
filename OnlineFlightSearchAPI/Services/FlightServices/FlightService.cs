using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories.FlightRepository;

namespace OnlineFlightSearchAPI.FlightServices
{
    public class FlightService : ISearchFlightService
    {
        private readonly IAirportServices _airportService;

        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository, IAirportServices airportServices)
        {
            _flightRepository = flightRepository;
            _airportService = airportServices;
        }

        public List<FlightDetail> SearchFlightDetails(string startLocation, string endLocation, DateTime departureDate)
        {
            if (!ValidateStartLocation(startLocation))
            {
                throw new ValidationException(ValidationMessages.InvalidStartLocation);
            }

            if (!ValidateDestination(endLocation))
            {
                throw new ValidationException(ValidationMessages.InvalidDestination);
            }

            if (!ValidateDepartureDate(departureDate))
            {
                throw new ValidationException(ValidationMessages.InvalidDepartureDate);
            }

            if (ValidateStartAndEndLocation(startLocation, endLocation))
            {
                return _flightRepository.FetchFlightDetails(startLocation, endLocation, departureDate);
            }

            return null;
        }

        private bool ValidateStartAndEndLocation(string startLocation, string endLocation)
        {
            if (startLocation.Equals(endLocation))
            {
                throw new ValidationException(ValidationMessages.StartandEndLocationCannotBeSame);
            }

            return true;
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

            if (!_airportService.IsAirportValid(endLocation))
            {
                throw new ValidationException(ValidationMessages.InvalidDestination);
            }

            return true;
        }

        private bool ValidateStartLocation(string startLocation)
        {
            if (string.IsNullOrEmpty(startLocation))
            {
                throw new ValidationException(ValidationMessages.StartLocationCannotBeEmpty);
            }

            if (!_airportService.IsAirportValid(startLocation))
            {
                throw new ValidationException(ValidationMessages.InvalidStartLocation);
            }

            return true;
        }
    }
}
