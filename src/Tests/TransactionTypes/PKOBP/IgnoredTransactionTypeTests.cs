using NUnit.Framework;
using Shared.TransactionTypes.PKOBP;

namespace Tests.TransactionTypes.PKOBP
{
    [TestFixture]
    public class IgnoredTransactionTypeTests
    {
        [Test]
        public void ReturnNull()
        {
            var testedRow = "\"2023-01-16\"," +
                "\"2023-01-15\"," +
                "\"Obciążenie\"," +
                "\"-50.00\"," +
                "\"PLN\"," +
                "\"100\"," +
                "\"Tytuł : OTWARCIE LOKATY OPROC.ST. 6,000000%\"";

            var rowColumns = testedRow
                    .Split(new string[] { "\",\"" }, StringSplitOptions.None)
                    .Select(x => x.Replace("\"", ""))
                    .ToArray();

            var transaction = new IgnoredTransactionType();
            Assert.IsNull(transaction.GetTransactionRow(rowColumns));
        }
    }
}
