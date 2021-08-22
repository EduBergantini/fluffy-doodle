using System.Threading.Tasks;

using SimpleHashing.Net;

using Blog.Application.Common.Protocols;


namespace Blog.Infrastructure.Common.Adapters
{
    public class SimpleHashingAdapter : ICreateHash
    {
        private readonly ISimpleHash simpleHash;

        public SimpleHashingAdapter(ISimpleHash simpleHash)
        {
            this.simpleHash = simpleHash;
        }

        public Task<string> CreateHash(string value)
        {
            var hashedPassword = this.simpleHash.Compute(value);
            return Task.FromResult(hashedPassword);
        }
    }
}
