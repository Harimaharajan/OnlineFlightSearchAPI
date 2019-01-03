using FluentValidation;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories.FlightRepository;
using OnlineFlightSearchAPI.Validator;
using System;
using System.Collections.Generic;
using ValidationException = FluentValidation.ValidationException;

namespace OnlineFlightSearchAPI.FlightServices
{
    public class FlightService : ISearchFlightService
    {
        private readonly IFlightRepository _flightRepository;

        private readonly FlightValidator _flightValidator;

        public FlightService(IFlightRepository flightRepository, FlightValidator flightValidator)
        {
            _flightRepository = flightRepository;
            _flightValidator = flightValidator;
        }

        public List<FlightDetail> SearchFlightDetails(string startLocation, string endLocation, DateTime departureDate)
        {
            SearchFlightModel searchFlightModel = new SearchFlightModel
            {
                StartLocation = startLocation,
                EndLocation = endLocation,
                DepartureDate = departureDate
            };

            _flightValidator.ValidateAndThrow(searchFlightModel);

            var flightDetails = _flightRepository.FetchFlightDetails(startLocation, endLocation, departureDate);

            if (flightDetails.Count == 0)
            {
                throw new ValidationException(ValidationMessages.NoFlightsAvailable);
            }

            return flightDetails;
        }
    }
}
