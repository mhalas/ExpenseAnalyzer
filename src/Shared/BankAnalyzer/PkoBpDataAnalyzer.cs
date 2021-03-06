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

        public IEnumerable<ExpenseDataRow> AnalyzeExpenseHistory(string historyData)
        {
            Logger.Debug("Analyzing PkoBP history.");

            var rows = DeserializeData(historyData);

            foreach (var row in rows)
            {
                var category = _configuration.CategoryDictionary.FirstOrDefault(x => 
                    x.Value.Any(y =>
                        y.Title == null? 
                            row.Source.ToLower().Contains(y.Source.ToLower()):
                            row.Title.ToLower().Contains(y.Title.ToLower())));

                if (category.Key == null)
                    row.Category = _configuration.DefaultCategoryName;
                else
                    row.Category = category.Key;
            }

            rows = RemoveInvestmentData(rows);

            return rows;
        }

        private IEnumerable<ExpenseDataRow> RemoveInvestmentData(IEnumerable<ExpenseDataRow> rows)
        {
            return rows.Where(x => !x.Source.Contains("000%"));
        }

        private IEnumerable<ExpenseDataRow> DeserializeData(string historyData)
        {
            var rows = historyData.Split('\n').ToList();
            var headerRowColumns = rows.First().Split(',').ToList();

            int valueDateIndex = _configuration.ValueDateColumn.ColumnIndex.HasValue ?
                _configuration.ValueDateColumn.ColumnIndex.Value : headerRowColumns.IndexOf(_configuration.ValueDateColumn.ColumnName);
            int amountIndex = _configuration.AmountColumn.ColumnIndex.HasValue ?
                _configuration.AmountColumn.ColumnIndex.Value : headerRowColumns.IndexOf(_configuration.AmountColumn.ColumnName);
            int sourceIndex = _configuration.SourceColumn.ColumnIndex.HasValue ?
                _configuration.SourceColumn.ColumnIndex.Value : headerRowColumns.IndexOf(_configuration.SourceColumn.ColumnName);
            int titleIndex = _configuration.TitleColumn.ColumnIndex.HasValue ?
                _configuration.TitleColumn.ColumnIndex.Value : headerRowColumns.IndexOf(_configuration.TitleColumn.ColumnName);

            rows.RemoveAt(0);

            var result = new List<ExpenseDataRow>();
            foreach (var row in rows)
            {
                if (string.IsNullOrEmpty(row))
                    continue;

                var rowColumns = row.Split(',');
                result.Add(new ExpenseDataRow(
                        DateTime.Parse(rowColumns[valueDateIndex].Replace("\"", "")),
                        decimal.Parse(rowColumns[amountIndex].Replace("\"", "").Replace(".", ",")),
                        rowColumns[titleIndex].Replace("\"", ""),
                        rowColumns[sourceIndex].Replace("\"", "")));
            }

            return result;
        }
    }
}
