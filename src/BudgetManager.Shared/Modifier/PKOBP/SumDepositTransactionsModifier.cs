using BudgetManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetManager.Shared.Modifiers.PKOBP
{
    public class SumDepositTransactionsModifier : ITransactionModifier
    {
        private const string RevolvingDepositText = "ODNAWIALNA";

        private List<string> _transactionsToFilter = new List<string>()
        {
            "Obciążenie",
            "Uznanie"
        };

        public IEnumerable<TransactionResultRow> ModifyTransactions(IEnumerable<TransactionResultRow> allTransactions)
        {
            var transactions = allTransactions
                .Where(x => _transactionsToFilter.Any(y => x.Description.Contains(y)))
                .OrderBy(x => x.ValueDate)
                .ToList();

            var revolvingDepositNumbers = transactions
                .Where(x => x.Description.Contains(RevolvingDepositText))
                .Select(x => GetDepositNumber(x.Description));

            var revolvingDepositPairs = transactions
                .Select(x => new
                {
                    Number = GetDepositNumber(x.Description),
                    Transaction = x,
                })
                .GroupBy(x => x.Number, x => x.Transaction)
                .Where(group => revolvingDepositNumbers.Any(number => number == group.Key));

            var transactionsToAdd = new List<TransactionResultRow>();

            foreach (var pair in revolvingDepositPairs) 
            {
                var depositTransaction = pair.First();
                var returnTransaction = pair.Last();

                if(Math.Abs(returnTransaction.Amount) == Math.Abs(depositTransaction.Amount))
                {
                    continue;
                }

                transactionsToAdd.Add(new TransactionResultRow(returnTransaction.ValueDate,
                    Math.Abs(returnTransaction.Amount) - Math.Abs(depositTransaction.Amount),
                    $@"Zysk z lokaty nr {pair.Key}",
                    "Lokata",
                    depositTransaction.TargetAccount,
                    depositTransaction.TargetName));
            }

            var result = allTransactions.ToList();
            result.AddRange(transactionsToAdd);

            return result;
        }

        private string GetDepositNumber(string description)
        {
            var number = description
                .Split("LOKATY NR", StringSplitOptions.RemoveEmptyEntries)[1]
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)[0];

            return number;
        }
    }
}
