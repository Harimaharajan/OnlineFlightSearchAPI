using System.ComponentModel.DataAnnotations;
using AutoFixture;
using Moq;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Repositories;
using OnlineFlightSearchAPI.Services.AuthenticationServices;
using Xunit;

namespace OnlineFlightSearchAPI.UnitTests.ServiceTests
{
    public class UserServicesTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void IsValidUser_IfUserNameIsNullOrEmpty_ReturnsValidationException(string userName)
        {
            var fixture = new Fixture();
            var user = fixture.Build<UserLoginModel>()
                              .With(x => x.UserName, userName)
                              .Create();

            var userService = new UserService(new Mock<IUserRepository>().Object);

            var expectedException = new ValidationException(ValidationMessages.UserNameCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => userService.IsValidUser(user));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void IsValidUser_IfPasswordIsNullOrEmpty_ReturnsValidationException(string password)
        {
            var fixture = new Fixture();
            var user = fixture.Build<UserLoginModel>()
                              .With(x => x.Password, password)
                              .Create();

            var userService = new UserService(new Mock<IUserRepository>().Object);

            var expectedException = new ValidationException(ValidationMessages.PasswordCannotBeEmpty);
            var actualException = Assert.Throws<ValidationException>(() => userService.IsValidUser(user));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("abc", "def")]
        public void IsValidUser_IfInValidUserNameAndPasswordProvided_ReturnsValidationException(string userName, string password)
        {
            var fixture = new Fixture();
            var user = fixture.Build<UserLoginModel>()
                              .With(x => x.Password, password)
                              .With(x => x.UserName, userName)
                              .Create();

            var invalidUserDetails = fixture.Build<UserLoginModel>()
                        .Create();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.FetchValidUserDetails(user.UserName, user.Password)).Returns(invalidUserDetails);

            var userService = new UserService(mockUserRepository.Object);

            var expectedException = new ValidationException(ValidationMessages.InvalidUserCredentials);
            var actualException = Assert.Throws<ValidationException>(() => userService.IsValidUser(user));

            Assert.Equal(expectedException.Message, actualException.Message);
        }

        [Theory]
        [InlineData("Hari", "123")]
        public void IsValidUser_IfValidUserNameAndPasswordProvided_ReturnsTrue(string userName, string password)
        {
            var fixture = new Fixture();
            var user = fixture.Build<UserLoginModel>()
                              .With(x => x.Password, password)
                              .With(x => x.UserName, userName)
                              .Create();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.FetchValidUserDetails(user.UserName, user.Password)).Returns(user);

            var userService = new UserService(mockUserRepository.Object);

            var actual = userService.IsValidUser(user);

            Assert.True(actual);
        }
    }
}
