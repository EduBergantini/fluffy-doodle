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

namespace Blog.UnitTests.Contents
{
    public class ContentRepositoryTests
    {
        private readonly Mock<ContentDataContext> dataContextMock;

        public ContentRepositoryTests()
        {
            var inMemoryOptions = new DbContextOptionsBuilder<ContentDataContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;
            this.dataContextMock = new Mock<ContentDataContext>(MockBehavior.Default, new[] { inMemoryOptions });
        }

        [Fact]
        public async Task ShouldReturnListOfContentOnSuccess()
        {
            //Given
            var contents = new List<Content>();
            var mock = contents.AsQueryable().BuildMockDbSet();
            var sut = new ContentRepository(this.dataContextMock.Object);

            //When
            this.dataContextMock.SetupGet(property => property.Contents).Returns(mock.Object);
            var expected = await sut.GetContentList();

            //Then
            Assert.Equal(expected, contents);
        }
    }
}
