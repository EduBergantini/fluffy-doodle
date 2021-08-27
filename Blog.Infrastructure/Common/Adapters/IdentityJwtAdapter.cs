using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Tokens;

using Blog.Application.Common.Protocols;
using Blog.Domain.Users.Models;
using Blog.Infrastructure.Common.Adapters.Configurations;

namespace Blog.Infrastructure.Common.Adapters
{
    public class IdentityJwtAdapter : ICreateEncryption
    {
        private readonly AuthenticationTokenConfiguration authenticationTokenConfiguration;

        public IdentityJwtAdapter(AuthenticationTokenConfiguration authenticationTokenConfiguration)
        {
            this.authenticationTokenConfiguration = authenticationTokenConfiguration;
        }

        public Task<AuthenticationTokenModel> CreateToken(int userId, int roleId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes("[SECRET USED TO SIGN AND VERIFY JWT TOKENS, IT CAN BE ANY STRING]");
            var key = Encoding.UTF32.GetBytes(this.authenticationTokenConfiguration.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.PrimarySid, userId.ToString()),
                    new Claim(ClaimTypes.Role, roleId.ToString()),
                }),
                Expires = this.authenticationTokenConfiguration.ExpirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var authModel = new AuthenticationTokenModel
            {
                Token = tokenHandler.WriteToken(token),
                ExpireInMs = this.authenticationTokenConfiguration.ExpireInMs
            };
            return Task.FromResult(authModel);
        }
    }
}
