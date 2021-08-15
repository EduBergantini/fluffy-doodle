
using Blog.Domain.Contents.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Application.Contents.Protocols
{
    public interface IGetContentRepository
    {
        Task<Content> GetContent(Expression<Func<Content, bool>> predicate);
    }
}
