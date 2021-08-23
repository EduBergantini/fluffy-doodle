using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MockQueryable.Moq;
using Xunit;

using Blog.Domain.Users.Entities;
using Blog.Infrastructure.SqlServer.Users;
using Blog.UnitTests.Common;
using Blog.UnitTests.Users.Fakes;

namespace Blog.UnitTests.Users
{
    public class UserRepositoryTests : CommonRepositoryTest
    {
        private readonly IEnumerable<User> users;
        private readonly UserRepository sut;

        public UserRepositoryTests()
            : base()
        {
            this.users = UserFake.GetUserList();
            this.sut = new UserRepository(base.DataContextMock.Object);
        }

        [Fact]
        public async Task ShouldReturnValidUserWhenGetByEmailSucceeds()
        {
            //Given
            var mock = this.users.AsQueryable().BuildMockDbSet();
            var actual = this.users.First();

            //When
            base.DataContextMock.SetupGet(property => property.Users).Returns(mock.Object);
            var expected = await sut.GetByEmail(actual.Email);

            //Then
            Assert.Equal(expected, actual);
        }
    }
}
