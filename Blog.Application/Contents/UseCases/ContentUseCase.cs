using System.Threading.Tasks;
using System.Collections.Generic;

using Blog.Domain.Contents.Entities;

using Blog.Application.Contents.Protocols;
using Blog.Domain.Contents.UseCases.Contents;

namespace Blog.Application.Contents.UseCases
{
    public class ContentUseCase : IGetContentListUseCase
    {
        private readonly IGetContentListRepository getContentListRepository;

        public ContentUseCase(IGetContentListRepository getContentListRepository)
        {
            this.getContentListRepository = getContentListRepository;
        }
        
        public Task<IEnumerable<Content>> GetContentList()
        {
            return this.getContentListRepository.GetContentList();
        }
        
    }
}