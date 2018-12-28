using System.Linq;
using OnlineFlightSearchAPI.DBModelsFolder;
using OnlineFlightSearchAPI.Models;

namespace OnlineFlightSearchAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IFlightDBContext _flightDBContext;

        public UserRepository(IFlightDBContext flightDBContext)
        {
            _flightDBContext = flightDBContext;
        }
        public UserLoginModel FetchValidUserDetails(string userName, string password)
        {
            return _flightDBContext.Users.Where(user => user.UserName == userName &&
                                                        user.Password == password)
                                                        .SingleOrDefault();
        }
    }
}
