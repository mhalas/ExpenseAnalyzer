using Api.Filters;
using Api.HandlerRequests;
using Api.HttpRequests;
using DataAccess.Contracts.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/Users/{userId:int}/Categories")]
    [ApiController]
    public class UserCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] int userId)
        {
            var filter = new UserReferenceFilter<UserCategory>(userId);

            var result = await _mediator.Send(new GetItemsRequest<UserCategory>(filter));

            return Ok(result);
        }

        [HttpGet("{itemId:int}")]
        public async Task<IActionResult> GetSingle([FromRoute] int userId, [FromRoute] int itemId)
        {
            var filter = new UserReferenceFilter<UserCategory>(userId);

            var result = await _mediator.Send(new GetSingleItemRequest<UserCategory>(filter, itemId));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] int userId, [FromBody] NewCategoryRequest item)
        {
            var result = await _mediator.Send(new CreateUserReferenceItemRequest(userId, item));

            return Created(Url.Action(nameof(GetSingle), new { userId = userId, objectId = result.Item.Id }), (object)result.Item);
        }

        [HttpPut("{itemId:int}")]
        public async Task<IActionResult> Update([FromRoute] int userId, [FromRoute] int itemId, [FromBody] UpdatedCategoryRequest request)
        {
            var result = await _mediator.Send(new UpdateItemRequest(userId, itemId, request));

            return Ok(result);
        }

        [HttpDelete("{itemId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int userId, [FromRoute] int itemId)
        {
            var filter = new UserReferenceFilter<UserCategory>(userId);

            var result = await _mediator.Send(new RemoveItemRequest<UserCategory>(filter, itemId));

            return Ok(result);
        }
    }
}
