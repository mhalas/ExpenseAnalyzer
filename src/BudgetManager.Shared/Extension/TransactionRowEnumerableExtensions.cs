using System.Collections.Generic;
using System.Linq;
using BudgetManager.Shared.Models;

namespace BudgetManager.Shared.Extension
{
    public static class TransactionRowEnumerableExtensions
    {
        public static IEnumerable<MonthlyTransactionSummary> GetSummaryOutcome(this IEnumerable<TransactionResultRow> data)
            => data.Where(x => x.Amount < 0)
                .GroupBy(x => new { x.Category, x.ValueDate.Date.Month })
                .Select(x => new MonthlyTransactionSummary(x.Key.Month, x.Key.Category, x.Sum(y => y.Amount)))
                .ToList();

        public static IEnumerable<MonthlyTransactionSummary> GetSummaryIncome(this IEnumerable<TransactionResultRow> data)
            => data.Where(x => x.Amount > 0)
                .GroupBy(x => new { x.Category, x.ValueDate.Date.Month })
                .Select(x => new MonthlyTransactionSummary(x.Key.Month, x.Key.Category, x.Sum(y => y.Amount)))
                .ToList();
    }
}
