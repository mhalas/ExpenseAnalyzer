using Shared.Dto;
using System.Collections.Generic;

namespace Shared.BankAnalyzer
{
    public interface IBankAnalyzer
    {
        IEnumerable<ExpenseDataRow> AnalyzeExpenseHistory(string historyData);
    }
}
