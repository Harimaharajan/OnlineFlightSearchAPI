using System;
using FluentValidation;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Validator
{
    public class FlightValidator : AbstractValidator<SearchFlightModel>
    {
        private readonly IAirportServices _airportServices;

        public FlightValidator(IAirportServices airportServices)
        {
            _airportServices = airportServices;

            RuleFor(flight => flight.StartLocation)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationMessages.StartLocationCannotBeEmpty)
                .NotEqual(flight => flight.EndLocation).WithMessage(ValidationMessages.StartandEndLocationCannotBeSame)
                .Must(BeAValidAirport).WithMessage(ValidationMessages.InvalidStartLocation);
            RuleFor(flight => flight.EndLocation)
                .NotEmpty().WithMessage(ValidationMessages.DestinationCannotBeEmpty)
                .Must(BeAValidAirport).WithMessage(ValidationMessages.InvalidDestination)
                .NotEqual(flight => flight.StartLocation).WithMessage(ValidationMessages.StartandEndLocationCannotBeSame);
            RuleFor(flight => flight.DepartureDate)
                .GreaterThan(DateTime.UtcNow.Date).WithMessage(ValidationMessages.InvalidDepartureDate);
        }    

        private bool BeAValidAirport(string airportCode)
        {
            return _airportServices.IsAirportValid(airportCode);
        }
    }
}
