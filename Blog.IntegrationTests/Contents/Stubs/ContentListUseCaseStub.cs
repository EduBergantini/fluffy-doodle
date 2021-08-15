using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Blog.Domain.Contents.Entities;
using Blog.Domain.Contents.UseCases.Contents;

namespace Blog.IntegrationTests.Contents.Stubs
{
    public class ContentListUseCaseStub : IGetContentListUseCase
    {
        public Task<IEnumerable<Content>> GetContentList()
        {
            var contents = new List<Content>();
            contents.Add(new Content { Id = 1, FeaturedImage = "/img/feature-1.png", Title = "Titulo 1", Subtitle = "Subtitulo 1" });
            contents.Add(new Content { Id = 2, FeaturedImage = "/img/feature-2.png", Title = "Titulo 2", Subtitle = "Subtitulo 2" });
            contents.Add(new Content { Id = 3, FeaturedImage = "/img/feature-3.png", Title = "Titulo 3", Subtitle = "Subtitulo 3" });
            return Task.FromResult(contents.AsEnumerable());
        }
    }
}
