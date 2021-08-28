using System.Threading.Tasks;

using Blog.Domain.Users.Models;
using Blog.Domain.Users.UseCases;

namespace Blog.IntegrationTests.Users.Stubs
{
    public class AuthenticateUseCaseStub : IAuthenticateUseCase
    {
        public Task<AuthenticationTokenModel> Authenticate(string email, string password)
        {
            return Task.FromResult(new AuthenticationTokenModel
            {
                Token = "any_token",
                ExpireInMs = 1000
            });
        }
    }
}
