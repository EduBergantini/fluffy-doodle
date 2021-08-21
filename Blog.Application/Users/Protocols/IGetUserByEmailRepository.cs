using System.Threading.Tasks;

using Blog.Domain.Users.Entities;

namespace Blog.Application.Users.Protocols
{
    public interface IGetUserByEmailRepository
    {
        Task<User> GetByEmail(string email);
    }
}
