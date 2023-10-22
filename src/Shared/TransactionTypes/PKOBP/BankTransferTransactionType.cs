namespace Shared.TransactionTypes.PKOBP
{
    /// <summary>
    /// MOBILE_PAYMENT_C2C, MOBILE_PAYMENT_C2C_EXTERNAL
    /// </summary>
    public class BankTransferTransactionType : BaseTransactionType, ITransactionType
    {
        private const int TargetAccountIndex = 6;
        private const int TargetNameIndex = 7;
        private const int DescriptionIndex = 8;

        public override string GetTargetAccount(string[] rowColumns)
        {
            if (rowColumns[TargetAccountIndex].Contains("Rachunek odbiorcy : "))
            {
                return rowColumns[TargetAccountIndex].Split("Rachunek odbiorcy : ")[1];
            }

            if(rowColumns[TargetAccountIndex].Contains("Rachunek nadawcy : "))
            {
                return rowColumns[TargetAccountIndex].Split("Rachunek nadawcy : ")[1];
            }

            return string.Empty;
        }

        public override string GetTargetName(string[] rowColumns)
        {
            if (rowColumns[TargetAccountIndex].Contains("Nazwa odbiorcy : "))
            {
                return rowColumns[TargetAccountIndex].Split("Nazwa odbiorcy : ")[1];
            }

            if (rowColumns[TargetNameIndex].Contains("Nazwa odbiorcy : "))
            {
                return rowColumns[TargetNameIndex].Split("Nazwa odbiorcy : ")[1];
            }

            if (rowColumns[TargetAccountIndex].Contains("Nazwa nadawcy : "))
            {
                return rowColumns[TargetAccountIndex].Split("Nazwa nadawcy : ")[1];
            }

            if (rowColumns[TargetNameIndex].Contains("Nazwa nadawcy : "))
            {
                return rowColumns[TargetNameIndex].Split("Nazwa nadawcy : ")[1];
            }

            return string.Empty;
        }

        public override string GetDescription(string[] rowColumns)
        {
            if (rowColumns[DescriptionIndex].Contains("Adres"))
            {
                return rowColumns[DescriptionIndex + 1].Split("Tytuł : ")[1].Split("OD: ")[0];
            }

            return rowColumns[DescriptionIndex].Split("Tytuł : ")[1].Split("OD: ")[0];
        }
    }
}
