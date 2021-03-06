namespace Shared.Dto
{
    public class ExpenseDataSummary
    {

        public ExpenseDataSummary(int month, string category, decimal amount)
        {
            Month = month;
            Category = category;
            Amount = amount;
        }

        public int Month { get; }
        public string Category { get; }
        public decimal Amount { get; }
    }
}
