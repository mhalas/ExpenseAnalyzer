namespace BudgetManager.Shared.TransactionTypes
{
    public interface ITransactionType
    {
        TransactionRow GetTransactionRow(string[] rowColumns);
    }
}
