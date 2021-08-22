using System.Threading.Tasks;

namespace Blog.Application.Users.Protocols
{
    public interface IEncrypter
    {
        Task<string> Encrypt(string value);
    }
}
