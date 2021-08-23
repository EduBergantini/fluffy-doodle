
using Blog.Domain.Users.Entities;
using System.Collections.Generic;

namespace Blog.Domain.Status.Entities
{
    public class ApplicationStatus
    {
        public ApplicationStatus()
        {
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
