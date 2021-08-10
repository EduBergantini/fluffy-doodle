using Blog.Domain.Contents.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController : ControllerBase
    {
        private readonly IGetContentListUseCase getContentListUseCase;

        public ContentsController(IGetContentListUseCase getContentListUseCase)
        {
            this.getContentListUseCase = getContentListUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var contents = await this.getContentListUseCase.GetContentList();
            return Ok(contents);

        }
    }
}
