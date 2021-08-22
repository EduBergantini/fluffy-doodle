using System;
using System.Threading.Tasks;

using Xunit;
using Moq;

using Blog.Application.Users.UseCases;
using Blog.Application.Users.Protocols;
using Blog.Domain.Users.Errors;
using Blog.Domain.Users.Entities;
using Blog.UnitTests.Users.Fakes;

namespace Blog.UnitTests.Users
{
    public class UserUseCaseTests
    {
        private readonly Mock<IGetUserByEmailRepository> mockedGetByEmailRepository;
        private readonly Mock<IEncrypter> mockedEncrypter;
        private readonly UserUseCase sut;

        public UserUseCaseTests()
        {
            this.mockedGetByEmailRepository = new Mock<IGetUserByEmailRepository>(MockBehavior.Default);
            this.mockedGetByEmailRepository
                .Setup(method => method.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(UserFake.GetUser());

            this.mockedEncrypter = new Mock<IEncrypter>(MockBehavior.Default);
            this.mockedEncrypter
                .Setup(method => method.Encrypt(It.IsAny<string>()))
                .ReturnsAsync("encrypted_password");

            this.sut = new UserUseCase(this.mockedGetByEmailRepository.Object, this.mockedEncrypter.Object);
        }

        [Fact]
        public async Task ShouldCallGetUserByEmailRepositoryWithCorrectValues()
        {
            
            var actual = "any_mail";
            string emailParameter = null;

            this.mockedGetByEmailRepository.Setup(method => method.GetByEmail(It.IsAny<string>()))
                .Callback<string>((email) => emailParameter = email)
                .ReturnsAsync(UserFake.GetUser());

            var user = await this.sut.Authenticate(actual, "any_password");

            Assert.Equal(emailParameter, actual);
        }

        [Fact]
        public async Task ShouldReturnUserWhenGetUserByEmailRepositoryReturnsUser()
        {
            var actual = UserFake.GetUser();
            this.mockedGetByEmailRepository
                .Setup(method => method.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(actual);

            var expected = await this.sut.Authenticate("any_email", "any_password");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ShouldThrowWhenGetUserByEmailRepositoryThrows()
        {
            var actual = new Exception();
            this.mockedGetByEmailRepository
                .Setup(method => method.GetByEmail(It.IsAny<string>()))
                .ThrowsAsync(actual);

            await  Assert.ThrowsAsync<Exception>(() => this.sut.Authenticate("any_email", "any_password"));
        }
        
        [Fact]
        public async Task ShouldThrowUserNotFoundWhenGetUserByEmailRepositoryReturnsNull()
        {
            User empty = null;

            this.mockedGetByEmailRepository
                .Setup(method => method.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(empty);

            await Assert.ThrowsAsync<UserNotFoundException>(() => this.sut.Authenticate("any_email", "any_password"));
        }

        [Fact]
        public async Task ShouldCallIEncrypterWithCorrectValues()
        {

            var actual = "any_password";
            string passwordParameter = null;

            this.mockedEncrypter.Setup(method => method.Encrypt(It.IsAny<string>()))
                .Callback<string>((password) => passwordParameter = password)
                .ReturnsAsync("encrypted_password");

            var user = await this.sut.Authenticate("any_mail", actual);

            Assert.Equal(passwordParameter, actual);
        }
    }
}
