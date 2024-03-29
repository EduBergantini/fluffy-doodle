﻿using System;

using Blog.Domain.Roles.Entities;
using Blog.Domain.Status.Entities;

namespace Blog.Domain.Users.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string PublicId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }

        public int StatusId { get; set; }
        public ApplicationStatus Status { get; set; }

        public int RoleId { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
