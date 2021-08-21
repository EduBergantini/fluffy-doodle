using System;

using Bogus;

using Blog.Domain.Users.Entities;

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
                FullName = this.faker.Name.FullName(),
                Email = this.faker.Internet.Email(),
                Password = this.faker.Internet.Password(),
                RegisterDate = DateTime.UtcNow
            };
        } 

        public static User GetUser()
        {
            var fakeUser = new UserFake();
            return fakeUser.GetFakeUser();
        }
    }
}
