using NLog;
using Shared.Configuration;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.BankAnalyzer
{
    public class PkoBpDataAnalyzer: IBankAnalyzer
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly ConfigurationDto _configuration;

        public PkoBpDataAnalyzer(ConfigurationDto configuration)
        {
            _configuration = configuration;
        }

        public bool CanExecute()
        {
            if (!_configuration.ColumnDefinitions.ContainsKey("ValueDateColumn"))
            {
                Logger.Info("Required 'ValueDateColumn' in configuration property 'ColumnDefinitions'.");
                return false;
            }
            if (!_configuration.ColumnDefinitions.ContainsKey("AmountColumn"))
            {
                Logger.Info("Required 'AmountColumn' in configuration property 'ColumnDefinitions'.");
                return false;
            }
            if (!_configuration.ColumnDefinitions.ContainsKey("DescriptionColumns"))
            {
                Logger.Info("Required 'DescriptionColumns' in configuration property 'ColumnDefinitions'.");
                return false;
            }

            return true;
        }

        public IEnumerable<ExpenseDataRow> AnalyzeExpenseHistory(string historyData)
        {
            Logger.Debug("Analyzing PkoBP history.");

            var rows = AnalyzeHistoryData(historyData);

            rows = RemoveInvestmentData(rows);

            return rows;
        }

        private IEnumerable<ExpenseDataRow> RemoveInvestmentData(IEnumerable<ExpenseDataRow> rows)
        {
            return rows.Where(x => !x.Description.Contains("000%"));
        }

        private IEnumerable<ExpenseDataRow> AnalyzeHistoryData(string historyData)
        {
            var rows = historyData.Split('\n').ToList();
            var headerRowColumns = rows.First().Split(',').ToList();

            var valueDateIndex = GetDefinitionsData("ValueDateColumn", headerRowColumns).First();
            var amountIndex = GetDefinitionsData("AmountColumn", headerRowColumns).First();
            var descriptionColumns = GetDefinitionsData("DescriptionColumns", headerRowColumns);

            rows.RemoveAt(0);

            var result = new List<ExpenseDataRow>();
            foreach (var row in rows)
            {
                if (string.IsNullOrEmpty(row))
                    continue;

                var rowColumns = row.Split(new string[] { "\",\"" }, StringSplitOptions.None);

                var valueDate = DateTime.Parse(rowColumns[valueDateIndex].Replace("\"", ""));
                var amount = decimal.Parse(rowColumns[amountIndex].Replace("\"", "").Replace(".", ","));
                (string Description, string Category) categoryAndDescription = GetCategory(rowColumns, descriptionColumns);

                result.Add(new ExpenseDataRow(valueDate, amount, categoryAndDescription.Description, categoryAndDescription.Category));
            }

            return result;
        }

        private IEnumerable<int> GetDefinitionsData(string definitionName, List<string> headerRowColumns)
        {
            var definitions = _configuration.ColumnDefinitions[definitionName];
            foreach (var def in definitions)
            {
                yield return def.ColumnIndex.HasValue ?
                    def.ColumnIndex.Value - 1
                    : headerRowColumns.IndexOf(def.ColumnName);
            }
        }

        private (string, string) GetCategory(string[] rowColumns, IEnumerable<int> descriptionColumns)
        {
            foreach (var column in descriptionColumns)
            {
                var category = _configuration.CategoryDictionary.FirstOrDefault(x => x.Value.Any(y => rowColumns[column].ToLower().Contains(y.ToLower())));
                if (category.Key != null)
                    return (rowColumns[column].Replace("\"", ""), category.Key.Replace("\"", ""));
            }

            return (rowColumns[descriptionColumns.First()].Replace("\"", ""), _configuration.DefaultCategoryName);
        }
    }
}
