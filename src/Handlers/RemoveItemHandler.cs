using Api.HandlerRequests;
using Api.HandlerResponses;
using DataAccess.Contracts.Shared;
using DataAccess.EntityFramework;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers
{
    public class RemoveItemHandler<TItemResponse> : IRequestHandler<RemoveItemRequest<TItemResponse>, GeneralResponse>
        where TItemResponse : class, IId
    {
        private readonly ExpenseDbContext _dbContext;

        public RemoveItemHandler(ExpenseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GeneralResponse> Handle(RemoveItemRequest<TItemResponse> request, CancellationToken cancellationToken)
        {
            var query = _dbContext
                .Set<TItemResponse>()
                .Where(x => x.Id == request.ItemId);

            request.Filter.FilterBy(query);

            var item = query.SingleOrDefault();

            _dbContext.Remove(item);

            var count = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var response = new GeneralResponse(count > 0, "Success.");

            return await Task.FromResult(response);
        }
    }
}
