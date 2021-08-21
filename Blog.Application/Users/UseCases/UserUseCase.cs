using System;
using System.Threading.Tasks;

using Blog.Application.Users.Protocols;
using Blog.Domain.Users.Entities;
using Blog.Domain.Users.UseCases;

namespace Blog.Application.Users.UseCases
{
    public class UserUseCase : IAuthenticateUseCase
    {
        private readonly IGetUserByEmailRepository getUserByEmailRepository;

        public UserUseCase(IGetUserByEmailRepository getUserByEmailRepository)
        {
            this.getUserByEmailRepository = getUserByEmailRepository;
        }

        public Task<User> Authenticate(string email, string password)
        {
            this.getUserByEmailRepository.GetByEmail(email);
            return Task.FromResult((User)null);
        }
    }
}
