
namespace Blog.Domain.Users.Models
{
    public class AuthenticationTokenModel
    {
        public string Token { get; set; }
        public int ExpireInMs { get; set; }
    }
}
