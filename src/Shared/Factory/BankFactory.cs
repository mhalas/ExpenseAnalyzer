using Shared.Configuration;
using Shared.Enum;
using Shared.Filters;
using Shared.Filters.PKOBP;
using Shared.Modifiers;
using Shared.Modifiers.PKOBP;
using Shared.TransactionsProcessors;
using Shared.TransactionsProcessors.PKOBP;
using System.Collections.Generic;

namespace Shared.Factory
{
    public class BankFactory
    {
        private readonly ConfigurationDto _configuration;

        public BankFactory(ConfigurationDto configuration)
        {
            _configuration = configuration;
        }

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
                        new TransactionsProcessor(_configuration)));
        }
    }
}
