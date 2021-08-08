using System.Threading.Tasks;
using System.Collections.Generic;

using Blog.Domain.Contents.Entities;


namespace Blog.Domain.Contents.UseCases
{
    public interface IGetContentListUseCase
    {
        Task<IEnumerable<Content>> GetContentList();
    }
}