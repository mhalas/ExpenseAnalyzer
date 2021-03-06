using System;

namespace Shared.Dto
{
    public class ExpenseDataRow
    {
        public ExpenseDataRow(DateTime valueDate, decimal amount, string title, string source)
        {
            ValueDate = valueDate;
            Amount = amount;
            Title = title;
            Source = source;
        }

        public DateTime ValueDate { get; }

        public decimal Amount { get; }

        public string Title { get; }

        public string Source { get; }

        public string Category { get; set; }
    }
}
