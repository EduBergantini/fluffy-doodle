using System;
using System.Threading.Tasks;

using Moq;
using SimpleHashing.Net;
using Xunit;

using Blog.Infrastructure.Common.Adapters;

namespace Blog.UnitTests.Common
{
    public class SimpleHashingAdapterTests
    {
        private readonly Mock<ISimpleHash> mockedSimpleHash;
        private readonly SimpleHashingAdapter sut;
        private readonly string hashedPassword;

        public SimpleHashingAdapterTests()
        {
            this.mockedSimpleHash = new Mock<ISimpleHash>(MockBehavior.Default);
            this.sut = new SimpleHashingAdapter(mockedSimpleHash.Object);
            this.hashedPassword = "hashed_password";
            this.mockedSimpleHash.Setup(method => method.Compute(It.IsAny<string>(), It.IsAny<int>())).Returns(this.hashedPassword);
        }

        [Fact]
        public async Task ShouldReturnValidHashedPasswordWhenreateSucceeds()
        {
            var expected = await this.sut.CreateHash("any_password", 1);
            Assert.Equal(expected, this.hashedPassword);
        }

        [Fact]
        public async Task ShouldThrowWhenCreateThrows()
        {
            this.mockedSimpleHash.Setup(method => method.Compute(It.IsAny<string>(), It.IsAny<int>())).Throws(new Exception());
            await Assert.ThrowsAsync<Exception>(() => this.sut.CreateHash("any_password", 1));
        }
    }
}
