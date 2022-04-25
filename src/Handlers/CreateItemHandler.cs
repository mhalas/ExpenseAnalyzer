using Api.HandlerRequests;
using Api.HandlerResponses;
using DataAccess.EntityFramework;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers
{
    public class CreateItemHandler : IRequestHandler<CreateItemRequest, CreatedResponse>
    {
        private readonly ExpenseDbContext _dbContext;

        public CreateItemHandler(ExpenseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreatedResponse> Handle(CreateItemRequest request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.AddAsync((object)request.NewObject, cancellationToken);
            await _dbContext.SaveChangesAsync();

            return new CreatedResponse(request.NewObject, true, "Success.");
        }
    }
}
