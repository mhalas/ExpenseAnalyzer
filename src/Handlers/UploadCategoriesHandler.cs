using Api.HandlerRequests;
using Api.HandlerResponses;
using DataAccess.Contracts.Model;
using DataAccess.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers
{
    public class UploadCategoriesHandler : IRequestHandler<UploadCategoriesRequest, GeneralResponse>
    {
        private readonly ExpenseDbContext _dbContext;

        public UploadCategoriesHandler(ExpenseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GeneralResponse> Handle(UploadCategoriesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userCategories = await _dbContext
                .UserCategory
                .Include(x => x.UserCategoryValues)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

                var result = new List<UserCategory>();

                foreach (var item in request.Categories)
                {
                    UserCategory category = userCategories.SingleOrDefault(x => x.CategoryName == item.Key);

                    if (category == null)
                    {
                        category = new UserCategory()
                        {
                            UserId = request.UserId,
                            CategoryName = item.Key,
                            UserCategoryValues = new List<UserCategoryValue>(),
                        };

                        await _dbContext
                            .UserCategory
                            .AddAsync(category, cancellationToken)
                            .ConfigureAwait(false);
                    }

                    var existsCategoryValues = category.UserCategoryValues;

                    foreach (var value in item.Value.Where(x => !existsCategoryValues.Any(y => y.SellerName == x)))
                    {
                        var newCategoryValue = new UserCategoryValue()
                        {
                            SellerName = value,
                            UserCategoryId = category.Id
                        };

                        category.UserCategoryValues.Add(newCategoryValue);
                    }

                    await _dbContext.SaveChangesAsync();
                }

                var response = new GeneralResponse(true, "Success");

                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                var response = new GeneralResponse(ex.Message);

                return await Task.FromResult(response);
            }
        }
    }
}
