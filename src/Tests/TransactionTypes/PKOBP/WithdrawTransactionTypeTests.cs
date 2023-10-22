using NUnit.Framework;
using Shared.TransactionTypes;
using Shared.TransactionTypes.PKOBP;
using System.Globalization;

namespace Tests.TransactionTypes.PKOBP
{
    [TestFixture]
    internal class WithdrawTransactionTypeTests
    {
        private WithdrawTransactionType _transactionType;

        [SetUp]
        public void Setup()
        {
            _transactionType = new WithdrawTransactionType();
        }

        [TestFixture]
        public class WhenPassCorrectTransactionRow : WithdrawTransactionTypeTests
        {
            private static DateTime _transactionDate = new DateTime(2023, 01, 15);
            private const decimal _amount = -1000m;
            private const string _currency = "PLN";

            private const string description = "PKO Withdraw";
            private const string address = "UL. TESTOWA 1";

            private string _testedRowTemplate = "\"2023-01-16\"," +
                "\"{0}\"," +
                "\"Wypłata z bankomatu\"," +
                "\"{1}\"," +
                "\"{2}\"," +
                "\"0\"," +
                "\"Tytuł : {3}\"," +
                "\"Lokalizacja : Adres : {4} Miasto : TESTOWO Kraj : POLSKA\"," +
                "\"Data i czas operacji : 2023-01-16\"," +
                "\"Kwota Cash Back : 0.00\"," +
                "\"Oryginalna kwota operacji : 1000.00\"," +
                "\"Numer karty : 123456******9876\"";

            private TransactionRow GetTransactionRow()
            {
                var testedRow = string.Format(CultureInfo.InvariantCulture, _testedRowTemplate,
                    _transactionDate.ToString("yyyy-MM-dd"),
                    _amount,
                    _currency,
                    description,
                    address);

                var rowColumns = testedRow
                    .Split(new string[] { "\",\"" }, StringSplitOptions.None)
                    .Select(x => x.Replace("\"", ""))
                    .ToArray();

                return _transactionType.GetTransactionRow(rowColumns);
            }

            [Test]
            public void TransactionDateIsCorrect()
            {
                var transactionRow = GetTransactionRow();
                Assert.That(transactionRow.TransactionDate, Is.EqualTo(_transactionDate));
            }

            [Test]
            public void AmountIsCorrect()
            {
                var transactionRow = GetTransactionRow();
                Assert.That(transactionRow.Amount, Is.EqualTo(_amount));
            }

            [Test]
            public void CurrencyIsCorrect()
            {
                var transactionRow = GetTransactionRow();
                Assert.That(transactionRow.Currency, Is.EqualTo(_currency));
            }

            [Test]
            public void TargetAccountIsEmpty()
            {
                var transactionRow = GetTransactionRow();
                Assert.That(transactionRow.TargetAccount, Is.Empty);
            }

            [Test]
            public void TargetNameIsEmpty()
            {
                var transactionRow = GetTransactionRow();
                Assert.That(transactionRow.TargetName, Is.Empty);
            }

            [Test]
            public void DescriptionIsCorrect()
            {
                var expectedDescription = $@"Wypłata z bankomatu; Tytuł : {description}; Lokalizacja : Adres : {address} Miasto : TESTOWO Kraj : POLSKA";

                var transactionRow = GetTransactionRow();
                Assert.That(transactionRow.Description, Is.EqualTo(expectedDescription));
            }
        }
    }
}
