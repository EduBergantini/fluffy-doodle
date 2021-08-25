using System.Threading.Tasks;

using Blog.Domain.Users.Models;

namespace Blog.Domain.Users.UseCases
{
    public interface IAuthenticateUseCase
    {
        Task<AuthenticationTokenModel> Authenticate(string email, string password);
    }
}
