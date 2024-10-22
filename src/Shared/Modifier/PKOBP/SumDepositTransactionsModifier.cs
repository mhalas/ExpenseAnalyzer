using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Modifiers.PKOBP
{
    public class SumDepositTransactionsModifier : ITransactionModifier
    {
        private const string RevolvingDepositText = "ODNAWIALNA";

        private List<string> _transactionsToFilter = new List<string>()
        {
            "Obciążenie",
            "Uznanie"
        };

        public IEnumerable<ExpenseTransaction> ModifyTransactions(IEnumerable<ExpenseTransaction> allTransactions)
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

            var transactionsToRemove = new List<ExpenseTransaction>();

            foreach (var pair in revolvingDepositPairs) 
            {
                var depositTransaction = pair.First();
                var returnTransaction = pair.Last();

                if(Math.Abs(returnTransaction.Amount) == Math.Abs(depositTransaction.Amount))
                {
                    continue;
                }

                depositTransaction.Amount = Math.Abs(returnTransaction.Amount) - Math.Abs(depositTransaction.Amount);
                depositTransaction.Description = $@"Zysk z lokaty nr {pair.Key}";
                depositTransaction.Category = "Lokata";

                transactionsToRemove.Add(returnTransaction);
            }

            var result = allTransactions.ToList();
            result.RemoveAll(transactionsToRemove.Contains);

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
