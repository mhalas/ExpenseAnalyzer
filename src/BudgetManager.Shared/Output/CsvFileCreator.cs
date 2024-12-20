using BudgetManager.Shared.Configuration;
using BudgetManager.Shared.Extension;
using BudgetManager.Shared.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BudgetManager.Shared.Output
{
    public class CsvFileCreator : IDataOutput
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly ConfigurationDto _configuration;

        public CsvFileCreator(ConfigurationDto configuration)
        {
            _configuration = configuration;
        }

        public void OutputData(IEnumerable<TransactionResultRow> data)
        {
            if (!_configuration.SplitIntoChunks.HasValue)
            {
                CreateSingleFile(data);
                return;
            }

            int part = 1;
            foreach (var chunkedList in data.Chunk(_configuration.SplitIntoChunks.Value))
            {
                CreateSingleFile(chunkedList, $@"_Part{part}");
                part++;
            }
        }

        private void CreateSingleFile(IEnumerable<TransactionResultRow> data, string fileNamePostfix = null)
        {
            Logger.Debug("Create CSV file.");

            StringBuilder stringBuilder = new StringBuilder();
            FillWithData(data, stringBuilder);

            if (_configuration.GenerateSummary)
            {
                FillWithSummary(data, stringBuilder);
            }

            var result = stringBuilder.ToString();
            GenerateOutputFile(result, fileNamePostfix);
        }

        private void FillWithData(IEnumerable<TransactionResultRow> data, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("\"Data waluty\"," +
                "\"Kwota\"," +
                "\"Konto bankowe\"," +
                "\"Nazwa\"," +
                "\"Opis\"," +
                "\"Kategoria\"");

            foreach (var row in data)
            {
                stringBuilder.AppendLine($"\"{row.ValueDate.ToString("yyyy-MM-dd")}\"," +
                    $"\"{GetAmount(row)}\"," +
                    GetCell(row.TargetAccount) +
                    GetCell(row.TargetName) +
                    GetCell(row.Description) +
                    $"\"{row.Category}\"");
            }

            string GetCell(string data)
            {
                return string.IsNullOrEmpty(data) ? "\"\"," : $"\"{data}\",";
            }
        }

        private string GetAmount(TransactionResultRow row)
        {
            if (row.Amount >= 0)
            {
                return @$"+{row.Amount}";
            }

            return row.Amount.ToString();
        }

        private void FillWithSummary(IEnumerable<TransactionResultRow> data, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Wydatki,,");
            stringBuilder.AppendLine("\"Miesiąc\",\"Kategoria\",\"Suma\"");

            var outcomeSummary = data.GetSummaryOutcome();

            foreach (var s in outcomeSummary.OrderBy(x => x.Month).ThenBy(x => x.Category))
            {
                stringBuilder.AppendLine($"\"{s.Month}\",\"{s.Category}\",\"{s.Amount}\"");
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Przychód,,");
            stringBuilder.AppendLine("\"Miesiąc\",\"Kategoria\",\"Suma\"");

            var incomeSummary = data.GetSummaryIncome();

            foreach (var s in incomeSummary.OrderBy(x => x.Month).ThenBy(x => x.Category))
            {
                stringBuilder.AppendLine($"\"{s.Month}\",\"{s.Category}\",\"{s.Amount}\"");
            }
        }

        private void GenerateOutputFile(string result, string fileNamePostfix = null)
        {
            if (!Directory.Exists(_configuration.OutputPath))
                Directory.CreateDirectory(_configuration.OutputPath);

            var fullFilePath = Path.Combine(_configuration.OutputPath, $"AnalyzedHistory-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}{fileNamePostfix}.csv");

            Logger.Info($"Save analyzed file to {fullFilePath}.");
            File.WriteAllText(fullFilePath, result, Encoding.UTF8);
        }
    }
}
