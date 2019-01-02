using FluentValidation;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFlightSearchAPI.Validator
{
    public class FlightValidator: AbstractValidator<FlightDetail>
    {
        public FlightValidator()
        {
            RuleFor(flight => flight.StartLocation)
                .NotEmpty().WithMessage(ValidationMessages.InvalidStartLocation)
                .NotEqual(flight => flight.EndLocation).WithMessage(ValidationMessages.StartandEndLocationCannotBeSame);
            RuleFor(flight => flight.EndLocation)
                .NotEmpty().WithMessage(ValidationMessages.InvalidDestination)
                .NotEqual(flight => flight.StartLocation).WithMessage(ValidationMessages.StartandEndLocationCannotBeSame);
            RuleFor(flight => flight.DepartureDate)
                .LessThan(DateTime.Now.Date).WithMessage(ValidationMessages.InvalidDepartureDate);
        }
    }
}
