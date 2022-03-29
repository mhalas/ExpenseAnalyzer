using Api.HandlerResponses;
using MediatR;

namespace Api.HandlerRequests
{
    public class UpdateItemRequest: IRequest<GeneralResponse>
    {
        public UpdateItemRequest(int userId, int itemId, object updatedObject)
        {
            UserId = userId;
            ItemId = itemId;
            UpdatedObject = updatedObject;
        }

        public int UserId { get; }
        public int ItemId { get; }
        public object UpdatedObject { get; }
    }
}
