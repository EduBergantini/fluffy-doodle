using System.Threading.Tasks;
using System.Linq;
using System;

using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

using Blog.Domain.Users.UseCases;
using Blog.Server.Api.Controllers;
using Blog.UnitTests.Users.Fakes;
using Blog.Server.Api.Models;
using Blog.Domain.Users.Models;
using Blog.Domain.Users.Errors;

namespace Blog.UnitTests.Users
{
    public class UsersControllerTests
    {
        private readonly AuthenticationTokenModel fakeAuthToken;
        private readonly Mock<IAuthenticateUseCase> mockedAuthenticateUseCase;
        private readonly UsersController sut;

        public UsersControllerTests()
        {
            this.fakeAuthToken = UserFake.GetAuthToken();
            this.mockedAuthenticateUseCase = new Mock<IAuthenticateUseCase>(MockBehavior.Default);
            this.mockedAuthenticateUseCase
                .Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(this.fakeAuthToken);
            this.sut = new UsersController(this.mockedAuthenticateUseCase.Object);
        }

        [Fact]
        public async Task ShouldCallAuthenticateWithCorrectParameters()
        {
            string emailParameter = null, passwordParameter = null;
            var authModel = new AuthenticationModel { Email = "any_email", PlainTextPassword = "any_password" };
            this.mockedAuthenticateUseCase
                .Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((email, password) =>
                {
                    emailParameter = email;
                    passwordParameter = password;
                })
                .ReturnsAsync(this.fakeAuthToken);

            var response = await this.sut.Post(authModel);

            Assert.Equal(authModel.Email, emailParameter);
            Assert.Equal(authModel.PlainTextPassword, passwordParameter);
        }

        [Fact]
        public async Task ShouldReturnBadRequestWhenAuthenticationModelIsInvalid()
        {
            const string errorMessage = "Required";
            this.sut.ModelState.AddModelError("Email", errorMessage);
            this.sut.ModelState.AddModelError("PlainTextPassword", errorMessage);

            var response = await this.sut.Post(null);

            var responseResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, responseResult.StatusCode);

            var actual = Assert.IsAssignableFrom<SerializableError>(responseResult.Value);
            Assert.Equal(2, actual.Count);
            foreach (var item in actual)
            {
                Assert.Equal(new string[] { errorMessage }, item.Value);
            }
        }

        [Fact]
        public async Task ShouldReturnBadRequestWhenAuthenticateThrowsUserNotFound()
        {
            this.mockedAuthenticateUseCase
                .Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new UserNotFoundException());

            var response = await this.sut.Post(new AuthenticationModel { Email = "any_email", PlainTextPassword = "any_password" });

            var responseResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, responseResult.StatusCode);

            var actual = Assert.IsAssignableFrom<SerializableError>(responseResult.Value);
            string[] errors = actual.Single().Value as string[];
            Assert.Single(actual);
            Assert.True(errors[0] == "Senha inválida ou o usuário não encontrado");
        }

        [Fact]
        public async Task ShouldReturnBadRequestWhenAuthenticateThrowsInvalidPassword()
        {
            this.mockedAuthenticateUseCase
                .Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new InvalidPasswordException());

            var response = await this.sut.Post(new AuthenticationModel { Email = "any_email", PlainTextPassword = "any_password" });

            var responseResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, responseResult.StatusCode);

            var actual = Assert.IsAssignableFrom<SerializableError>(responseResult.Value);
            string[] errors = actual.Single().Value as string[];
            Assert.Single(actual);
            Assert.True(errors[0] == "Senha inválida ou o usuário não encontrado");
        }

        [Fact]
        public async Task ShouldReturnServerErrorWhenAuthenticateThrowsException()
        {
            var expected = new Exception("Stub Exception");

            this.mockedAuthenticateUseCase
                .Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(expected);

            var response = await this.sut.Post(new AuthenticationModel { Email = "any_email", PlainTextPassword = "any_password" });

            var responseResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(500, responseResult.StatusCode);

            var model = Assert.IsAssignableFrom<Exception>(responseResult.Value);
            Assert.Equal(expected, model);
        }

        [Fact]
        public async Task ShouldReturnAuthTokenWhenSignInSucceeds()
        {
            var authModel = new AuthenticationModel { Email = "any_email", PlainTextPassword = "any_password" };
            var response = await this.sut.Post(authModel);

            var createdResult = Assert.IsType<CreatedResult>(response);
            Assert.Equal(201, createdResult.StatusCode);

            var actual = Assert.IsAssignableFrom<AuthenticationTokenModel>(createdResult.Value);
            Assert.Equal(this.fakeAuthToken, actual);
            Assert.Equal("api/contents", createdResult.Location);
        }
    }
}
