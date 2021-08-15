using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Moq;
using Xunit;

using Blog.Domain.Contents.Entities;
using Blog.Server.Api.Controllers;
using Blog.UnitTests.Contents.Fakes;
using Blog.Domain.Contents.UseCases.Contents;

namespace Blog.UnitTests.Contents
{
    public class ContentsControllerTests
    {
        private readonly Mock<IGetContentListUseCase> mockedGetContentuseCase;
        private readonly Mock<IGetContentByPublicIdUseCase> mockedGetContentByPublicIdUseCase;
        private readonly ContentsController sut;

        public ContentsControllerTests()
        {
            this.mockedGetContentuseCase = new Mock<IGetContentListUseCase>(MockBehavior.Strict);
            this.mockedGetContentByPublicIdUseCase = new Mock<IGetContentByPublicIdUseCase>(MockBehavior.Strict);
            this.sut = new ContentsController(
                this.mockedGetContentuseCase.Object,
                this.mockedGetContentByPublicIdUseCase.Object
            );
        }

        [Fact]
        public async Task ShouldReturnContentListWhenGetReturnsSuccess()
        {
            //Given
            var expected = ContentFake.GetContentList();

            //When
            this.mockedGetContentuseCase.Setup(method => method.GetContentList()).ReturnsAsync(() => expected);
            var httpResponse = await this.sut.Get();

            //Then
            var okResult = Assert.IsType<OkObjectResult>(httpResponse);
            Assert.Equal(200, okResult.StatusCode);

            var model = Assert.IsAssignableFrom<IEnumerable<Content>>(okResult.Value);
            Assert.Equal(expected, model);
        }

        [Fact]
        public async Task ShouldReturnNoContentWhenGetReturnsSuccessWithNoData()
        {
            //Given
            IEnumerable<Content> expected = null;

            //When
            this.mockedGetContentuseCase.Setup(method => method.GetContentList()).ReturnsAsync(() => expected);
            var httpResponse = await this.sut.Get();

            //Then
            var noContentResult = Assert.IsType<NoContentResult>(httpResponse);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnInternalServerErrorWhenIGetContentListUseCaseThrows()
        {
            //Given
            var expected = new Exception();

            //When
            this.mockedGetContentuseCase.Setup(method => method.GetContentList()).ThrowsAsync(expected);
            var httpResponse = await this.sut.Get();

            //Then
            var statusCodeResult = Assert.IsType<ObjectResult>(httpResponse);
            Assert.Equal(500, statusCodeResult.StatusCode);

            var model = Assert.IsAssignableFrom<Exception>(statusCodeResult.Value);
            Assert.Equal(expected, model);
        }

        [Fact]
        public async Task ShouldReturnSContentWhenGetWithPublicIdParameterReturnsSuccess()
        {
            //Given
            var expected = ContentFake.GetContent();

            //When
            this.mockedGetContentByPublicIdUseCase
                .Setup(method => method.GetContent(It.IsAny<string>()))
                .ReturnsAsync(() => expected);

            var httpResponse = await this.sut.Get(expected.PublicId);

            //Then
            var okResult = Assert.IsType<OkObjectResult>(httpResponse);
            Assert.Equal(200, okResult.StatusCode);

            var model = Assert.IsAssignableFrom<Content>(okResult.Value);
            Assert.Equal(expected, model);
        }
    }
}
