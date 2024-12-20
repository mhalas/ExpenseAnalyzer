using BudgetManager.Shared.Output;
using BudgetManager.Shared.TransactionsProcessors;
using NLog;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace BudgetManager.Shared.SourceData
{
    public class SingleFileDataSource : ISourceDataExecutor
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly string _filePath;

        public SingleFileDataSource(string filePath)
        {
            _filePath = filePath;
        }

        public void Execute(ITransactionsProcessor transactionProcessor, IDataOutput outputLogic)
        {
            using (var reader = new StreamReader(_filePath))
            {
                Stopwatch time = new Stopwatch();

                Logger.Info($@"Start analyzing file {_filePath}.");
                time.Start();
                var result = transactionProcessor
                    .ProcessTransactions(reader.ReadToEnd())
                    .OrderBy(x => x.ValueDate);

                Logger.Info("Analyze complete.");

                outputLogic.OutputData(result);

                time.Stop();
                Logger.Info($"Complete in time {time.Elapsed.Hours}:{time.Elapsed.Minutes}:{time.Elapsed.Seconds}.{time.Elapsed.Milliseconds}.");
            }
        }
    }
}