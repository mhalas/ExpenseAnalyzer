using DataAccess.Contracts.References;
using System.Linq;

namespace DataAccess.EntityFramework.Filters
{
    public class CategoryReferenceFilter<TItem> : IFilter<TItem>
        where TItem : class, IUserCategoryReference
    {
        private readonly int _categoryId;

        public CategoryReferenceFilter(int categoryId)
        {
            _categoryId = categoryId;
        }

        public IQueryable<TItem> FilterBy(IQueryable<TItem> query)
        {
            return query.Where(x => x.UserCategoryId == _categoryId);
        }
    }
}
