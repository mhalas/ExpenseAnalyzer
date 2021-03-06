using System;

namespace Shared.Dto
{
    public class ExpenseDataRow
    {
        public ExpenseDataRow(DateTime valueDate, decimal amount, string description, string category)
        {
            ValueDate = valueDate;
            Amount = amount;
            Description = description;
            Category = category;
        }

        public DateTime ValueDate { get; }

        public decimal Amount { get; }

        public string Description { get; }

        public string Category { get; }
    }
}
