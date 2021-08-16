
using Blog.Domain.Contents.Entities;
using Blog.Domain.Contents.UseCases.Contents;
using System.Threading.Tasks;

namespace Blog.IntegrationTests.Contents.Stubs
{
    public class NullContentByPublicIdUseCaseStub : IGetContentByPublicIdUseCase
    {
        public Task<Content> GetContent(string publicId)
        {
            Content content = null;
            return Task.FromResult(content);
        }
    }
}
