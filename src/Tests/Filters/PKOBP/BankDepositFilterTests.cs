using BudgetManager.Shared.Filters.PKOBP;
using BudgetManager.Shared.Models;
using NUnit.Framework;

namespace Tests.Filters.PKOBP
{
    [TestFixture]
    public class BankDepositFilterTests
    {
        private BankDepositFilter _filter;

        [SetUp]
        public void Setup()
        {
            _filter = new BankDepositFilter();
        }

        [Test]
        public void CorrectlyFilteredListForOnlyDeposits()
        {
            var list = new List<TransactionResultRow>()
            {
                new TransactionResultRow(DateTime.Now,
                    -100,
                    "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000001 M DO 01-01-2000 ODNAWIALNAOPROC.ST.  1,700000%; ; ; ",
                    "",
                    "",
                    "1"),
                new TransactionResultRow(DateTime.Now.AddDays(1),
                    150,
                    "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000001 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    "",
                    "",
                    "2"),

                new TransactionResultRow(DateTime.Now,
                    -100,
                    "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000002 M DO 01-01-2000 OPROC.ST.  1,700000%; ; ; ",
                    "",
                    "",
                    "3"),
                new TransactionResultRow(DateTime.Now.AddDays(1),
                    150,
                    "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000002 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    "",
                    "",
                    "4")
            };

            var result = _filter.FilterTransactions(list).ToList();
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void CorrectlyFilteredListForDepositsWhenThereIsOtherTransaction()
        {
            var list = new List<TransactionResultRow>()
            {
                new TransactionResultRow(DateTime.Now,
                    -100,
                    "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000001 M DO 01-01-2000 ODNAWIALNAOPROC.ST.  1,700000%; ; ; ",
                    "",
                    "",
                    "1"),
                new TransactionResultRow(DateTime.Now.AddDays(1),
                    150,
                    "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000001 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    "",
                    "",
                    "2"),

                new TransactionResultRow(DateTime.Now,
                    -100,
                    "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000002 M DO 01-01-2000 OPROC.ST.  1,700000%; ; ; ",
                    "",
                    "",
                    "3"),
                new TransactionResultRow(DateTime.Now.AddDays(1),
                    150,
                    "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000002 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    "",
                    "",
                    "4"),
                new TransactionResultRow(DateTime.Now,
                    -1000,
                    "TESTTEST",
                    "",
                    "",
                    "5"),
            };

            var result = _filter.FilterTransactions(list).ToList();
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result[0].TargetName, Is.EqualTo("5"));
        }

        [Test]
        public void CorrectlyFilteredListForDepositsWhenThereIsSingleTransactionWithoutReturn()
        {
            var list = new List<TransactionResultRow>()
            {
                new TransactionResultRow(DateTime.Now,
                    -100,
                    "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000001 M DO 01-01-2000 ODNAWIALNAOPROC.ST.  1,700000%; ; ; ",
                    "",
                    "",
                    "1"),
                new TransactionResultRow(DateTime.Now,
                    -100,
                    "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000002 M DO 01-01-2000 OPROC.ST.  1,700000%; ; ; ",
                    "",
                    "",
                    "3"),
                new TransactionResultRow(DateTime.Now.AddDays(1),
                    150,
                    "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000002 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    "",
                    "",
                    "4"),
                new TransactionResultRow(DateTime.Now,
                    -1000,
                    "TESTTEST",
                    "",
                    "",
                    "5"),
            };

            var result = _filter.FilterTransactions(list).ToList();
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result[0].TargetName, Is.EqualTo("5"));
        }
    }
}
