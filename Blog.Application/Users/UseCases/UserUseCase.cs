using System.Threading.Tasks;
using Blog.Application.Common.Protocols;
using Blog.Application.Users.Protocols;
using Blog.Domain.Users.Entities;
using Blog.Domain.Users.Errors;
using Blog.Domain.Users.UseCases;

namespace Blog.Application.Users.UseCases
{
    public class UserUseCase : IAuthenticateUseCase
    {
        private readonly IGetUserByEmailRepository getUserByEmailRepository;
        private readonly ICreateHash encrypter;

        public UserUseCase(
            IGetUserByEmailRepository getUserByEmailRepository,
            ICreateHash encrypter
        )
        {
            this.getUserByEmailRepository = getUserByEmailRepository;
            this.encrypter = encrypter;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await this.getUserByEmailRepository.GetByEmail(email);
            if (user == null) throw new UserNotFoundException();
            var encryptedPassword = await this.encrypter.CreateHash(password);
            if (encryptedPassword != user.Password) throw new InvalidPasswordException();
            return user;
        }
    }
}
