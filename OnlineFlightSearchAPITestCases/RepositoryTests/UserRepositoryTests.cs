using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineFlightSearchAPI.DBModelsFolder;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OnlineFlightSearchAPI.UnitTests.RepositoryTests
{
    public class UserRepositoryTests
    {
        [Theory]
        [InlineData("abc", "123")]
        public void FetchValidUserDetails_IfInValidUserNameAndPasswordProvided_ReturnsEmpty(string userName, string password)
        {
            var mockUsersDbSet = new Mock<DbSet<UserLoginModel>>();
            var UsersList = new List<UserLoginModel> {
                new UserLoginModel
                {
                    UserName=userName+"s",
                    Password=password+"p"
                }
            };

            mockUsersDbSet = UsersDbSetMock(UsersList);

            var flightDbContextMock = new Mock<IFlightDBContext>();
            flightDbContextMock.Setup(x => x.Users).Returns(mockUsersDbSet.Object);

            var userRepository = new UserRepository(flightDbContextMock.Object);
            var actual = userRepository.FetchValidUserDetails(userName, password);

            Assert.Null(actual);
        }

        [Theory]
        [InlineData("Hari", "123")]
        public void FetchValidUserDetails_IfValidUserNameAndPasswordProvided_ReturnsEmpty(string userName, string password)
        {
            var mockUsersDbSet = new Mock<DbSet<UserLoginModel>>();
            var UsersList = new List<UserLoginModel> {
                new UserLoginModel
                {
                    UserName=userName,
                    Password=password
                }
            };

            mockUsersDbSet = UsersDbSetMock(UsersList);

            var flightDbContextMock = new Mock<IFlightDBContext>();
            flightDbContextMock.Setup(x => x.Users).Returns(mockUsersDbSet.Object);

            var userRepository = new UserRepository(flightDbContextMock.Object);
            var actual = userRepository.FetchValidUserDetails(userName, password);

            Assert.IsType<UserLoginModel>(actual);
            Assert.Equal(userName, actual.UserName);
            Assert.Equal(password, actual.Password);
        }

        private Mock<DbSet<UserLoginModel>> UsersDbSetMock(List<UserLoginModel> usersList)
        {
            var userDetail = usersList.AsQueryable();
            var mockUsersDbSet = new Mock<DbSet<UserLoginModel>>();

            mockUsersDbSet.As<IQueryable<UserLoginModel>>().Setup(m => m.Provider).Returns(userDetail.Provider);
            mockUsersDbSet.As<IQueryable<UserLoginModel>>().Setup(m => m.Expression).Returns(userDetail.Expression);
            mockUsersDbSet.As<IQueryable<UserLoginModel>>().Setup(m => m.ElementType).Returns(userDetail.ElementType);
            mockUsersDbSet.As<IQueryable<UserLoginModel>>().Setup(m => m.GetEnumerator()).Returns(userDetail.GetEnumerator());

            return mockUsersDbSet;
        }
    }
}
