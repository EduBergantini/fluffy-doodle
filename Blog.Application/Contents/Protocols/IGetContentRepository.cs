
using Blog.Domain.Contents.Entities;
using System;
using System.Threading.Tasks;

namespace Blog.Application.Contents.Protocols
{
    public interface IGetContentRepository
    {
        Task<Content> GetContent(Func<Content, bool> predicate);
    }
}
