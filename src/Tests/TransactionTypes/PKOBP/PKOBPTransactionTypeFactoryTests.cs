using NUnit.Framework;
using Shared.TransactionTypes.PKOBP;

namespace Tests.TransactionTypes.PKOBP
{
    [TestFixture]
    public class PKOBPTransactionTypeFactoryTests
    {
        private PKOBPTransactionTypeFactory _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new PKOBPTransactionTypeFactory();
        }

        [TestCase("Naliczenie odsetek")]
        [TestCase("Opłata")]
        [TestCase("Opłata za użytkowanie karty")]
        [TestCase("Podatek od odsetek")]
        [TestCase("Prowizja")]
        [TestCase("Wpłata gotówki we wpłatomacie")]
        [TestCase("Wypłata gotówkowa z kasy")]
        public void ReturnBasicTransactionTypeFor(string transactionType)
        {
            Assert.That(_factory.GetTransactionType(transactionType), Is.TypeOf<BasicTransactionType>());
        }

        [TestCase("MOBILE_PAYMENT_C2C_EXTERNAL")]
        [TestCase("MOBILE_PAYMENT_C2C")]
        [TestCase("Przelew na konto")]
        [TestCase("Przelew natychmiastowy")]
        [TestCase("Przelew Natychmiastowy na konto")]
        [TestCase("Przelew Paybynet")]
        [TestCase("Przelew zagraniczny i walutowy")]
        [TestCase("Wpłata gotówkowa w kasie")]
        [TestCase("Zlecenie stałe")]
        public void ReturnBankTransferTransactionTypeFor(string transactionType)
        {
            Assert.That(_factory.GetTransactionType(transactionType), Is.TypeOf<BankTransferTransactionType>());
        }

        [TestCase("Przelew z rachunku")]
        public void ReturnTransferFromAccountTransactionTypeFor(string transactionType)
        {
            Assert.That(_factory.GetTransactionType(transactionType), Is.TypeOf<TransferFromAccountTransactionType>());
        }

        [TestCase("Płatność kartą")]
        public void ReturnPayByCardTransactionTypeFor(string transactionType)
        {
            Assert.That(_factory.GetTransactionType(transactionType), Is.TypeOf<PayByCardTransactionType>());
        }

        [TestCase("Płatność web - kod mobilny")]
        public void ReturnPayByWebTransactionTypeFor(string transactionType)
        {
            Assert.That(_factory.GetTransactionType(transactionType), Is.TypeOf<PayByWebTransactionType>());
        }

        [TestCase("Wypłata w bankomacie - kod mobilny")]
        [TestCase("Wypłata z bankomatu")]
        [TestCase("Zwrot płatności kartą")]
        public void ReturnWithdrawTransactionTypeFor(string transactionType)
        {
            Assert.That(_factory.GetTransactionType(transactionType), Is.TypeOf<WithdrawTransactionType>());
        }

        [TestCase("Autooszczędzanie")]
        [TestCase("Obciążenie")]
        [TestCase("Uznanie")]
        public void ReturnIgnoredTransactionTypeFor(string transactionType)
        {
            Assert.That(_factory.GetTransactionType(transactionType), Is.TypeOf<IgnoredTransactionType>());
        }

        [TestCase("X")]
        public void ReturnOtherTransactionTypeFor(string transactionType)
        {
            Assert.That(_factory.GetTransactionType(transactionType), Is.TypeOf<OtherTransactionType>());
        }
    }
}
