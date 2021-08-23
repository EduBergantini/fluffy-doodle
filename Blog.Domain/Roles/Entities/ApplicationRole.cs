
using Blog.Domain.Users.Entities;
using System.Collections.Generic;

namespace Blog.Domain.Roles.Entities
{
    public class ApplicationRole
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
