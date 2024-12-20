using System.Text;

namespace BudgetManager.Shared.TransactionTypes.PKOBP
{
    public class OtherTransactionType : BaseTransactionType, ITransactionType
    {
        private const int TransactionTypeIndex = 2;
        private const int DescriptionStartIndex = 5;
        private const int DescriptionEndIndex = 8;

        public override string GetDescription(string[] rowColumns)
        {
            StringBuilder descriptionBuilder = new StringBuilder();
            for (int i = DescriptionStartIndex; i <= DescriptionEndIndex; i++)
            {
                if(i != DescriptionStartIndex)
                {
                    descriptionBuilder.Append("; ");
                }

                if(rowColumns.Length > i)
                {
                    descriptionBuilder.Append($@"{rowColumns[i]}");
                }
            }

            var transactionType = rowColumns[TransactionTypeIndex];

            return $@"{transactionType}; {descriptionBuilder}";
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
