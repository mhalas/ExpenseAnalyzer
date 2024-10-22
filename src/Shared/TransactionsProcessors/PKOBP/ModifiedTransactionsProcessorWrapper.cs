using Shared.Dto;
using Shared.Modifiers;
using System.Collections.Generic;

namespace Shared.TransactionsProcessors.PKOBP
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

        public IEnumerable<ExpenseTransaction> ProcessTransactions(string historyData)
        {
            var transactions = _transactionsProcessor.ProcessTransactions(historyData);

            return _modifiers.ModifyTransactions(transactions);
        }
    }
}
