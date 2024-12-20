using BudgetManager.Shared.Models;
using System.Collections.Generic;

namespace BudgetManager.Shared.Modifiers
{
    public interface ITransactionModifier
    {
        public IEnumerable<TransactionResultRow> ModifyTransactions(IEnumerable<TransactionResultRow> dataRow);
    }
}
