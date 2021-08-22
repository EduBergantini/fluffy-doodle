using System.Threading.Tasks;

namespace Blog.Application.Common.Protocols
{
    public interface IApplicationHasher
    {
        Task<string> Hash(string value);
    }
}
