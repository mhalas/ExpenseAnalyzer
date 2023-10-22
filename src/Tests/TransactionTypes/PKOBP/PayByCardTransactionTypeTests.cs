using NUnit.Framework;
using Shared.TransactionTypes;
using Shared.TransactionTypes.PKOBP;
using System.Collections;
using System.Globalization;

namespace Tests.TransactionTypes.PKOBP
{
    [TestFixture]
    public class PayByCardTransactionTypeTests
    {
        private PayByCardTransactionType _transactionType;

        [SetUp]
        public void Setup()
        {
            _transactionType = new PayByCardTransactionType();
        }

        private class TestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return TestCase.PayByCard;
            }
        }

        [TestFixtureSource(typeof(TestCases))]
        public class WhenPassCorrectTransactionRow : PayByCardTransactionTypeTests
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
                    _testCase.TargetAccount,
                    _testCase.TargetName);

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
            public void TargetAccountIsCorrect()
            {
                var transactionRow = GetTransactionRow();
                Assert.That(transactionRow.TargetAccount, Is.EqualTo(_testCase.TargetAccount));
            }

            [Test]
            public void TargetNameIsCorrect()
            {
                var transactionRow = GetTransactionRow();
                Assert.That(transactionRow.TargetName, Is.EqualTo(_testCase.TargetName));
            }

            [Test]
            public void DescriptionIsEmpty()
            {
                var transactionRow = GetTransactionRow();
                Assert.That(transactionRow.Description, Is.Empty);
            }
        }
    }
}
