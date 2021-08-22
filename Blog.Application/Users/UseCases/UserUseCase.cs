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
        private readonly ICompareHash compareHash;

        public UserUseCase(
            IGetUserByEmailRepository getUserByEmailRepository,
            ICompareHash compareHash
        )
        {
            this.getUserByEmailRepository = getUserByEmailRepository;
            this.compareHash = compareHash;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await this.getUserByEmailRepository.GetByEmail(email);
            if (user == null) throw new UserNotFoundException();
            var compareResult = await this.compareHash.CompareHash(password, user.Password);
            if (!compareResult) throw new InvalidPasswordException();
            return user;
        }
    }
}
