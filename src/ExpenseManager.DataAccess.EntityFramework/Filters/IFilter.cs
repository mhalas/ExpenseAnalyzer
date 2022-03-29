using System.Linq;

namespace DataAccess.EntityFramework.Filters
{
    public interface IFilter<TItem>
        where TItem : class
    {
        IQueryable<TItem> FilterBy(IQueryable<TItem> query);
    }
}
