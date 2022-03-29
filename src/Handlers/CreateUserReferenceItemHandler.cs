using Api.HandlerRequests;
using Api.HandlerResponses;
using AutoMapper;
using DataAccess.Contracts.References;
using DataAccess.Contracts.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers
{
    public class CreateUserReferenceItemHandler<TItem> : IRequestHandler<CreateUserReferenceItemRequest, CreatedResponse>
        where TItem : class, IId, IUserReference, new()
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateUserReferenceItemHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<CreatedResponse> Handle(CreateUserReferenceItemRequest request, CancellationToken cancellationToken)
        {
            var newObject = _mapper.Map<TItem>(request.Item);
            newObject.UserId = request.UserId;

            return await _mediator.Send(new CreateItemRequest(newObject));
        }
    }
}
