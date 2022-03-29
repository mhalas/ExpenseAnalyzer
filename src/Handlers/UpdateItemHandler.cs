using Api.HandlerRequests;
using Api.HandlerResponses;
using AutoMapper;
using DataAccess.Contracts.References;
using DataAccess.Contracts.Shared;
using DataAccess.EntityFramework;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers
{
    public class UpdateItemHandler<TItem> : IRequestHandler<UpdateItemRequest, GeneralResponse>
        where TItem : class, IId, IUserReference
    {
        private readonly ExpenseDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateItemHandler(ExpenseDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GeneralResponse> Handle(UpdateItemRequest request, CancellationToken cancellationToken)
        {
            var updatedObject = _mapper.Map<object, TItem>(request.UpdatedObject);
            updatedObject.Id = request.ItemId;
            updatedObject.UserId = request.UserId;

            _dbContext.Update(updatedObject);
            var count = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return await Task.FromResult(new GeneralResponse(count > 0, "Success."));
        }
    }
}
