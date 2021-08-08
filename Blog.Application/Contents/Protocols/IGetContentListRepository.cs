using System.Collections.Generic;
using System.Threading.Tasks;

using Blog.Domain.Contents.Entities;

namespace Blog.Application.Contents.Protocols
{
    public interface IGetContentListRepository
    {
         Task<IEnumerable<Content>> GetContentList();
    }
}