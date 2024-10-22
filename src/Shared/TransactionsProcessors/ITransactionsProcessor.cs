using Shared.Dto;
using System.Collections.Generic;

namespace Shared.TransactionsProcessors
{
    public interface ITransactionsProcessor
    {
        bool CanExecute();

        IEnumerable<ExpenseTransaction> ProcessTransactions(string historyData);
    }
}
