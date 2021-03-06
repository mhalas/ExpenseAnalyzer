using NLog;
using Shared.Dto;
using Shared.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Output
{
    public class CsvFileCreator : IDataOutput
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly string _originFilePath;

        public CsvFileCreator(string originFilePath)
        {
            _originFilePath = originFilePath;
        }

        public Task OutputData(IEnumerable<ExpenseDataRow> data)
        {
            Logger.Debug("Create CSV file.");

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("\"Data waluty\",\"Kwota\",\"Tytuł\",\"Źródło\",\"Kategoria\"");

            foreach (var row in data)
            {
                stringBuilder.AppendLine($"\"{row.ValueDate.ToString("yyyy-MM-dd")}\",\"{row.Amount}\",\"{row.Title}\",\"{row.Source}\",\"{row.Category}\"");
            }

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

            var result = stringBuilder.ToString();

            var analyzedFilePath = Path.GetDirectoryName(Path.GetFullPath(_originFilePath));
            analyzedFilePath = Path.Combine(analyzedFilePath, $"AnalyzedHistory-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.csv");

            Logger.Debug($"Save analyzed file to {analyzedFilePath}.");
            File.WriteAllText(analyzedFilePath, result, Encoding.UTF8);

            return Task.CompletedTask;
        }
    }
}
