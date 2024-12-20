using BudgetManager.Shared.Models;
using System.Collections.Generic;

namespace BudgetManager.Shared.Filters
{
    public interface ITransactionFilter
    {
        public IEnumerable<TransactionResultRow> FilterTransactions(IEnumerable<TransactionResultRow> transactions);
    }
}
