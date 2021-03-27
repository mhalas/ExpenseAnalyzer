using NLog;
using Shared.Configuration;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.BankAnalyzer
{
    public class AmericanExpressDataAnalyzer : IBankAnalyzer
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly ConfigurationDto _configuration;

        public AmericanExpressDataAnalyzer(ConfigurationDto configuration)
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
            if (!_configuration.ColumnDefinitions.ContainsKey("DescriptionColumn"))
            {
                Logger.Info("Required 'DescriptionColumn' in configuration property 'ColumnDefinitions'.");
                return false;
            }

            return true;
        }

        public IEnumerable<ExpenseDataRow> AnalyzeExpenseHistory(string historyData)
        {
            var rows = historyData.Split('\n').ToList();
            var headerRowColumns = rows.First().Split(',').ToList();

            int valueDateIndex = _configuration.ColumnDefinitions["ValueDateColumn"].ColumnIndex.HasValue ?
                _configuration.ColumnDefinitions["ValueDateColumn"].ColumnIndex.Value : headerRowColumns.IndexOf(_configuration.ColumnDefinitions["ValueDateColumn"].ColumnName);

            int amountIndex = _configuration.ColumnDefinitions["AmountColumn"].ColumnIndex.HasValue ?
                _configuration.ColumnDefinitions["AmountColumn"].ColumnIndex.Value : headerRowColumns.IndexOf(_configuration.ColumnDefinitions["AmountColumn"].ColumnName);

            int descriptionIndex = _configuration.ColumnDefinitions["DescriptionColumn"].ColumnIndex.HasValue ?
                _configuration.ColumnDefinitions["DescriptionColumn"].ColumnIndex.Value : headerRowColumns.IndexOf(_configuration.ColumnDefinitions["DescriptionColumn"].ColumnName);

            rows.RemoveAt(0);

            var result = new List<ExpenseDataRow>();
            foreach (var row in rows)
            {
                if (string.IsNullOrEmpty(row))
                    continue;

                var rowColumns = row.Split(',');

                var valueDate = DateTime.Parse(rowColumns[valueDateIndex].Replace("\"", ""));
                var amount = decimal.Parse(rowColumns[amountIndex].Replace("\"", "").Replace(".", ","));
                var description = rowColumns[descriptionIndex].Replace("\"", "");

                var category = _configuration.CategoryDictionary.FirstOrDefault(x => x.Value.Any(y => rowColumns[descriptionIndex].ToLower().Contains(y.ToLower())));

                result.Add(new ExpenseDataRow(valueDate, 
                    amount, 
                    description, 
                    category.Key == null? _configuration.DefaultCategoryName: category.Key));
            }

            return result;
        }


    }
}
