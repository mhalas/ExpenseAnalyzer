using BudgetManager.Shared.Models;
using System.Collections.Generic;

namespace BudgetManager.Shared.TransactionsProcessors
{
    public interface ITransactionsProcessor
    {
        bool CanExecute();

        IEnumerable<TransactionResultRow> ProcessTransactions(string historyData);
    }
}
