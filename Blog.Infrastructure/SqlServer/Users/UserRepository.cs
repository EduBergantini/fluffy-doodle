using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Blog.Application.Users.Protocols;
using Blog.Domain.Users.Entities;
using Blog.Infrastructure.SqlServer.Contexts;

namespace Blog.Infrastructure.SqlServer.Users
{
    public class UserRepository : IGetUserByEmailRepository
    {
        private readonly ContentDataContext dataContext;

        public UserRepository(ContentDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public Task<User> GetByEmail(string email)
        {
            var user = this.dataContext.Users.SingleOrDefaultAsync(user => user.Email == email);
            return user;
        }
    }
}
