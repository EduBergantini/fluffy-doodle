using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using MockQueryable.Moq;

using Blog.Domain.Contents.Entities;
using Blog.Infrastructure.SqlServer.Contents;
using Blog.UnitTests.Contents.Fakes;
using Blog.UnitTests.Common;

namespace Blog.UnitTests.Contents
{
    public class ContentRepositoryTests : CommonRepositoryTest
    {
        private readonly IEnumerable<Content> contents;
        private readonly ContentRepository sut;

        public ContentRepositoryTests()
            : base()
        {
            this.contents = ContentFake.GetContentList();
            this.sut = new ContentRepository(base.DataContextMock.Object);
        }

        [Fact]
        public async Task ShouldReturnListOfContentWhenGetContentListSucceeds()
        {
            //Given
            var mock = this.contents.AsQueryable().BuildMockDbSet();

            //When
            base.DataContextMock.SetupGet(property => property.Contents).Returns(mock.Object);
            var expected = await sut.GetContentList();

            //Then
            Assert.Equal(expected, this.contents);
        }

        [Fact]
        public async Task ShouldThrowWhenGetContentListThrows()
        {
            //Given
            var exception = new Exception();

            //When
            base.DataContextMock.SetupGet(property => property.Contents).Throws(exception);

            //Then
            await Assert.ThrowsAsync<Exception>(() => sut.GetContentList());
        }

        [Fact]
        public async Task ShouldReturnContentWhenGetContentReturns()
        {
            //Given
            var mock = this.contents.AsQueryable().BuildMockDbSet();
            var content = this.contents.Last();

            //When
            base.DataContextMock.SetupGet(property => property.Contents).Returns(mock.Object);
            var expected = await sut.GetContent(x => x.PublicId == content.PublicId);

            //Then
            Assert.Equal(expected, content);
        }
    }
}
