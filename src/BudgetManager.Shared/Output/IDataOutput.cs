using BudgetManager.Shared.Models;
using System.Collections.Generic;

namespace BudgetManager.Shared.Output
{
    public interface IDataOutput
    {
        void OutputData(IEnumerable<TransactionResultRow> data);
    }
}
