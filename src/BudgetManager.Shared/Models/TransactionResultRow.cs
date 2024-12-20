using System;

namespace BudgetManager.Shared.Models
{
    public class TransactionResultRow(DateTime valueDate, 
        decimal amount, 
        string description, 
        string category, 
        string targetAccount, 
        string targetName)
    {
        public DateTime ValueDate { get; } = valueDate;

        public decimal Amount { get; } = amount;

        public string Description { get; } = description;

        public string Category { get; } = category;

        public string TargetAccount { get; } = targetAccount;

        public string TargetName { get; } = targetName;
    }
}
