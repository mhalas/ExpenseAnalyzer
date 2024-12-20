using BudgetManager.Shared.Filters;
using BudgetManager.Shared.Models;
using System.Collections.Generic;

namespace BudgetManager.Shared.TransactionsProcessors.PKOBP
{
    public class FilteredTransactionsProcessorWrapper: ITransactionsProcessor
    {
        private readonly ITransactionFilter _filters;
        private readonly ITransactionsProcessor _transactionsProcessor;

        public FilteredTransactionsProcessorWrapper(ITransactionFilter filters, 
            ITransactionsProcessor transactionsProcessor) 
        {
            _filters = filters;
            _transactionsProcessor = transactionsProcessor;
        }

        public bool CanExecute()
        {
            return _transactionsProcessor.CanExecute();
        }

        public IEnumerable<TransactionResultRow> ProcessTransactions(string historyData)
        {
            var transactions = _transactionsProcessor.ProcessTransactions(historyData);

            return _filters.FilterTransactions(transactions);
        }
    }
}
