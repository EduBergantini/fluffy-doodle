using System.Threading.Tasks;

using Xunit;
using Moq;

using Blog.Application.Contents.Protocols;
using Blog.Application.Contents.UseCases;
using System.Collections.Generic;
using Blog.Domain.Contents.Entities;

namespace Blog.UnitTests.Contents
{
    public class ContentUseCaseTests
    {
        [Fact]
        public async Task ShouldReturnListOfContentsWhen()
        {
            //Given
            IEnumerable<Content> actual = new List<Content>();
            var getContentListRepositoryMock = new Mock<IGetContentListRepository>(MockBehavior.Default);
            var sut = new ContentUseCase(getContentListRepositoryMock.Object);

            //When
            //mock.Setup(foo => foo.DoSomethingAsync()).Returns(async () => 42);
            getContentListRepositoryMock.Setup(method => method.GetContentList()).ReturnsAsync(() => actual);
            var expected = await sut.GetContentList();

            //Then
            Assert.Equal(expected, actual);

        }
    }
}