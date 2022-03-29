using DataAccess.Contracts.Shared;
using DataAccess.EntityFramework.Filters;
using MediatR;

namespace Api.HandlerRequests
{
    public class GetSingleItemRequest<TItemResponse>: IRequest<TItemResponse>
        where TItemResponse : class, IId
    {
        public GetSingleItemRequest(IFilter<TItemResponse> filter, int itemId)
        {
            Filter = filter;
            ItemId = itemId;
        }

        public IFilter<TItemResponse> Filter { get; }
        public int ItemId { get; }
    }
}
