using NUnit.Framework;
using Shared.TransactionTypes;
using Shared.TransactionTypes.PKOBP;
using System.Collections;
using System.Globalization;

namespace Tests.TransactionTypes.PKOBP
{
    [TestFixture]
    public class BasicTransactionTypeTests
    {
        private BasicTransactionType _transactionType;

        [SetUp]
        public void Setup()
        {
            _transactionType = new BasicTransactionType();
        }

        private class TestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return TestCase.Fee;
                yield return TestCase.CardFee;
                yield return TestCase.DepositInterest;
                yield return TestCase.Commision;
            }
        }

        [TestFixtureSource(typeof(TestCases))]
        public class WhenPassCorrectTransactionRow : BasicTransactionTypeTests
        {
            private readonly TestCase _testCase;

            public WhenPassCorrectTransactionRow(TestCase testCase)
            {
                _testCase = testCase;
            }


            private TransactionRow GetTransactionRow()
            {
                var testedRow = string.Format(CultureInfo.InvariantCulture, _testCase.RowTemplate,
                    _testCase.TransactionDate.ToString("yyyy-MM-dd"),
                    _testCase.Amount,
                    _testCase.Currency,
                    _testCase.Description);

                var rowColumns = testedRow
                    .Split(new string[] { "\",\"" }, StringSplitOptions.None)
                    .Select(x=> x.Replace("\"", ""))
                    .ToArray();

                return _transactionType.GetTransactionRow(rowColumns);
            }

            [Test]
            public void TransactionDateIsCorrect()
            {
                var transactionRow = GetTransactionRow();
                Assert.That(transactionRow.TransactionDate, Is.EqualTo(_testCase.TransactionDate));
            }

            [Test]
            public void AmountIsCorrect()
            {
                var transactionRow = GetTransactionRow();
                Assert.That(transactionRow.Amount, Is.EqualTo(_testCase.Amount));
            }

            [Test]
            public void CurrencyIsCorrect()
            {
                var transactionRow = GetTransactionRow();
                Assert.That(transactionRow.Currency, Is.EqualTo(_testCase.Currency));
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
                var transactionRow = GetTransactionRow();
                Assert.That(transactionRow.Description, Is.EqualTo(_testCase.ExpectedDescription));
            }
        }
    }
}
