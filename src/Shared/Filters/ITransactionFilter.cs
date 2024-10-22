using Shared.Dto;
using System.Collections.Generic;

namespace Shared.Filters
{
    public interface ITransactionFilter
    {
        public IEnumerable<ExpenseTransaction> FilterTransactions(IEnumerable<ExpenseTransaction> transactions);
    }
}
