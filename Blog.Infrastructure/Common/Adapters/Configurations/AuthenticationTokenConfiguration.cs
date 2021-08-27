using System;

namespace Blog.Infrastructure.Common.Adapters.Configurations
{
    public class AuthenticationTokenConfiguration
    {
        public string SecretKey { get; }
        public int TotalDaysToExpire { get; }
        public DateTime now;

        public AuthenticationTokenConfiguration(string secretKey, int totalDaysToExpire = 7)
        {
            SecretKey = secretKey;
            TotalDaysToExpire = totalDaysToExpire;
            this.ExpirationDate = DateTime.UtcNow.AddDays(this.TotalDaysToExpire);
            this.now = DateTime.UtcNow;
        }

        public DateTime ExpirationDate { get; }

        public int ExpireInMs
        {
            get
            {
                return (int)ExpirationDate.Subtract(this.now).TotalMilliseconds;
            }
        }
    }
}
