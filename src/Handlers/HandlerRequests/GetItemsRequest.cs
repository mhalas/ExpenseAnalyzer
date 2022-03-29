using DataAccess.Contracts.Shared;
using DataAccess.EntityFramework.Filters;
using MediatR;
using System.Collections.Generic;

namespace Api.HandlerRequests
{
    public class GetItemsRequest<TItemResponse>: IRequest<IEnumerable<TItemResponse>>
        where TItemResponse : class, IId
    {
        public GetItemsRequest(IFilter<TItemResponse> filter)
        {
            Filter = filter;
        }

        public IFilter<TItemResponse> Filter { get; }
    }
}
