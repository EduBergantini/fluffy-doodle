using Blog.Infrastructure.Common.Adapters;
using Blog.Infrastructure.Common.Adapters.Configurations;
using System.Threading.Tasks;

using Xunit;

namespace Blog.UnitTests.Common
{
    public class IdentityJwtAdapterTests
    {
        [Fact]
        public async Task ShouldReturnValidTokenWhenCreateTokenSucceeds()
        {
            var fakeAuthenticationToken = new AuthenticationTokenConfiguration("any_key");
            
            var sut = new IdentityJwtAdapter(fakeAuthenticationToken);

            var actual = await sut.CreateToken(1, 1);

            Assert.NotNull(actual);
            Assert.NotNull(actual.Token);
            Assert.True(actual.ExpireInMs > 0);
            Assert.Equal(fakeAuthenticationToken.ExpireInMs, actual.ExpireInMs);
        }
    }
}
