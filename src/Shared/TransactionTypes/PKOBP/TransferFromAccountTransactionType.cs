namespace Shared.TransactionTypes.PKOBP
{
    public class TransferFromAccountTransactionType : BaseTransactionType, ITransactionType
    {
        public override string GetDescription(string[] rowColumns)
        {
            if (rowColumns[6].Contains("Tytuł : "))
            {
                return rowColumns[6].Split("Tytuł : ")[1].Split("OD: ")[0];
            }

            if (rowColumns[7].Contains("Tytuł : "))
            {
                return rowColumns[7].Split("Tytuł : ")[1].Split("OD: ")[0];
            }

            return rowColumns[8].Split("Tytuł : ")[1].Split("OD: ")[0];
        }

        public override string GetTargetAccount(string[] rowColumns)
        {
            if (rowColumns[5].Contains("Rachunek odbiorcy : "))
            {
                return rowColumns[5].Split("Rachunek odbiorcy : ")[1];
            }

            if (rowColumns[5].Contains("Rachunek nadawcy : "))
            {
                return rowColumns[5].Split("Rachunek nadawcy : ")[1];
            }

            return string.Empty;
        }

        public override string GetTargetName(string[] rowColumns)
        {
            if (rowColumns[5].Contains("Nazwa odbiorcy : "))
            {
                return rowColumns[5].Split("Nazwa odbiorcy : ")[1];
            }

            if (rowColumns[6].Contains("Nazwa odbiorcy : "))
            {
                return rowColumns[6].Split("Nazwa odbiorcy : ")[1];
            }

            if (rowColumns[5].Contains("Nazwa nadawcy : "))
            {
                return rowColumns[5].Split("Nazwa nadawcy : ")[1];
            }

            if (rowColumns[6].Contains("Nazwa nadawcy : "))
            {
                return rowColumns[6].Split("Nazwa nadawcy : ")[1];
            }

            return string.Empty;
        }
    }
}
