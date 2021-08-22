using System;
using System.Threading.Tasks;

using Xunit;
using Moq;

using Blog.Application.Users.UseCases;
using Blog.Application.Users.Protocols;
using Blog.Domain.Users.Errors;
using Blog.Domain.Users.Entities;
using Blog.UnitTests.Users.Fakes;
using Blog.Application.Common.Protocols;

namespace Blog.UnitTests.Users
{
    public class UserUseCaseTests
    {
        private readonly string fakeHashedPassword;
        private readonly Mock<IGetUserByEmailRepository> mockedGetByEmailRepository;
        private readonly Mock<ICreateHash> mockedHasher;
        private readonly UserUseCase sut;

        public UserUseCaseTests()
        {
            this.fakeHashedPassword = "encrypted_password";
            this.mockedGetByEmailRepository = new Mock<IGetUserByEmailRepository>(MockBehavior.Default);
            this.mockedGetByEmailRepository.Setup(method => method.GetByEmail(It.IsAny<string>())).ReturnsAsync(UserFake.GetUser(this.fakeHashedPassword));

            this.mockedHasher = new Mock<ICreateHash>(MockBehavior.Default);
            this.mockedHasher.Setup(method => method.CreateHash(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(this.fakeHashedPassword);

            this.sut = new UserUseCase(this.mockedGetByEmailRepository.Object, this.mockedHasher.Object);
        }

        [Fact]
        public async Task ShouldCallGetUserByEmailRepositoryWithCorrectValues()
        {
            
            var actual = "any_mail";
            string emailParameter = null;

            this.mockedGetByEmailRepository.Setup(method => method.GetByEmail(It.IsAny<string>()))
                .Callback<string>((email) => emailParameter = email)
                .ReturnsAsync(UserFake.GetUser(this.fakeHashedPassword));

            var user = await this.sut.Authenticate(actual, "any_password");

            Assert.Equal(emailParameter, actual);
        }

        [Fact]
        public async Task ShouldReturnUserWhenGetUserByEmailRepositoryReturnsUser()
        {
            var actual = UserFake.GetUser(this.fakeHashedPassword);
            this.mockedGetByEmailRepository.Setup(method => method.GetByEmail(It.IsAny<string>())).ReturnsAsync(actual);
            var expected = await this.sut.Authenticate("any_email", "any_password");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ShouldThrowWhenGetUserByEmailRepositoryThrows()
        {
            var actual = new Exception();
            this.mockedGetByEmailRepository.Setup(method => method.GetByEmail(It.IsAny<string>())).ThrowsAsync(actual);
            await  Assert.ThrowsAsync<Exception>(() => this.sut.Authenticate("any_email", "any_password"));
        }
        
        [Fact]
        public async Task ShouldThrowUserNotFoundWhenGetUserByEmailRepositoryReturnsNull()
        {
            User empty = null;
            this.mockedGetByEmailRepository.Setup(method => method.GetByEmail(It.IsAny<string>())).ReturnsAsync(empty);
            await Assert.ThrowsAsync<UserNotFoundException>(() => this.sut.Authenticate("any_email", "any_password"));
        }

        [Fact]
        public async Task ShouldCallHasherWithCorrectValues()
        {

            var actual = "any_password";
            var email = "any_mail";
            string passwordParameter = null;
            int iterationsParameter = -1;

            this.mockedHasher.Setup(method => method.CreateHash(It.IsAny<string>(), It.IsAny<int>()))
                .Callback<string, int>((password, iterations) =>
                {
                    passwordParameter = password;
                    iterationsParameter = iterations;
                })
                .ReturnsAsync("encrypted_password");

            var user = await this.sut.Authenticate(email, actual);

            Assert.Equal(passwordParameter, actual);
            Assert.Equal(iterationsParameter, email.Length);
        }

        [Fact]
        public async Task ShouldThrowWhenHasherThrows()
        {
            this.mockedHasher.Setup(method => method.CreateHash(It.IsAny<string>(), It.IsAny<int>())).ThrowsAsync(new Exception());
            await Assert.ThrowsAsync<Exception>(() => this.sut.Authenticate("any_email", "any_password"));
        }

        [Fact]
        public async Task ShouldThrowInvalidPasswordWhenPasswordIsInvalid()
        {
            this.mockedHasher.Setup(method => method.CreateHash(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync("encrypted_password");
            this.mockedGetByEmailRepository.Setup(method => method.GetByEmail(It.IsAny<string>())).ReturnsAsync(UserFake.GetUser("another_encrypted_password"));
            await Assert.ThrowsAsync<InvalidPasswordException>(() => this.sut.Authenticate("any_email", "any_password"));
        }
    }
}
