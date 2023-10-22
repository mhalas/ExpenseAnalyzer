namespace Shared.TransactionTypes.PKOBP
{
    /// <summary>
    /// Płatność kartą
    /// </summary>
    public class PayByCardTransactionType : BaseTransactionType, ITransactionType
    {
        private int TargetAccountIndex = 6;
        private int TargetNameIndex = 7;

        public override string GetTargetAccount(string[] rowColumns)
        {
            return rowColumns[TargetAccountIndex].Split(" : ")[1];
        }

        public override string GetTargetName(string[] rowColumns)
        {
            return rowColumns[TargetNameIndex].Split("Adres : ")[1].Split(" Miasto : ")[0];
        }

        public override string GetDescription(string[] rowColumns)
        {
            return string.Empty;
        }
    }
}
