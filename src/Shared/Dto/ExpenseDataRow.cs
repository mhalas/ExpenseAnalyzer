using System;

namespace Shared.Dto
{
    public class ExpenseTransaction
    {
        public DateTime ValueDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string TargetAccount { get; set; }
        public string TargetName { get; set; }
        public string Category { get; set; }
    }
}
