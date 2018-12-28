using System.ComponentModel.DataAnnotations;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories;

namespace OnlineFlightSearchAPI.Services.AuthenticationServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsValidUser(UserLoginModel userLoginModel)
        {
            if (userLoginModel == null)
            {
                throw new ValidationException(ValidationMessages.UserNameAndPasswordCannotBeEmpty);
            }

            if (string.IsNullOrWhiteSpace(userLoginModel.UserName))
            {
                throw new ValidationException(ValidationMessages.UserNameCannotBeEmpty);
            }

            if (string.IsNullOrWhiteSpace(userLoginModel.Password))
            {
                throw new ValidationException(ValidationMessages.PasswordCannotBeEmpty);
            }

            var user = _userRepository.FetchValidUserDetails(userLoginModel.UserName, userLoginModel.Password);
            if (user == null)
            {
                throw new ValidationException(ValidationMessages.InvalidUserCredentials);
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
