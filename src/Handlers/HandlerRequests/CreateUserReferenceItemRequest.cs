using Api.HandlerResponses;
using MediatR;

namespace Api.HandlerRequests
{
    public class CreateUserReferenceItemRequest: IRequest<CreatedResponse>
    {
        public CreateUserReferenceItemRequest(int userId, object item)
        {
            UserId = userId;
            Item = item;
        }

        public int UserId { get; }
        public object Item { get; }
    }
}
