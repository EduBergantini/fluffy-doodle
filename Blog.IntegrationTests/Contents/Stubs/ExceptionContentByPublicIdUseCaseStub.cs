using System;
using System.Threading.Tasks;

using Blog.Domain.Contents.Entities;
using Blog.Domain.Contents.UseCases.Contents;


namespace Blog.IntegrationTests.Contents.Stubs
{
    public class ExceptionContentByPublicIdUseCaseStub : IGetContentByPublicIdUseCase
    {
        public Task<Content> GetContent(string publicId)
        {
            throw new Exception("Stub Exception");
        }
    }
}
