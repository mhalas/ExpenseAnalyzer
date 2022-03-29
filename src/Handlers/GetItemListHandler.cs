using Api.HandlerRequests;
using DataAccess.Contracts.References;
using DataAccess.Contracts.Shared;
using DataAccess.EntityFramework;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers
{
    public class GetItemListHandler<TItemResponse> : IRequestHandler<GetItemsRequest<TItemResponse>, IEnumerable<TItemResponse>>
        where TItemResponse : class, IId
    {
        private readonly ExpenseDbContext _dbContext;

        public GetItemListHandler(ExpenseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TItemResponse>> Handle(GetItemsRequest<TItemResponse> request, CancellationToken cancellationToken)
        {
            var list = _dbContext
                .Set<TItemResponse>()
                .AsQueryable<TItemResponse>();


            list = request.Filter.FilterBy(list);

            return await Task.FromResult(list.ToList());
        }
    }
}
