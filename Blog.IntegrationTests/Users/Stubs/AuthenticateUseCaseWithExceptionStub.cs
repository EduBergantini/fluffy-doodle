using System;
using System.Threading.Tasks;

using Blog.Domain.Users.Models;
using Blog.Domain.Users.UseCases;

namespace Blog.IntegrationTests.Users.Stubs
{
    public class AuthenticateUseCaseWithExceptionStub : IAuthenticateUseCase
    {
        private readonly Exception exception;

        public AuthenticateUseCaseWithExceptionStub(Exception exception)
        {
            this.exception = exception;
        }

        public Task<AuthenticationTokenModel> Authenticate(string email, string password)
        {
            throw exception;
        }
    }
}
