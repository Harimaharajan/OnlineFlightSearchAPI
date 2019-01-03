using System.Linq;
using AutoFixture;
using FluentValidation;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Validator;
using Xunit;

namespace OnlineFlightSearchAPI.UnitTests.ValidatorTests
{
    public class UserValidatorTests
    {
        [Theory]
        [InlineData("", "def")]
        public void UserValidatorTests_WhenUserNameisEmpty_ShouldThrowValidationException(string userName, string password)
        {
            UserValidator userValidator = new UserValidator();
            var fixture = new Fixture();
            var user = fixture.Build<Users>()
                              .With(x => x.Password, password)
                              .With(x => x.UserName, userName)
                              .Create();

            var actual = userValidator.Validate(user);
            var expected = new ValidationException(ValidationMessages.UserNameCannotBeEmpty);

            Assert.Equal(expected.Message, actual.Errors.FirstOrDefault().ToString());
        }

        [Theory]
        [InlineData("abc", "")]
        public void UserValidatorTests_WhenPasswordisEmpty_ShouldThrowValidationException(string userName, string password)
        {
            UserValidator userValidator = new UserValidator();
            var fixture = new Fixture();
            var user = fixture.Build<Users>()
                              .With(x => x.Password, password)
                              .With(x => x.UserName, userName)
                              .Create();

            var actual = userValidator.Validate(user);
            var expected = new ValidationException(ValidationMessages.PasswordCannotBeEmpty);

            Assert.Equal(expected.Message, actual.Errors.FirstOrDefault().ToString());
        }
    }
}
