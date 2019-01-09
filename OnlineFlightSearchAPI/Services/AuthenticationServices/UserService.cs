using FluentValidation;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories;
using OnlineFlightSearchAPI.Validator;
using ValidationException = FluentValidation.ValidationException;

namespace OnlineFlightSearchAPI.Services.AuthenticationServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserValidator _userValidator;

        public UserService(IUserRepository userRepository, UserValidator userValidator)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
        }

        public bool IsValidUser(Users userLoginModel)
        {
            _userValidator.ValidateAndThrow(userLoginModel);

            var user = _userRepository.FetchValidUserDetails(userLoginModel.UserName, userLoginModel.Password);
            if (user == null)
            {
                return false;
            }
            else if (user.UserName == userLoginModel.UserName &&
                user.Password == userLoginModel.Password)
            {
                return true;
            }
            else
            {
                throw new ValidationException(ValidationMessages.InvalidUserCredentials);
            }
        }
    }
}
