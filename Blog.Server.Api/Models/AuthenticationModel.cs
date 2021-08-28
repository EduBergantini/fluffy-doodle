using System.ComponentModel.DataAnnotations;

namespace Blog.Server.Api.Models
{
    public class AuthenticationModel
    {
        [Required(AllowEmptyStrings =false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string PlainTextPassword { get; set; }
    }
}
