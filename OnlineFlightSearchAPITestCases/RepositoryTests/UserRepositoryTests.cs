using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineFlightSearchAPI.DBContext;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories;
using Xunit;

namespace OnlineFlightSearchAPI.UnitTests.RepositoryTests
{
    public class UserRepositoryTests : MockDBContext
    {
        [Theory]
        [InlineData("abc", "123")]
        public void FetchValidUserDetails_IfInValidUserNameAndPasswordProvided_ReturnsNull(string userName, string password)
        {
            var mockUsersDbSet = new Mock<DbSet<Users>>();
            var UsersList = new List<Users> {
                new Users
                {
                    UserName=userName+"s",
                    Password=password+"p"
                }
            };

            mockUsersDbSet = MockDbSet(UsersList);

            var flightDbContextMock = new Mock<IFlightDBContext>();
            flightDbContextMock.Setup(x => x.Users).Returns(mockUsersDbSet.Object);

            var userRepository = new UserRepository(flightDbContextMock.Object);
            var actual = userRepository.FetchValidUserDetails(userName, password);

            Assert.Null(actual);
        }

        [Theory]
        [InlineData("Hari", "123")]
        public void FetchValidUserDetails_IfValidUserNameAndPasswordProvided_ReturnsValidUserModel(string userName, string password)
        {
            var mockUsersDbSet = new Mock<DbSet<Users>>();
            var UsersList = new List<Users> {
                new Users
                {
                    UserName=userName,
                    Password=password
                }
            };

            mockUsersDbSet = MockDbSet(UsersList);

            var flightDbContextMock = new Mock<IFlightDBContext>();
            flightDbContextMock.Setup(x => x.Users).Returns(mockUsersDbSet.Object);

            var userRepository = new UserRepository(flightDbContextMock.Object);
            var actual = userRepository.FetchValidUserDetails(userName, password);

            Assert.IsType<Users>(actual);
            Assert.Equal(userName, actual.UserName);
            Assert.Equal(password, actual.Password);
        }
    }
}
