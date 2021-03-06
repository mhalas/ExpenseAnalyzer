using Shared.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Extension
{
    public static class ExpenseDataRowEnumerableExtensions
    {
        public static IEnumerable<ExpenseDataSummary> GetSummaryOutcome(this IEnumerable<ExpenseDataRow> data)
            => data.Where(x => x.Amount < 0)
                .GroupBy(x => new { x.Category, x.ValueDate.Date.Month })
                .Select(x => new ExpenseDataSummary(x.Key.Month, x.Key.Category, x.Sum(y => y.Amount)))
                .ToList();

        public static IEnumerable<ExpenseDataSummary> GetSummaryIncome(this IEnumerable<ExpenseDataRow> data)
            => data.Where(x => x.Amount > 0)
                .GroupBy(x => new { x.Category, x.ValueDate.Date.Month })
                .Select(x => new ExpenseDataSummary(x.Key.Month, x.Key.Category, x.Sum(y => y.Amount)))
                .ToList();
    }
}
