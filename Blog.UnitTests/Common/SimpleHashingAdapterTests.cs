using Blog.Infrastructure.Common.Adapters;
using Moq;
using SimpleHashing.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Blog.UnitTests.Common
{
    public class SimpleHashingAdapterTests
    {
        [Fact]
        public async Task ShouldReturnValidHashedPassword()
        {
            var mockedSimpleHash = new Mock<ISimpleHash>(MockBehavior.Default);
            var sut = new SimpleHashingAdapter(mockedSimpleHash.Object);
            var hashedPassword = "hashed_password";

            mockedSimpleHash.Setup(method => method.Compute(It.IsAny<string>())).Returns("hashed_password");

            var expected = await sut.Hash("any_password");

            Assert.Equal(expected, hashedPassword);
        }
    }
}
