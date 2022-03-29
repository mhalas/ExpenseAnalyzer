using Api.HandlerRequests;
using DataAccess.Contracts.Shared;
using DataAccess.EntityFramework;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers
{
    public class GetSingleItemHandler<TItemResponse> : IRequestHandler<GetSingleItemRequest<TItemResponse>, TItemResponse>
        where TItemResponse : class, IId
    {
        private readonly ExpenseDbContext _dbContext;

        public GetSingleItemHandler(ExpenseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TItemResponse> Handle(GetSingleItemRequest<TItemResponse> request, CancellationToken cancellationToken)
        {
            var query = _dbContext
                .Set<TItemResponse>()
                .Where(x => x.Id == request.ItemId);

            query = request.Filter.FilterBy(query);

            return await Task.FromResult(query.SingleOrDefault());
        }
    }
}
