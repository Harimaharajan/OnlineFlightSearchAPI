using FluentValidation;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFlightSearchAPI.Validator
{
    public class UserValidator : AbstractValidator<Users>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage(ValidationMessages.UserNameCannotBeEmpty);

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage(ValidationMessages.PasswordCannotBeEmpty);
        }
    }
}
