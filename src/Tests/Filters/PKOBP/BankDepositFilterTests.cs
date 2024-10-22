using NUnit.Framework;
using Shared.Dto;
using Shared.Filters.PKOBP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var list = new List<ExpenseTransaction>()
            {
                new ExpenseTransaction()
                {
                    TargetName = "1",
                    ValueDate = DateTime.Now,
                    Description = "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000001 M DO 01-01-2000 ODNAWIALNAOPROC.ST.  1,700000%; ; ; ",
                    Amount = -100,
                },
                new ExpenseTransaction()
                {
                    TargetName = "2",
                    ValueDate = DateTime.Now.AddDays(1),
                    Description = "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000001 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    Amount = 150,
                },

                new ExpenseTransaction()
                {
                    TargetName = "3",
                    ValueDate = DateTime.Now,
                    Description = "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000002 M DO 01-01-2000 OPROC.ST.  1,700000%; ; ; ",
                    Amount = -100,
                },
                new ExpenseTransaction()
                {
                    TargetName = "4",
                    ValueDate = DateTime.Now.AddDays(1),
                    Description = "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000002 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    Amount = 150,
                }
            };

            var result = _filter.FilterTransactions(list).ToList();
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result[0].TargetName, Is.EqualTo("1"));
            Assert.That(result[1].TargetName, Is.EqualTo("2"));
        }

        [Test]
        public void CorrectlyFilteredListForDepositsWhenThereIsOtherTransaction()
        {
            var list = new List<ExpenseTransaction>()
            {
                new ExpenseTransaction()
                {
                    TargetName = "1",
                    ValueDate = DateTime.Now,
                    Description = "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000001 M DO 01-01-2000 ODNAWIALNAOPROC.ST.  1,700000%; ; ; ",
                    Amount = -100,
                },
                new ExpenseTransaction()
                {
                    TargetName = "2",
                    ValueDate = DateTime.Now.AddDays(1),
                    Description = "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000001 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    Amount = 150,
                },

                new ExpenseTransaction()
                {
                    TargetName = "3",
                    ValueDate = DateTime.Now,
                    Description = "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000002 M DO 01-01-2000 OPROC.ST.  1,700000%; ; ; ",
                    Amount = -100,
                },
                new ExpenseTransaction()
                {
                    TargetName = "4",
                    ValueDate = DateTime.Now.AddDays(1),
                    Description = "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000002 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    Amount = 150,
                },
                new ExpenseTransaction()
                {
                    TargetName = "5",
                    ValueDate = DateTime.Now,
                    Description = "TESTTEST",
                    Amount = -1000,
                },
            };

            var result = _filter.FilterTransactions(list).ToList();
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result[0].TargetName, Is.EqualTo("1"));
            Assert.That(result[1].TargetName, Is.EqualTo("2"));
            Assert.That(result[2].TargetName, Is.EqualTo("5"));
        }

        [Test]
        public void CorrectlyFilteredListForDepositsWhenThereIsSingleTransactionWithoutReturn()
        {
            var list = new List<ExpenseTransaction>()
            {
                new ExpenseTransaction()
                {
                    TargetName = "1",
                    ValueDate = DateTime.Now,
                    Description = "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000001 M DO 01-01-2000 ODNAWIALNAOPROC.ST.  1,700000%; ; ; ",
                    Amount = -100,
                },
                new ExpenseTransaction()
                {
                    TargetName = "3",
                    ValueDate = DateTime.Now,
                    Description = "Obciążenie; Tytuł : OTWARCIE LOKATY NR       000000000000000000000000000000002 M DO 01-01-2000 OPROC.ST.  1,700000%; ; ; ",
                    Amount = -100,
                },
                new ExpenseTransaction()
                {
                    TargetName = "4",
                    ValueDate = DateTime.Now.AddDays(1),
                    Description = "Uznanie; Tytuł : ZERWANIE LOKATY NR       000000000000000000000000000000002 M OD 01-01-2000OPR.OBN    0,000000%; ; ; ",
                    Amount = 150,
                },
                new ExpenseTransaction()
                {
                    TargetName = "5",
                    ValueDate = DateTime.Now,
                    Description = "TESTTEST",
                    Amount = -1000,
                },
            };

            var result = _filter.FilterTransactions(list).ToList();
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result[0].TargetName, Is.EqualTo("5"));
        }
    }
}
