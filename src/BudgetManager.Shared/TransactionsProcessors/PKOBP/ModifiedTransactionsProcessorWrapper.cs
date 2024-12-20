using BudgetManager.Shared.Models;
using BudgetManager.Shared.Modifiers;
using System.Collections.Generic;

namespace BudgetManager.Shared.TransactionsProcessors.PKOBP
{
    public class ModifiedTransactionsProcessorWrapper: ITransactionsProcessor
    {
        private readonly ITransactionModifier _modifiers;
        private readonly ITransactionsProcessor _transactionsProcessor;

        public ModifiedTransactionsProcessorWrapper(ITransactionModifier modifiers, 
            ITransactionsProcessor transactionsProcessor)
        {
            _modifiers = modifiers;
            _transactionsProcessor = transactionsProcessor;
        }

        public bool CanExecute()
        {
            return _transactionsProcessor.CanExecute();
        }

        public IEnumerable<TransactionResultRow> ProcessTransactions(string historyData)
        {
            var transactions = _transactionsProcessor.ProcessTransactions(historyData);

            return _modifiers.ModifyTransactions(transactions);
        }
    }
}
