namespace Shared.TransactionTypes.PKOBP
{
    public class TransferFromAccountTransactionType : BaseTransactionType, ITransactionType
    {
        public override string GetDescription(string[] rowColumns)
        {
            if (rowColumns[7].Contains("Tytuł : "))
            {
                return rowColumns[7].Split("Tytuł : ")[1].Split("OD: ")[0];
            }

            if (rowColumns[8].Contains("Tytuł : "))
            {
                return rowColumns[8].Split("Tytuł : ")[1].Split("OD: ")[0];
            }

            return rowColumns[9].Split("Tytuł : ")[1].Split("OD: ")[0];
        }

        public override string GetTargetAccount(string[] rowColumns)
        {
            if (rowColumns[6].Contains("Rachunek odbiorcy : "))
            {
                return rowColumns[6].Split("Rachunek odbiorcy : ")[1];
            }

            if (rowColumns[6].Contains("Rachunek nadawcy : "))
            {
                return rowColumns[6].Split("Rachunek nadawcy : ")[1];
            }

            return string.Empty;
        }

        public override string GetTargetName(string[] rowColumns)
        {
            if (rowColumns[6].Contains("Nazwa odbiorcy : "))
            {
                return rowColumns[6].Split("Nazwa odbiorcy : ")[1];
            }

            if (rowColumns[7].Contains("Nazwa odbiorcy : "))
            {
                return rowColumns[7].Split("Nazwa odbiorcy : ")[1];
            }

            if (rowColumns[6].Contains("Nazwa nadawcy : "))
            {
                return rowColumns[6].Split("Nazwa nadawcy : ")[1];
            }

            if (rowColumns[7].Contains("Nazwa nadawcy : "))
            {
                return rowColumns[7].Split("Nazwa nadawcy : ")[1];
            }

            return string.Empty;
        }
    }
}
