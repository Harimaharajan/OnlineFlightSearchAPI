using FluentValidation;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFlightSearchAPI.Validator
{
    public class AirportValidator : AbstractValidator<AirportDetail>
    {
        public AirportValidator()
        {
            RuleFor(x => x.AirportCode)
                .NotEmpty().WithMessage(ValidationMessages.InvalidStartLocation);
        }
    }
}
