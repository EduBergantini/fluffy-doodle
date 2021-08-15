using System.Threading.Tasks;
using System.Collections.Generic;

using Blog.Domain.Contents.Entities;


namespace Blog.Domain.Contents.UseCases.Contents
{
    public interface IGetContentListUseCase
    {
        Task<IEnumerable<Content>> GetContentList();
    }
}