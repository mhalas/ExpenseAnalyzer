namespace Shared.TransactionTypes.PKOBP
{
    /// <summary>
    /// Wypłata z bankomatu
    /// </summary>
    public class WithdrawTransactionType : BaseTransactionType, ITransactionType
    {
        private const int TypeNameIndex = 2;
        private const int DescriptionIndex = 6;
        private const int AddressIndex = 7;

        public override string GetDescription(string[] rowColumns)
        {
            var type = rowColumns[TypeNameIndex];
            var description = rowColumns[DescriptionIndex];
            var address = rowColumns[AddressIndex];

            return $@"{type}; {description}; {address}";
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
