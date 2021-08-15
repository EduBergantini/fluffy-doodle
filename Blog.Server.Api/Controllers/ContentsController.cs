using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Blog.Domain.Contents.UseCases.Contents;

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
            try
            {
                var contents = await this.getContentListUseCase.GetContentList();
                if (!contents?.Any() ?? true) return NoContent();
                return Ok(contents);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }

        }
    }
}
