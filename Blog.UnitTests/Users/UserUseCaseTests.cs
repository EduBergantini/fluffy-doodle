using System;
using System.Threading.Tasks;

using Xunit;
using Moq;

using Blog.Application.Users.UseCases;
using Blog.Application.Users.Protocols;
using Blog.Domain.Users.Errors;
using Blog.Domain.Users.Entities;
using Blog.Domain.Users.Models;
using Blog.UnitTests.Users.Fakes;
using Blog.Application.Common.Protocols;

namespace Blog.UnitTests.Users
{
    public class UserUseCaseTests
    {
        private readonly string fakeHashedPassword;
        private readonly User contextUser;
        private readonly Mock<IGetUserByEmailRepository> mockedGetByEmailRepository;
        private readonly Mock<ICompareHash> mockedCompareHasher;
        private readonly Mock<ICreateEncryption> mockedEncryption;
        private readonly UserUseCase sut;

        public UserUseCaseTests()
        {
            this.fakeHashedPassword = "encrypted_password";
            this.contextUser = UserFake.GetUser(this.fakeHashedPassword);
            this.mockedGetByEmailRepository = new Mock<IGetUserByEmailRepository>(MockBehavior.Default);
            this.mockedGetByEmailRepository.Setup(method => method.GetByEmail(It.IsAny<string>())).ReturnsAsync(this.contextUser);

            this.mockedCompareHasher = new Mock<ICompareHash>(MockBehavior.Default);
            this.mockedCompareHasher.Setup(method => method.CompareHash(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            this.mockedEncryption = new Mock<ICreateEncryption>(MockBehavior.Default);
            this.mockedEncryption.Setup(method => method.CreateToken(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(UserFake.GetAuthToken());

            this.sut = new UserUseCase(this.mockedGetByEmailRepository.Object, this.mockedCompareHasher.Object, this.mockedEncryption.Object);
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
        public async Task ShouldCallCompareHasherWithCorrectValues()
        {

            var actual = "any_password";
            string passwordParameter = null;
            string hashedPasswordParameter = null;

            this.mockedCompareHasher.Setup(method => method.CompareHash(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((password, hashedPassword) =>
                {
                    passwordParameter = password;
                    hashedPasswordParameter = hashedPassword;
                })
                .ReturnsAsync(true);

            var user = await this.sut.Authenticate("any_mail", actual);

            Assert.Equal(passwordParameter, actual);
            Assert.Equal(hashedPasswordParameter, this.fakeHashedPassword);
        }

        [Fact]
        public async Task ShouldThrowWhenCompareHasherThrows()
        {
            this.mockedCompareHasher.Setup(method => method.CompareHash(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            await Assert.ThrowsAsync<Exception>(() => this.sut.Authenticate("any_email", "any_password"));
        }

        [Fact]
        public async Task ShouldThrowInvalidPasswordWhenCompareHashReturnsFalse()
        {
            this.mockedCompareHasher.Setup(method => method.CompareHash(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            await Assert.ThrowsAsync<InvalidPasswordException>(() => this.sut.Authenticate("any_email", "any_password"));
        }

        [Fact]
        public async Task ShouldCallCreateTokenWithCorrectParameters()
        {
            int userIdParameter = -1;
            int roleIdParameter = -1;

            this.mockedEncryption.Setup(method => method.CreateToken(It.IsAny<int>(), It.IsAny<int>()))
                .Callback<int, int>((userId, roleId) =>
                {
                    userIdParameter = userId;
                    roleIdParameter = roleId;
                });

            var user = await this.sut.Authenticate("any_mail", "any_password");

            Assert.Equal(this.contextUser.Id, userIdParameter);
            Assert.Equal(this.contextUser.RoleId, roleIdParameter);
        }

        [Fact]
        public async Task ShouldThrowWhenCreateTokenThrows()
        {
            this.mockedEncryption.Setup(method => method.CreateToken(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception());
            await Assert.ThrowsAsync<Exception>(() => this.sut.Authenticate("any_mail", "any_password"));
        }
    }
}
