using BudgetManager.Shared.Models;
using BudgetManager.Shared.Modifiers.PKOBP;
using NUnit.Framework;

namespace Tests.Modifier.PKOBP
{
    [TestFixture]
    public class SumDepositTransactionsModifierTests
    {
        private SumDepositTransactionsModifier _modifier;

        [SetUp]
        public void Setup()
        {
            _modifier = new SumDepositTransactionsModifier();
        }

        [Test]
        public void WhenThereIsRenewableDepositTransaction_AddNewTransactionsWithProfit()
        {
            var dateTime = DateTime.Now;

            var list = new List<TransactionResultRow>()
            {
                new TransactionResultRow(dateTime, 
                -100, 
                "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000001 M DO 01-01-2000 ODNAWIALNAOPROC.ST.  1,700000%; ; ; ",
                "",
                "",
                "1"),
                new TransactionResultRow(dateTime.AddDays(1),
                150,
                "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000001 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                "",
                "",
                "2")
            };

            var result = _modifier.ModifyTransactions(list).ToList();

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].TargetName, Is.EqualTo("1"));
            Assert.That(result[0].Amount, Is.EqualTo(-100));
            Assert.That(result[1].TargetName, Is.EqualTo("2"));
            Assert.That(result[1].Amount, Is.EqualTo(150));

            Assert.That(result[2].TargetName, Is.EqualTo("1"));
            Assert.That(result[2].ValueDate, Is.EqualTo(dateTime.AddDays(1)));
            Assert.That(result[2].Description, Is.EqualTo("Zysk z lokaty nr 000000000000000000000000000000001"));
            Assert.That(result[2].Amount, Is.EqualTo(50));
            Assert.That(result[2].Category, Is.EqualTo("Lokata"));
        }

        [Test]
        public void WhenThereIsRenewableDepositTransactionAndOtherTransactions_AddNewTransactionsWithProfitForClosedDeposits()
        {
            var dateTime = DateTime.Now;

            var list = new List<TransactionResultRow>()
            {
                new TransactionResultRow(dateTime,
                    -150,
                    "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000001 M DO 01-01-2000 OPROC.ST.  1,700000%; ; ; ",
                    "",
                    "",
                    "1"),
                new TransactionResultRow(dateTime,
                    -150,
                    "test",
                    "",
                    "",
                    "2"),
                new TransactionResultRow(dateTime,
                    -100,
                    "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000002 M DO 01-01-2000 ODNAWIALNAOPROC.ST.  1,700000%; ; ; ",
                    "",
                    "",
                    "3"),
                new TransactionResultRow(dateTime.AddDays(1),
                    150,
                    "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000002 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    "",
                    "",
                    "4"),
                new TransactionResultRow(dateTime,
                    -150,
                    "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000003 M DO 01-01-2000 OPROC.ST.  1,700000%; ; ; ",
                    "",
                    "",
                    "5")
            };

            var result = _modifier.ModifyTransactions(list).ToList();

            Assert.That(result.Count, Is.EqualTo(6));
            Assert.That(result[0].TargetName, Is.EqualTo("1"));
            Assert.That(result[0].Amount, Is.EqualTo(-150));
            Assert.That(result[1].TargetName, Is.EqualTo("2"));
            Assert.That(result[1].Amount, Is.EqualTo(-150));
            Assert.That(result[2].TargetName, Is.EqualTo("3"));
            Assert.That(result[2].Amount, Is.EqualTo(-100));
            Assert.That(result[3].TargetName, Is.EqualTo("4"));
            Assert.That(result[3].Amount, Is.EqualTo(150));
            Assert.That(result[4].TargetName, Is.EqualTo("5"));
            Assert.That(result[4].Amount, Is.EqualTo(-150));

            Assert.That(result[5].TargetName, Is.EqualTo("3"));
            Assert.That(result[5].ValueDate, Is.EqualTo(dateTime.AddDays(1)));
            Assert.That(result[5].Description, Is.EqualTo("Zysk z lokaty nr 000000000000000000000000000000002"));
            Assert.That(result[5].Amount, Is.EqualTo(50));
            Assert.That(result[5].Category, Is.EqualTo("Lokata"));
        }

        [Test]
        public void WhenThereIsManyRenewableDepositTransactions_AddNewTransactionsWithProfit()
        {
            var dateTime = DateTime.Now;

            var list = new List<TransactionResultRow>()
            {
                new TransactionResultRow(dateTime, 
                    -100,
                    "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000001 M DO 01-01-2000 ODNAWIALNAOPROC.ST.  1,700000%; ; ; ",
                    "",
                    "",
                    "1"),
                new TransactionResultRow(dateTime,
                    -200,
                    "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000002 M DO 01-01-2000 ODNAWIALNAOPROC.ST.  1,700000%; ; ; ",
                    "",
                    "",
                    "2"),
                new TransactionResultRow(dateTime.AddDays(1),
                    210,
                    "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000002 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    "",
                    "",
                    "3"),
                new TransactionResultRow(dateTime.AddDays(1),
                    105,
                    "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000001 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    "",
                    "",
                    "4"),
            };

            var result = _modifier.ModifyTransactions(list).ToList();

            Assert.That(result.Count, Is.EqualTo(6));
            Assert.That(result[0].TargetName, Is.EqualTo("1"));
            Assert.That(result[0].Amount, Is.EqualTo(-100));
            Assert.That(result[1].TargetName, Is.EqualTo("2"));
            Assert.That(result[1].Amount, Is.EqualTo(-200));
            Assert.That(result[2].TargetName, Is.EqualTo("3"));
            Assert.That(result[2].Amount, Is.EqualTo(210));
            Assert.That(result[3].TargetName, Is.EqualTo("4"));
            Assert.That(result[3].Amount, Is.EqualTo(105));

            Assert.That(result[4].TargetName, Is.EqualTo("1"));
            Assert.That(result[4].ValueDate, Is.EqualTo(dateTime.AddDays(1)));
            Assert.That(result[4].Description, Is.EqualTo("Zysk z lokaty nr 000000000000000000000000000000001"));
            Assert.That(result[4].Amount, Is.EqualTo(5));
            Assert.That(result[4].Category, Is.EqualTo("Lokata"));

            Assert.That(result[5].TargetName, Is.EqualTo("2"));
            Assert.That(result[5].ValueDate, Is.EqualTo(dateTime.AddDays(1)));
            Assert.That(result[5].Description, Is.EqualTo("Zysk z lokaty nr 000000000000000000000000000000002"));
            Assert.That(result[5].Amount, Is.EqualTo(10));
            Assert.That(result[5].Category, Is.EqualTo("Lokata"));
        }
    }
}
