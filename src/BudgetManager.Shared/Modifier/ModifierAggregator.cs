using BudgetManager.Shared.Models;
using System.Collections.Generic;

namespace BudgetManager.Shared.Modifiers
{
    public class ModifierAggregator: ITransactionModifier
    {
        private readonly IEnumerable<ITransactionModifier> _transactionModifiers;

        public ModifierAggregator(IEnumerable<ITransactionModifier> transactionModifiers)
        {
            _transactionModifiers = transactionModifiers;
        }

        public IEnumerable<TransactionResultRow> ModifyTransactions(IEnumerable<TransactionResultRow> dataRow)
        {
            foreach (var modifier in _transactionModifiers)
            {
                dataRow = modifier.ModifyTransactions(dataRow);
            }

            return dataRow;
        }
    }
}
