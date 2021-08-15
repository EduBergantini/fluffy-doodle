using System.Threading.Tasks;

using Blog.Domain.Contents.Entities;


namespace Blog.Domain.Contents.UseCases.Contents
{
    public interface IGetContentByPublicIdUseCase
    {
        Task<Content> GetContent(string publicId);
    }
}
