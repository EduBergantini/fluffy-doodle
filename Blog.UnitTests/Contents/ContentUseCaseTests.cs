using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xunit;
using Moq;
using Neleus.LambdaCompare;


using Blog.Application.Contents.Protocols;
using Blog.Application.Contents.UseCases;
using Blog.Domain.Contents.Entities;
using Blog.UnitTests.Contents.Fakes;
using System.Linq.Expressions;

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
                .Setup(method => method.GetContent(It.IsAny<Expression<Func<Content, bool>>>()))
                .ReturnsAsync(() => actual);

            var expected = await this.sut.GetContent(actual.PublicId);

            //Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ShouldCallGetContentRepositoryWithCorrectPublicId()
        {
            //Given
            Content content = ContentFake.GetContent();
            Expression<Func<Content, bool>> actual = null;
            Expression<Func<Content, bool>> expected = x => x.PublicId == content.PublicId;

            //When
            this.getContentRepositoryMock
                .Setup(method => method.GetContent(It.IsAny<Expression<Func<Content, bool>>>()))
                .Callback<Expression<Func<Content, bool>>>((getContent) => actual = getContent)
                .ReturnsAsync(() => content);

            var result = await this.sut.GetContent(content.PublicId);

            //Then
            Assert.NotNull(actual);
            this.getContentRepositoryMock
                .Verify(c => c.GetContent(It.Is<Expression<Func<Content, bool>>>
                (criteria => Lambda.Eq(criteria, expected))), Times.Once);
        }
    }
}