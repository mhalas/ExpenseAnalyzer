using Shared.Dto;
using System.Collections.Generic;

namespace Shared.Modifiers
{
    public class ModifierAggregator: ITransactionModifier
    {
        private readonly IEnumerable<ITransactionModifier> _transactionModifiers;

        public ModifierAggregator(IEnumerable<ITransactionModifier> transactionModifiers)
        {
            _transactionModifiers = transactionModifiers;
        }

        public IEnumerable<ExpenseTransaction> ModifyTransactions(IEnumerable<ExpenseTransaction> dataRow)
        {
            foreach (var modifier in _transactionModifiers)
            {
                dataRow = modifier.ModifyTransactions(dataRow);
            }

            return dataRow;
        }
    }
}
