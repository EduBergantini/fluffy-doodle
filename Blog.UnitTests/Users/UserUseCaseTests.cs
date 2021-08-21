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
        private readonly Mock<IGetUserByEmailRepository> mockedGetByEmailRepository;
        private readonly UserUseCase sut;

        public UserUseCaseTests()
        {
            this.mockedGetByEmailRepository = new Mock<IGetUserByEmailRepository>(MockBehavior.Default);
            this.sut = new UserUseCase(mockedGetByEmailRepository.Object);
        }

        [Fact]
        public async Task ShouldCallGetUserByEmailRepositoryWithCorrectValues()
        {
            
            var actual = "any_mail";
            string emailParameter = null;

            this.mockedGetByEmailRepository.Setup(method => method.GetByEmail(It.IsAny<string>()))
                .Callback<string>((email) => emailParameter = email)
                .ReturnsAsync(new User());

            var user = await this.sut.Authenticate(actual, "any_password");

            Assert.Equal(emailParameter, actual);
        }

        [Fact]
        public async Task ShouldReturnUserWhenGetUserByEmailRepositoryReturnsUser()
        {
            var actual = new User();
            this.mockedGetByEmailRepository
                .Setup(method => method.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(actual);

            var expected = await this.sut.Authenticate("any_email", "any_password");

            Assert.Equal(expected, actual);
        }

    }
}
