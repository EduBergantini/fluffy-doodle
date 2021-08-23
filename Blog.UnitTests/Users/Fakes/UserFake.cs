using System;
using System.Collections.Generic;

using Bogus;

using Blog.Domain.Users.Entities;
using Blog.Domain.Roles.Entities;
using Blog.Domain.Status.Entities;

namespace Blog.UnitTests.Users.Fakes
{
    public class UserFake
    {
        private readonly Faker faker;

        private UserFake()
        {
            this.faker = new Faker();
        }

        private User GetFakeUser()
        {
            return new User
            {
                Id = this.faker.Internet.Random.Int(min: 0),
                PublicId = this.faker.Random.Uuid().ToString(),
                FullName = this.faker.Name.FullName(),
                Email = this.faker.Internet.Email(),
                Password = this.faker.Internet.Password(),
                RoleId = 1,
                Role = new ApplicationRole { Id = 1, Description = "Administrador"},
                StatusId = 1,
                Status = new ApplicationStatus { Id = 1, Description = "Ativo"},
                RegisterDate = DateTime.UtcNow
            };
        }

        internal static IEnumerable<User> GetUserList(int count = 3)
        {
            var users = new List<User>();
            for (int i = 0; i < count; i++)
            {
                var userFake = new UserFake();
                users.Add(userFake.GetFakeUser());
            }
            return users;
        }

        internal static User GetUser(string password)
        {
            var fakeUser = new UserFake();
            var user = fakeUser.GetFakeUser();
            user.Password = password;
            return user;
        }
    }
}
