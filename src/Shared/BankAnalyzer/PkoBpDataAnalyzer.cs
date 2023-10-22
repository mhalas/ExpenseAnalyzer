using NLog;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using Shared.Configuration;
using Shared.Dto;
using Shared.TransactionTypes;
using Shared.TransactionTypes.PKOBP;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Shared.BankAnalyzer
{
    public class PkoBpDataAnalyzer : IBankAnalyzer
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        private const int TransactionTypeIndex = 2;

        private readonly ConfigurationDto _configuration;
        private readonly PKOBPTransactionTypeFactory _factory;

        public PkoBpDataAnalyzer(ConfigurationDto configuration, PKOBPTransactionTypeFactory factory)
        {
            _configuration = configuration;
            _factory = factory;
        }

        public bool CanExecute()
        {
            return true;
        }

        public IEnumerable<ExpenseDataRow> AnalyzeExpenseHistory(string historyData)
        {
            Logger.Debug("Analyzing PkoBP history.");

            var rows = AnalyzeHistoryData(historyData);
            return rows;
        }

        private IEnumerable<ExpenseDataRow> AnalyzeHistoryData(string historyData)
        {
            var rows = historyData.Split('\n').ToList();
            rows.RemoveAt(0);

            List<string> succeedRows = new List<string>();
            List<string> failedRows = new List<string>();
            List<string> ignoredRows = new List<string>();
            

            var result = new List<ExpenseDataRow>();
            for (int i = 0; i < rows.Count; i++)
            {
                var row = rows[i].Replace("\"\r", "");
                Logger.Debug($@"{i + 1}: {row}");

                if (string.IsNullOrEmpty(row))
                {
                    continue;
                }

                try
                {
                    var rowColumns = row
                        .Split(new string[] { "\",\"" }, StringSplitOptions.None)
                        .Select(x => x.Replace("\"", ""))
                        .ToArray();

                    var transactionType = _factory.GetTransactionType(rowColumns[TransactionTypeIndex]);
                    var transactionRow = transactionType.GetTransactionRow(rowColumns);

                    if (transactionRow == null)
                    {
                        ignoredRows.Add(row);
                        continue;
                    }

                    var category = GetCategory(transactionRow);

                    var amount = GetAbsoluteValueOfAmount(transactionRow.Amount);
                    result.Add(new ExpenseDataRow
                    {
                        Amount = amount,
                        Category = category,
                        TargetName = transactionRow.TargetName,
                        TargetAccount = transactionRow.TargetAccount,
                        Description = transactionRow.Description,
                        ValueDate = transactionRow.TransactionDate
                    });

                    succeedRows.Add(row);
                }
                catch(Exception ex)
                {
                    Logger.Error(ex, $"Error occured for row {i + 1}\r\n{row}.");
                    failedRows.Add(row);
                }
            }

            Logger.Info($@"Succeed rows: '{succeedRows.Count}'.");
            Logger.Warn($@"Ignored rows: '{ignoredRows.Count}'.");
            Logger.Error($@"Failed rows: '{failedRows.Count}'.");

            return result;
        }

        private decimal GetAbsoluteValueOfAmount(decimal amount)
        {
            if (_configuration.UseAbsoluteValuesForAmount)
            {
                return decimal.Abs(amount);
            }

            return amount;
        }

        private string GetCategory(TransactionRow row)
        {
            var category = _configuration
                .CategoryDictionary
                .FirstOrDefault(x => x.Value.Any(y => 
                    row.Description.ToLower().Contains(y.ToLower()) ||
                    row.TargetAccount.ToLower().Contains(y.ToLower()) ||
                    row.TargetName.ToLower().Contains(y.ToLower())));

            if (category.Key != null)
            {
                return category.Key.Replace("\"", "");
            }

            if (row.Amount > 0)
            {
                return _configuration.DefaultIncomeCategoryName;
            }

            return _configuration.DefaultExpenseCategoryName;
        }
    }
}
