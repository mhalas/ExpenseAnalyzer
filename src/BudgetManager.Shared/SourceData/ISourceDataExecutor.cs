using BudgetManager.Shared.Output;
using BudgetManager.Shared.TransactionsProcessors;

namespace BudgetManager.Shared.SourceData
{
    public interface ISourceDataExecutor
    {
        void Execute(ITransactionsProcessor bankAnalyzer, IDataOutput outputLogic);
    }
}