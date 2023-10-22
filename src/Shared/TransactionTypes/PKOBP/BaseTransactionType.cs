using System;
using System.Globalization;

namespace Shared.TransactionTypes.PKOBP
{
    public abstract class BaseTransactionType : ITransactionType
    {
        private int TransactionDateIndex = 1;
        private int AmountIndex = 3;
        private int CurrencyIndex = 4;

        public virtual TransactionRow GetTransactionRow(string[] rowColumns)
        {
            return new TransactionRow(
                DateTime.Parse(rowColumns[TransactionDateIndex], CultureInfo.InvariantCulture),
                decimal.Parse(rowColumns[AmountIndex], CultureInfo.InvariantCulture),
                rowColumns[CurrencyIndex],
                GetTargetAccount(rowColumns),
                GetTargetName(rowColumns),
                GetDescription(rowColumns));
        }

        public abstract string GetTargetAccount(string[] rowColumns);
        public abstract string GetTargetName(string[] rowColumns);
        public abstract string GetDescription(string[] rowColumns);
    }
}
