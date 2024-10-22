using NUnit.Framework;
using Shared.Dto;
using Shared.Modifiers.PKOBP;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void WhenThereIsRenewableDepositTransaction_ModifyIt()
        {
            var dateTime = DateTime.Now;

            var list = new List<ExpenseTransaction>()
            {
                new ExpenseTransaction()
                {
                    TargetName = "1",
                    ValueDate = dateTime,
                    Description = "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000001 M DO 01-01-2000 ODNAWIALNAOPROC.ST.  1,700000%; ; ; ",
                    Amount = -100,
                },
                new ExpenseTransaction()
                {
                    TargetName = "2",
                    ValueDate = dateTime.AddDays(1),
                    Description = "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000001 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    Amount = 150,
                }
            };

            var result = _modifier.ModifyTransactions(list).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].TargetName, Is.EqualTo("1"));
            Assert.That(result[0].ValueDate, Is.EqualTo(dateTime));
            Assert.That(result[0].Description, Is.EqualTo("Zysk z lokaty nr 000000000000000000000000000000001"));
            Assert.That(result[0].Amount, Is.EqualTo(50));
            Assert.That(result[0].Category, Is.EqualTo("Lokata"));
        }

        [Test]
        public void WhenThereIsRenewableDepositTransactionAndOtherTransactions_ModifyOnlyRenewableDepositTransaction()
        {
            var dateTime = DateTime.Now;

            var list = new List<ExpenseTransaction>()
            {
                new ExpenseTransaction()
                {
                    TargetName = "1",
                    ValueDate = dateTime,
                    Description = "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000001 M DO 01-01-2000 OPROC.ST.  1,700000%; ; ; ",
                    Amount = -150,
                },
                new ExpenseTransaction()
                {
                    TargetName = "2",
                    ValueDate = dateTime,
                    Description = "test",
                    Amount = -150,
                },
                new ExpenseTransaction()
                {
                    TargetName = "3",
                    ValueDate = dateTime,
                    Description = "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000002 M DO 01-01-2000 ODNAWIALNAOPROC.ST.  1,700000%; ; ; ",
                    Amount = -100,
                },
                new ExpenseTransaction()
                {
                    TargetName = "4",
                    ValueDate = dateTime.AddDays(1),
                    Description = "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000002 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    Amount = 150,
                },
                new ExpenseTransaction()
                {
                    TargetName = "5",
                    ValueDate = dateTime,
                    Description = "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000003 M DO 01-01-2000 OPROC.ST.  1,700000%; ; ; ",
                    Amount = -150,
                },

            };

            var result = _modifier.ModifyTransactions(list).ToList();

            Assert.That(result.Count, Is.EqualTo(4));
            Assert.That(result[0].TargetName, Is.EqualTo("1"));
            Assert.That(result[1].TargetName, Is.EqualTo("2"));
            Assert.That(result[2].TargetName, Is.EqualTo("3"));
            Assert.That(result[3].TargetName, Is.EqualTo("5"));
        }

        [Test]
        public void WhenThereIsManyRenewableDepositTransactions_ModifyThem()
        {
            var dateTime = DateTime.Now;

            var list = new List<ExpenseTransaction>()
            {
                new ExpenseTransaction()
                {
                    TargetName = "1",
                    ValueDate = dateTime,
                    Description = "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000001 M DO 01-01-2000 ODNAWIALNAOPROC.ST.  1,700000%; ; ; ",
                    Amount = -100,
                },
                new ExpenseTransaction()
                {
                    TargetName = "2",
                    ValueDate = dateTime,
                    Description = "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000002 M DO 01-01-2000 ODNAWIALNAOPROC.ST.  1,700000%; ; ; ",
                    Amount = -200,
                },
                new ExpenseTransaction()
                {
                    TargetName = "3",
                    ValueDate = dateTime.AddDays(1),
                    Description = "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000002 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    Amount = 205,
                },
                new ExpenseTransaction()
                {
                    TargetName = "4",
                    ValueDate = dateTime.AddDays(1),
                    Description = "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000001 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    Amount = 105,
                },
            };

            var result = _modifier.ModifyTransactions(list).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].TargetName, Is.EqualTo("1"));
            Assert.That(result[0].ValueDate, Is.EqualTo(dateTime));
            Assert.That(result[0].Description, Is.EqualTo("Zysk z lokaty nr 000000000000000000000000000000001"));
            Assert.That(result[0].Amount, Is.EqualTo(5));
            Assert.That(result[0].Category, Is.EqualTo("Lokata"));

            Assert.That(result[1].TargetName, Is.EqualTo("2"));
            Assert.That(result[1].ValueDate, Is.EqualTo(dateTime));
            Assert.That(result[1].Description, Is.EqualTo("Zysk z lokaty nr 000000000000000000000000000000002"));
            Assert.That(result[1].Amount, Is.EqualTo(5));
            Assert.That(result[1].Category, Is.EqualTo("Lokata"));
        }
    }
}
