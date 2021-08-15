using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Blog.Application.Contents.Protocols;
using Blog.Domain.Contents.Entities;
using Blog.Infrastructure.SqlServer.Contexts;
using System.Linq.Expressions;
using System;

namespace Blog.Infrastructure.SqlServer.Contents
{
    public class ContentRepository : IGetContentListRepository, IGetContentRepository
    {
        private readonly ContentDataContext dataContext;

        public ContentRepository(ContentDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Task<Content> GetContent(Expression<Func<Content, bool>> predicate)
        {
            return this.dataContext.Contents.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Content>> GetContentList()
        {
            var contents = await this.dataContext.Contents.ToListAsync();
            return contents;
        }
    }
}
