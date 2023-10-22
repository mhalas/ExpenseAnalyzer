using System;

namespace Shared.TransactionTypes
{
    public class TransactionRow
    {
        public TransactionRow(DateTime transactionDate,
            decimal amount,
            string currency,
            string targetAccount,
            string targetName,
            string description)
        {
            TransactionDate = transactionDate;
            Amount = amount;
            Currency = currency;
            TargetAccount = targetAccount;
            TargetName = targetName;
            Description = description;
        }

        public DateTime TransactionDate { get; }
        public decimal Amount { get; }
        public string Currency { get; }
        public string TargetAccount { get; }
        public string TargetName { get; }
        public string Description { get; }
    }
}
