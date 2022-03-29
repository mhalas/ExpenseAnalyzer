using Api.Filters;
using Api.HandlerRequests;
using Api.HttpRequests;
using DataAccess.Contracts.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/Users/{userId:int}/Configuration")]
    [ApiController]
    [Authorize]
    public class UserConfigurationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserConfigurationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] int userId)
        {
            var filter = new UserReferenceFilter<UserConfiguration>(userId);

            var result = await _mediator.Send(new GetItemsRequest<UserConfiguration>(filter));

            return Ok(result);
        }

        [HttpGet("{itemId:int}")]
        public async Task<IActionResult> GetSingle([FromRoute] int userId, [FromRoute] int itemId)
        {
            var filter = new UserReferenceFilter<UserConfiguration>(userId);

            var result = await _mediator.Send(new GetSingleItemRequest<UserConfiguration>(filter, itemId));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] int userId, [FromRoute] int categoryId, [FromBody] NewUserConfigurationRequest request)
        {
            var result = await _mediator.Send(new CreateUserReferenceItemRequest(userId, request));

            return Created(Url.Action(nameof(GetSingle), new { userId = userId, objectId = result.Item.Id }), (object)result.Item);
        }

        [HttpDelete("{itemId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int userId, [FromRoute] int itemId)
        {
            var filter = new UserReferenceFilter<UserConfiguration>(userId);

            var result = await _mediator.Send(new RemoveItemRequest<UserConfiguration>(filter, itemId));

            return Ok(result);
        }
    }
}
