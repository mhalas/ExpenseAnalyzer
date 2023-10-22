using Shared.BankAnalyzer;
using Shared.Configuration;
using Shared.Enum;
using Shared.TransactionTypes.PKOBP;

namespace Shared.Factory
{
    public class BankFactory
    {
        private readonly ConfigurationDto _configuration;

        public BankFactory(ConfigurationDto configuration)
        {
            _configuration = configuration;
        }

        public IBankAnalyzer GetBankAnalyzer(BankType type)
        {
            switch (type)
            {
                case BankType.PkoBP:
                default:
                    return new PkoBpDataAnalyzer(_configuration, new PKOBPTransactionTypeFactory());
            }

        }
    }
}
