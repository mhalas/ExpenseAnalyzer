using Shared.Dto;
using System.Collections.Generic;

namespace Shared.BankAnalyzer
{
    public interface IBankAnalyzer
    {
        bool CanExecute();
        IEnumerable<ExpenseDataRow> AnalyzeExpenseHistory(string historyData);
    }
}
