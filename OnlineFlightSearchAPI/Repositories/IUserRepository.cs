using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Repositories
{
    public interface IUserRepository
    {
        UserLoginModel FetchValidUserDetails(string userName, string password);
    }
}