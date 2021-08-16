
using Blog.Domain.Contents.Entities;
using Blog.Domain.Contents.UseCases.Contents;
using System.Threading.Tasks;

namespace Blog.IntegrationTests.Contents.Stubs
{
    public class ContentByPublicIdUseCaseStub : IGetContentByPublicIdUseCase
    {
        public Task<Content> GetContent(string publicId)
        {
            return Task.FromResult(new Content { Id = 1, PublicId = publicId, FeaturedImage = "/img/feature-1.png", Title = "Titulo 1", Subtitle = "Subtitulo 1" });
        }
    }
}
