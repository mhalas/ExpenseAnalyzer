using Shared.Dto;
using Shared.Filters;
using System.Collections.Generic;

namespace Shared.TransactionsProcessors.PKOBP
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

        public IEnumerable<ExpenseTransaction> ProcessTransactions(string historyData)
        {
            var transactions = _transactionsProcessor.ProcessTransactions(historyData);

            return _filters.FilterTransactions(transactions);
        }
    }
}
