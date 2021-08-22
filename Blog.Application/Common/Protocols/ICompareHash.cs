using System.Threading.Tasks;

namespace Blog.Application.Common.Protocols
{
    public interface ICompareHash
    {
        Task<bool> CompareHash(string plainValue, string hashedValue);
    }
}
