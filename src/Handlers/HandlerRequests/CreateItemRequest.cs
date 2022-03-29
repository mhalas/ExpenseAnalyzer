using Api.HandlerResponses;
using DataAccess.Contracts.Shared;
using MediatR;

namespace Api.HandlerRequests
{
    public class CreateItemRequest : IRequest<CreatedResponse>
    {
        public CreateItemRequest(IId newObject)
        {
            NewObject = newObject;
        }

        public IId NewObject { get; }
    }
}
