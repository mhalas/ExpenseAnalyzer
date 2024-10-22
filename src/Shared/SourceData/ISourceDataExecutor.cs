using Shared.Output;
using Shared.TransactionsProcessors;

namespace Shared.SourceData
{
    public interface ISourceDataExecutor
    {
        void Execute(ITransactionsProcessor bankAnalyzer, IDataOutput outputLogic);
    }
}
