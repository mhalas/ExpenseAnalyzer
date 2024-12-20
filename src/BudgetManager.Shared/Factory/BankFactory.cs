using BudgetManager.Shared.Configuration;
using BudgetManager.Shared.Enum;
using BudgetManager.Shared.Filters;
using BudgetManager.Shared.Filters.PKOBP;
using BudgetManager.Shared.Modifiers;
using BudgetManager.Shared.Modifiers.PKOBP;
using BudgetManager.Shared.TransactionsProcessors;
using BudgetManager.Shared.TransactionsProcessors.PKOBP;
using System.Collections.Generic;

namespace BudgetManager.Shared.Factory
{
    public class BankFactory(ConfigurationDto configuration)
    {
        public ITransactionsProcessor GetBankAnalyzer(BankType type)
        {
            switch (type)
            {
                case BankType.PkoBP:
                default:
                    return GetPKOBPTransactionProcessor();
            }

        }

        private ITransactionsProcessor GetPKOBPTransactionProcessor()
        {
            var filterAggregator = new FilterAggregator(new List<ITransactionFilter>()
            {
                new BankDepositFilter()
            });

            var modifierAggregator = new ModifierAggregator(new List<ITransactionModifier>()
            {
                new SumDepositTransactionsModifier()
            });

            return new FilteredTransactionsProcessorWrapper(
                    filterAggregator,
                    new ModifiedTransactionsProcessorWrapper(
                        modifierAggregator,
                        new TransactionsProcessor(configuration)));
        }
    }
}
