using System.Threading.Tasks;

using Xunit;
using Moq;

using Blog.Application.Users.UseCases;

using Blog.Application.Users.Protocols;
using Blog.Domain.Users.Entities;

namespace Blog.UnitTests.Users
{
    public class UserUseCaseTests
    {

        [Fact]
        public async Task ShouldCallGetUserByEmailRepositoryWithCorrectValues()
        {
            var mockedGetByEmailRepository = new Mock<IGetUserByEmailRepository>(MockBehavior.Default);
            var sut = new UserUseCase(mockedGetByEmailRepository.Object);
            var actual = "any_mail";
            string emailParameter = null;

            mockedGetByEmailRepository.Setup(method => method.GetByEmail(It.IsAny<string>()))
                .Callback<string>((email) => emailParameter = email)
                .ReturnsAsync(new User());

            var user = await sut.Authenticate(actual, "any_password");

            Assert.Equal(emailParameter, actual);
        }
    }
}
