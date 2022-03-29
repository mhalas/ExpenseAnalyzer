using DataAccess.Contracts.References;
using DataAccess.EntityFramework.Filters;
using System.Linq;

namespace Api.Filters
{
    public class UserReferenceFilter<TItem>: IFilter<TItem>
        where TItem : class, IUserReference
    {
        private readonly int _userId;

        public UserReferenceFilter(int userId)
        {
            _userId = userId;
        }

        public IQueryable<TItem> FilterBy(IQueryable<TItem> query)
        {
            return query.Where(x => x.UserId == _userId);
        }
    }
}
