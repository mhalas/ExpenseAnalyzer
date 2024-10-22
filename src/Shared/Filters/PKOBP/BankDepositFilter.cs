using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Filters.PKOBP
{
    public class BankDepositFilter : ITransactionFilter
    {
        private List<string> _transactionsToFilter = new List<string>()
        {
            "Obciążenie",
            "Uznanie"
        };

        public IEnumerable<ExpenseTransaction> FilterTransactions(IEnumerable<ExpenseTransaction> allTransactions)
        {
            var depositTransactions = allTransactions
                .Where(x => _transactionsToFilter.Any(y => x.Description.Contains(y)))
                .ToList();

            var result = allTransactions.ToList();
            result.RemoveAll(depositTransactions.Contains);

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
