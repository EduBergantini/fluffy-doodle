using System;
using System.Threading.Tasks;

using Xunit;
using Moq;

using Blog.Application.Contents.Protocols;
using Blog.Application.Contents.UseCases;
using System.Collections.Generic;
using Blog.Domain.Contents.Entities;
using Blog.UnitTests.Contents.Fakes;

namespace Blog.UnitTests.Contents
{
    public class ContentUseCaseTests
    {
        private readonly Mock<IGetContentListRepository> getContentListRepositoryMock;
        private readonly Mock<IGetContentRepository> getContentRepositoryMock;
        private readonly ContentUseCase sut;

        public ContentUseCaseTests()
        {
            this.getContentListRepositoryMock = new Mock<IGetContentListRepository>(MockBehavior.Default);
            this.getContentRepositoryMock = new Mock<IGetContentRepository>(MockBehavior.Default);
            this.sut = new ContentUseCase(
                this.getContentListRepositoryMock.Object,
                this.getContentRepositoryMock.Object
            );
        }

        [Fact]
        public async Task ShouldReturnListOfContentsOnSuccess()
        {
            //Given
            IEnumerable<Content> actual = ContentFake.GetContentList();

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

        [Fact]
        public async Task ShouldReturnAContentOnSuccess()
        {
            //Given
            Content actual = ContentFake.GetContent();

            //When
            this.getContentRepositoryMock
                .Setup(method => method.GetContent(It.IsAny<Func<Content, bool>>()))
                .ReturnsAsync(() => actual);

            var expected = await this.sut.GetContent(actual.PublicId);

            //Then
            Assert.Equal(expected, actual);
        }
    }
}