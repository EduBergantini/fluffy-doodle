using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Xunit;
using Moq;
using MockQueryable.Moq;

using Blog.Domain.Contents.Entities;
using Blog.Infrastructure.SqlServer.Contexts;
using Blog.Infrastructure.SqlServer.Contents;
using Microsoft.EntityFrameworkCore;
using Blog.UnitTests.Contents.Fakes;

namespace Blog.UnitTests.Contents
{
    public class ContentRepositoryTests
    {
        private readonly Mock<ContentDataContext> dataContextMock;
        private readonly IEnumerable<Content> contents;
        private readonly ContentRepository sut;

        public ContentRepositoryTests()
        {
            var inMemoryOptions = new DbContextOptionsBuilder<ContentDataContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;
            this.dataContextMock = new Mock<ContentDataContext>(MockBehavior.Default, new[] { inMemoryOptions });

            this.contents = ContentFake.GetContentList();
            this.sut = new ContentRepository(this.dataContextMock.Object);
        }

        [Fact]
        public async Task ShouldReturnListOfContentWhenGetContentListSucceeds()
        {
            //Given
            var mock = this.contents.AsQueryable().BuildMockDbSet();

            //When
            this.dataContextMock.SetupGet(property => property.Contents).Returns(mock.Object);
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
            this.dataContextMock.SetupGet(property => property.Contents).Throws(exception);

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
            this.dataContextMock.SetupGet(property => property.Contents).Returns(mock.Object);
            var expected = await sut.GetContent(x => x.PublicId == content.PublicId);

            //Then
            Assert.Equal(expected, content);
        }
    }
}
