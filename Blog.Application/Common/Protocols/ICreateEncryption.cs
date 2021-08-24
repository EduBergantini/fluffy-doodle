using System.Threading.Tasks;

using Blog.Domain.Users.Models;

namespace Blog.Application.Common.Protocols
{
    public interface ICreateEncryption
    {
        Task<AuthenticationTokenModel> CreateToken(int userId, int roleId);
    }
}
