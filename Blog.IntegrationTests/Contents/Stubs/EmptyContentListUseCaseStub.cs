using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Blog.Domain.Contents.Entities;
using Blog.Domain.Contents.UseCases.Contents;

namespace Blog.IntegrationTests.Contents.Stubs
{
    public class EmptyContentListUseCaseStub : IGetContentListUseCase
    {
        public Task<IEnumerable<Content>> GetContentList()
        {
            return Task.FromResult(new List<Content>().AsEnumerable());
        }
    }
}
