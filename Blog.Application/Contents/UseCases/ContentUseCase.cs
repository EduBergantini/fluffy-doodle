using System.Threading.Tasks;
using System.Collections.Generic;

using Blog.Domain.Contents.Entities;
using Blog.Domain.Contents.UseCases;

using Blog.Application.Contents.Protocols;

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