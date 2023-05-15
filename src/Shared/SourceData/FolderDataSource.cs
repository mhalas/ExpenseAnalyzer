﻿using NLog;
using Shared.BankAnalyzer;
using Shared.Dto;
using Shared.Output;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Shared.SourceData
{
    public class FolderDataSource : ISourceDataExecutor
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly string _filesSourcePath;

        public FolderDataSource(string filesSourcePath)
        {
            _filesSourcePath = filesSourcePath;
        }

        public void Execute(IBankAnalyzer bankAnalyzer, IDataOutput outputLogic)
        {
            Stopwatch time = new Stopwatch();
            time.Start();

            var expenseHistory = new List<ExpenseDataRow>();

            var files = Directory.GetFiles(_filesSourcePath);
            foreach (var file in files)
            {
                using (var reader = new StreamReader(file))
                {
                    Logger.Info($@"Start analyzing file {file}.");
                    var result = bankAnalyzer.AnalyzeExpenseHistory(reader.ReadToEnd());
                    expenseHistory.AddRange(result);
                    Logger.Info("Analyze complete.");
                }
            }

            expenseHistory = expenseHistory
                .OrderBy(x => x.ValueDate)
                .ToList();

            outputLogic.OutputData(expenseHistory);
            time.Stop();

            Logger.Info($"Complete in time {time.Elapsed.Hours}:{time.Elapsed.Minutes}:{time.Elapsed.Seconds}.{time.Elapsed.Milliseconds}.");
        }
    }
}
