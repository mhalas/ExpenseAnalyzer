namespace BudgetManager.Shared.TransactionTypes.PKOBP
{
    /// <summary>
    /// Naliczenie odsetek, Opłata, Opłata za użytkowanie karty, Prowizja
    /// </summary>
    public class BasicTransactionType : BaseTransactionType, ITransactionType
    {
        private const int TransactionTypeIndex = 2;
        private const int DescriptionIndex = 5;

        public override string GetDescription(string[] rowColumns)
        {
            if (rowColumns[DescriptionIndex].Contains("Tytuł:"))
            {
                var title = rowColumns[DescriptionIndex].Split("Tytuł: ")[1];
                return $@"{rowColumns[TransactionTypeIndex]}; {title}";
            }

            return $@"{rowColumns[TransactionTypeIndex]}";
        }

        public override string GetTargetAccount(string[] rowColumns)
        {
            return string.Empty;
        }

        public override string GetTargetName(string[] rowColumns)
        {
            return string.Empty;
        }
    }
}
