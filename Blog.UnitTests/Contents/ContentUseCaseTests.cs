using System;
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
        private readonly Mock<IGetContentListRepository> getContentListRepositoryMock;
        private readonly ContentUseCase sut;

        public ContentUseCaseTests()
        {
            this.getContentListRepositoryMock = new Mock<IGetContentListRepository>(MockBehavior.Default);
            this.sut = new ContentUseCase(this.getContentListRepositoryMock.Object);
        }

        [Fact]
        public async Task ShouldReturnListOfContentsOnSuccess()
        {
            //Given
            IEnumerable<Content> actual = new List<Content>();

            //When
            this.getContentListRepositoryMock.Setup(method => method.GetContentList()).ReturnsAsync(() => actual);
            var expected = await this.sut.GetContentList();

            //Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowWhenGetContentListRepositoryThrows()
        {
            //Given
            var exception = new Exception();
            
            //When
            this.getContentListRepositoryMock.Setup(method => method.GetContentList()).ThrowsAsync(exception);
            
            //Then
            Assert.ThrowsAsync<Exception>(() => this.sut.GetContentList());
        }
    }
}