using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Repositories
{
    public interface IUserRepository
    {
        Users FetchValidUserDetails(string userName, string password);
    }
}