using Api.HandlerResponses;
using DataAccess.Contracts.Shared;
using DataAccess.EntityFramework.Filters;
using MediatR;

namespace Api.HandlerRequests
{
    public class RemoveItemRequest<TItemResponse> : IRequest<GeneralResponse>
        where TItemResponse : class, IId
    {
        public RemoveItemRequest(IFilter<TItemResponse> filter, int itemId)
        {
            Filter = filter;
            ItemId = itemId;
        }

        public IFilter<TItemResponse> Filter { get; }
        public int ItemId { get; }
    }
}
