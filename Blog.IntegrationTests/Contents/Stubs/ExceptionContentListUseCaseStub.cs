using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Blog.Domain.Contents.Entities;
using Blog.Domain.Contents.UseCases.Contents;

namespace Blog.IntegrationTests.Contents.Stubs
{
    public class ExceptionContentListUseCaseStub : IGetContentListUseCase
    {
        public Task<IEnumerable<Content>> GetContentList()
        {
            throw new Exception("Stub Exception");
        }
    }
}
