namespace Shared.TransactionTypes.PKOBP
{
    /// <summary>
    /// Autooszczędzanie, Uznanie, Obciążenie
    /// </summary>
    public class IgnoredTransactionType : ITransactionType
    {
        public TransactionRow GetTransactionRow(string[] rowColumns)
        {
            return null;
        }
    }
}
