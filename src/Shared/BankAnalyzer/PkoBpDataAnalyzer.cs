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
            if (!_configuration.ColumnDefinitions.ContainsKey("SourceColumn"))
            {
                Logger.Info("Required 'SourceColumn' in configuration property 'ColumnDefinitions'.");
                return false;
            }
            if (!_configuration.ColumnDefinitions.ContainsKey("TitleColumn"))
            {
                Logger.Info("Required 'TitleColumn' in configuration property 'ColumnDefinitions'.");
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

            int valueDateIndex = _configuration.ColumnDefinitions["ValueDateColumn"].ColumnIndex.HasValue ?
                _configuration.ColumnDefinitions["ValueDateColumn"].ColumnIndex.Value : headerRowColumns.IndexOf(_configuration.ColumnDefinitions["ValueDateColumn"].ColumnName);

            int amountIndex = _configuration.ColumnDefinitions["AmountColumn"].ColumnIndex.HasValue ?
                _configuration.ColumnDefinitions["AmountColumn"].ColumnIndex.Value : headerRowColumns.IndexOf(_configuration.ColumnDefinitions["AmountColumn"].ColumnName);

            int sourceIndex = _configuration.ColumnDefinitions["SourceColumn"].ColumnIndex.HasValue ?
                _configuration.ColumnDefinitions["SourceColumn"].ColumnIndex.Value : headerRowColumns.IndexOf(_configuration.ColumnDefinitions["SourceColumn"].ColumnName);

            int titleIndex = _configuration.ColumnDefinitions["TitleColumn"].ColumnIndex.HasValue ?
                _configuration.ColumnDefinitions["TitleColumn"].ColumnIndex.Value : headerRowColumns.IndexOf(_configuration.ColumnDefinitions["TitleColumn"].ColumnName);

            rows.RemoveAt(0);

            var result = new List<ExpenseDataRow>();
            foreach (var row in rows)
            {
                if (string.IsNullOrEmpty(row))
                    continue;

                var rowColumns = row.Split(',');

                var valueDate = DateTime.Parse(rowColumns[valueDateIndex].Replace("\"", ""));
                var amount = decimal.Parse(rowColumns[amountIndex].Replace("\"", "").Replace(".", ","));
                (string Description, string Category) categoryAndDescription = GetCategory(rowColumns, sourceIndex, titleIndex);

                result.Add(new ExpenseDataRow(valueDate, amount, categoryAndDescription.Description, categoryAndDescription.Category));
            }

            return result;
        }

        private (string, string) GetCategory(string[] rowColumns, int sourceIndex, int titleIndex)
        {
            var category = _configuration.CategoryDictionary.FirstOrDefault(x => x.Value.Any(y => rowColumns[sourceIndex].ToLower().Contains(y.ToLower())));
            if (category.Key != null)
                return (rowColumns[sourceIndex], category.Key);

            category = _configuration.CategoryDictionary.FirstOrDefault(x => x.Value.Any(y => rowColumns[titleIndex].ToLower().Contains(y.ToLower())));
            if (category.Key == null)
                return (string.Empty, _configuration.DefaultCategoryName);
            else
                return (rowColumns[titleIndex], category.Key);
        }
    }
}
