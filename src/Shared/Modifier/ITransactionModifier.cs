using Shared.Dto;
using System.Collections.Generic;

namespace Shared.Modifiers
{
    public interface ITransactionModifier
    {
        public IEnumerable<ExpenseTransaction> ModifyTransactions(IEnumerable<ExpenseTransaction> dataRow);
    }
}
