namespace Shared.TransactionTypes.PKOBP
{
    /// <summary>
    /// Naliczenie odsetek, Opłata, Opłata za użytkowanie karty, Prowizja
    /// </summary>
    public class BasicTransactionType : BaseTransactionType, ITransactionType
    {
        private const int TransactionTypeIndex = 2;
        private const int DescriptionIndex = 6;

        public override string GetDescription(string[] rowColumns)
        {
            var title = rowColumns[DescriptionIndex].Split("Tytuł : ")[1];

            return $@"{rowColumns[TransactionTypeIndex]}; {title}";
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
