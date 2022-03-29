using Api.Filters;
using Api.HandlerRequests;
using Api.HttpRequests;
using AutoMapper;
using DataAccess.Contracts.Model;
using DataAccess.EntityFramework.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/Users/{userId:int}/Categories/{categoryId:int}/Values")]
    [ApiController]
    public class UserCategoryValueController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserCategoryValueController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] int categoryId)
        {
            var filter = new CategoryReferenceFilter<UserCategoryValue>(categoryId);

            var result = await _mediator.Send(new GetItemsRequest<UserCategoryValue>(filter));

            return Ok(result);
        }

        [HttpGet("{itemId:int}")]
        public async Task<IActionResult> GetSingle([FromRoute] int categoryId, [FromRoute] int itemId)
        {
            var filter = new CategoryReferenceFilter<UserCategoryValue>(categoryId);

            var result = await _mediator.Send(new GetSingleItemRequest<UserCategoryValue>(filter, itemId));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] int userId, [FromRoute] int categoryId, [FromBody] NewCategoryValueRequest request)
        {
            var newObject = _mapper.Map<UserCategoryValue>(request);
            newObject.UserCategoryId = categoryId;

            var result = await _mediator.Send(new CreateItemRequest(newObject));

            return Created(Url.Action(nameof(GetSingle), new { categoryId = categoryId, objectId = newObject.Id }), newObject);
        }

        [HttpDelete("{itemId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int categoryId, [FromRoute] int itemId)
        {
            var filter = new CategoryReferenceFilter<UserCategoryValue>(categoryId);

            var result = await _mediator.Send(new RemoveItemRequest<UserCategoryValue>(filter, itemId));

            return Ok(result);
        }
    }
}
