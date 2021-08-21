using System.Threading.Tasks;

using Blog.Domain.Users.Entities;

namespace Blog.Domain.Users.UseCases
{
    public interface IAuthenticateUseCase
    {
        Task<User> Authenticate(string email, string password);
    }
}
