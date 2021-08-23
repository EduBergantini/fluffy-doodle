using System;

using Microsoft.EntityFrameworkCore;
using Moq;

using Blog.Infrastructure.SqlServer.Contexts;

namespace Blog.UnitTests.Common
{
    public abstract class CommonRepositoryTest
    {
        public Mock<ContentDataContext> DataContextMock { get; }

        public CommonRepositoryTest()
        {
            var inMemoryOptions = new DbContextOptionsBuilder<ContentDataContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;
            this.DataContextMock = new Mock<ContentDataContext>(MockBehavior.Default, new[] { inMemoryOptions });
        }
    }
}
