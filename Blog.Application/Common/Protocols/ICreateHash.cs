using System.Threading.Tasks;

namespace Blog.Application.Common.Protocols
{
    public interface ICreateHash
    {
        Task<string> CreateHash(string value);
    }
}
