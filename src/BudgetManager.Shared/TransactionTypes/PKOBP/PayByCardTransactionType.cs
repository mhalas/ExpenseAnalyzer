namespace BudgetManager.Shared.TransactionTypes.PKOBP
{
    /// <summary>
    /// Płatność kartą
    /// </summary>
    public class PayByCardTransactionType : BaseTransactionType, ITransactionType
    {
        private int TargetAccountIndex = 5;
        private int TargetNameIndex = 6;

        public override string GetTargetAccount(string[] rowColumns)
        {
            return rowColumns[TargetAccountIndex].Split(":")[1].TrimStart().TrimEnd();
        }

        public override string GetTargetName(string[] rowColumns)
        {
            return rowColumns[TargetNameIndex]
                .Split("Adres:")[1].Split("Miasto:")[0].TrimStart().TrimEnd();
        }

        public override string GetDescription(string[] rowColumns)
        {
            return GetTargetName(rowColumns);
        }
    }
}
