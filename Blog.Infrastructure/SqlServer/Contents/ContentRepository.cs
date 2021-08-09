using Blog.Application.Contents.Protocols;
using Blog.Domain.Contents.Entities;
using Blog.Infrastructure.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Infrastructure.SqlServer.Contents
{
    public class ContentRepository : IGetContentListRepository
    {
        private readonly ContentDataContext dataContext;

        public ContentRepository(ContentDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<IEnumerable<Content>> GetContentList()
        {
            var contents = await this.dataContext.Contents.ToListAsync();
            return contents;
        }
    }
}
