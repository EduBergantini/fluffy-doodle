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
        private readonly ICreateEncryption createEncryption;

        public UserUseCase(
            IGetUserByEmailRepository getUserByEmailRepository,
            ICompareHash compareHash,
            ICreateEncryption createEncryption
        )
        {
            this.getUserByEmailRepository = getUserByEmailRepository;
            this.compareHash = compareHash;
            this.createEncryption = createEncryption;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await this.getUserByEmailRepository.GetByEmail(email);
            if (user == null) throw new UserNotFoundException();
            var compareResult = await this.compareHash.CompareHash(password, user.Password);
            if (!compareResult) throw new InvalidPasswordException();
            await this.createEncryption.CreateToken(user.Id, user.RoleId);
            return user;
        }
    }
}
