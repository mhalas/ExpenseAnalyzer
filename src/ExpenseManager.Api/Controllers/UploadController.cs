using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/Users/{userId:int}/Upload")]
    [ApiController]
    public class UploadController: ControllerBase
    {
        private readonly IMediator _mediator;

        public UploadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("UploadCategories")]
        public async Task<IActionResult> UploadCategories([FromRoute] int userId, [FromBody] HttpRequests.UploadCategoriesRequest request)
        {
            var result = await _mediator.Send(new HandlerRequests.UploadCategoriesRequest(userId, request.CategoriesDictionary));

            return Ok(result);
        }
    }
}
