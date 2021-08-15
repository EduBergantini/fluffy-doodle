using System.Threading.Tasks;
using System.Collections.Generic;

using Blog.Domain.Contents.Entities;

using Blog.Application.Contents.Protocols;
using Blog.Domain.Contents.UseCases.Contents;

namespace Blog.Application.Contents.UseCases
{
    public class ContentUseCase : IGetContentListUseCase, IGetContentByPublicIdUseCase
    {
        private readonly IGetContentListRepository getContentListRepository;
        private readonly IGetContentRepository getContentRepository;

        public ContentUseCase(
            IGetContentListRepository getContentListRepository,
            IGetContentRepository getContentRepository
        )
        {
            this.getContentListRepository = getContentListRepository;
            this.getContentRepository = getContentRepository;
        }

        public Task<Content> GetContent(string publicId)
        {
            return this.getContentRepository.GetContent(content => content.PublicId == publicId);
        }

        public Task<IEnumerable<Content>> GetContentList()
        {
            return this.getContentListRepository.GetContentList();
        }
        
    }
}