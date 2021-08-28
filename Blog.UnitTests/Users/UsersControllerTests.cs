using Blog.Domain.Users.UseCases;
using Blog.Server.Api.Controllers;
using Blog.UnitTests.Users.Fakes;
using Blog.Server.Api.Models;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Blog.Domain.Users.Models;

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
