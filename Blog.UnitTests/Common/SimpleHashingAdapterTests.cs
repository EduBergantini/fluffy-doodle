﻿using System;
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
            this.mockedSimpleHash.Setup(method => method.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        }

        [Fact]
        public async Task ShouldReturnValidHashedPasswordWhenCreateHashSucceeds()
        {
            var expected = await this.sut.CreateHash("any_password", 1);
            Assert.Equal(expected, this.hashedPassword);
        }

        [Fact]
        public async Task ShouldCallComputeWithCorrectParameters()
        {
            string plainTextParameter = string.Empty;
            int iterationsParameter = -1;
            this.mockedSimpleHash.Setup(method => method.Compute(It.IsAny<string>(), It.IsAny<int>()))
                .Callback<string, int>((plainValue, iterations) => 
                {
                    plainTextParameter = plainValue;
                    iterationsParameter = iterations;
                })
                .Returns(this.hashedPassword);

            await this.sut.CreateHash("any_value", 10);

            Assert.Equal("any_value", plainTextParameter);
            Assert.Equal(10, iterationsParameter);
        }

        [Fact]
        public async Task ShouldThrowWhenComputeThrows()
        {
            this.mockedSimpleHash.Setup(method => method.Compute(It.IsAny<string>(), It.IsAny<int>())).Throws(new Exception());
            await Assert.ThrowsAsync<Exception>(() => this.sut.CreateHash("any_password", 1));
        }

        [Fact]
        public async Task ShouldReturnTrueWhenCompareHashSucceeds()
        {
            var expected = await this.sut.CompareHash("any_password", "encrypted_password");
            Assert.True(expected);
        }

        [Fact]
        public async Task ShouldCallVerifyWithCorrectParameters()
        {
            string plainTextParameter = string.Empty;
            string hashedParameter = string.Empty;

            this.mockedSimpleHash.Setup(method => method.Verify(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((plainValue, hashedValue) =>
                {
                    plainTextParameter = plainValue;
                    hashedParameter = hashedValue;
                })
                .Returns(true);

            await this.sut.CompareHash("any_value", "hashed_value");

            Assert.Equal("any_value", plainTextParameter);
            Assert.Equal("hashed_value", hashedParameter);
        }

        [Fact]
        public async Task ShouldThrowWhenVerifyThrows()
        {
            this.mockedSimpleHash.Setup(method => method.Verify(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            await Assert.ThrowsAsync<Exception>(() => this.sut.CompareHash("any_password", "encrypted_password"));
        }

    }
}
