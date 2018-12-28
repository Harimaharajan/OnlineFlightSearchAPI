using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Services.AuthenticationServices
{
    public interface IUserService
    {
        bool IsValidUser(UserLoginModel userLoginModel);
    }
}
