namespace Shared.TransactionTypes.PKOBP
{
    /// <summary>
    /// Płatność web - kod mobilny
    /// </summary>
    public class PayByWebTransactionType : BaseTransactionType, ITransactionType
    {
        private const int TargetNameIndex = 7;
        private const int DescriptionIndex = 8;

        public override string GetDescription(string[] rowColumns)
        {
            if(rowColumns[DescriptionIndex].Contains("Tytuł : "))
            {
                return rowColumns[DescriptionIndex].Split("Tytuł : ")[1];
            }

            return string.Empty;
        }

        public override string GetTargetAccount(string[] rowColumns)
        {
            return string.Empty;
        }

        public override string GetTargetName(string[] rowColumns)
        {
            if(rowColumns[TargetNameIndex].Contains("Numer telefonu : "))
            {
                return rowColumns[TargetNameIndex + 1].Split("Lokalizacja : Adres : ")[1];
            }

            if(rowColumns[TargetNameIndex].Contains("Lokalizacja : Adres : "))
            {
                return rowColumns[TargetNameIndex].Split("Lokalizacja : Adres : ")[1];
            }
            
            return string.Empty;
        }
    }
}
