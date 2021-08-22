using System.Threading.Tasks;

using SimpleHashing.Net;

using Blog.Application.Common.Protocols;


namespace Blog.Infrastructure.Common.Adapters
{
    public class SimpleHashingAdapter : ICreateHash, ICompareHash
    {
        private readonly ISimpleHash simpleHash;

        public SimpleHashingAdapter(ISimpleHash simpleHash)
        {
            this.simpleHash = simpleHash;
        }

        public Task<bool> CompareHash(string plainValue, string hashedValue)
        {
            var result = this.simpleHash.Verify(plainValue, hashedValue);
            return Task.FromResult(result);
        }

        public Task<string> CreateHash(string value, int interations)
        {
            var hashedPassword = this.simpleHash.Compute(value, interations);
            return Task.FromResult(hashedPassword);
        }
    }
}
