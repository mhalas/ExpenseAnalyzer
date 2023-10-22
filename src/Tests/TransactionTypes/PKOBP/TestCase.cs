using System.Globalization;

namespace Tests.TransactionTypes.PKOBP
{
    public class TestCase
    {
        private TestCase()
        {

        }

        public DateTime TransactionDate { get; private set; }
        public decimal Amount { get; private set; }
        public string? Currency { get; private set; }
        public string TargetAccount { get; private set; }
        public string TargetName { get; private set; }
        public string? Description { get; private set; }
        public string? RowTemplate { get; private set; }

        public string? ExpectedDescription { get; private set; }

        public Func<string> GetRow;

        public static TestCase DepositInterest => new TestCase()
        {
            TransactionDate = new DateTime(2023, 01, 15),
            Amount = 500,
            Currency = "PLN",
            Description = "DESCRIPTION",
            RowTemplate = "\"2023-01-16\"," +
            "\"{0}\"," +
            "\"Naliczenie odsetek\"," +
            "\"{1}\"," +
            "\"{2}\"," +
            "\"100.25\"," +
            "\"Tytuł : {3}\"",

            ExpectedDescription = "Naliczenie odsetek; DESCRIPTION"
        };

        public static TestCase Fee => new TestCase()
        {
            TransactionDate = new DateTime(2023, 01, 15),
            Amount = -2,
            Currency = "PLN",
            Description = "DESCRIPTION",
            RowTemplate = "\"2023-01-16\"," +
            "\"{0}\"," +
            "\"Opłata\"," +
            "\"{1}\"," +
            "\"{2}\"," +
            "\"100.25\"," +
            "\"Tytuł : {3}\"",

            ExpectedDescription = "Opłata; DESCRIPTION"
        };

        public static TestCase CardFee => new TestCase()
        {
            TransactionDate = new DateTime(2023, 01, 15),
            Amount = -25,
            Currency = "PLN",
            Description = "OPŁATA PROP. ZA KARTĘ421234******9876, 20.01-21.01",
            RowTemplate = "\"2023-01-16\"," +
            "\"{0}\"," +
            "\"Opłata za użytkowanie karty\"," +
            "\"{1}\"," +
            "\"{2}\"," +
            "\"100.25\"," +
            "\"Tytuł : {3}\"",

            ExpectedDescription = "Opłata za użytkowanie karty; OPŁATA PROP. ZA KARTĘ421234******9876, 20.01-21.01"
        };

        public static TestCase Commision => new TestCase()
        {
            TransactionDate = new DateTime(2023, 01, 15),
            Amount = -10,
            Currency = "PLN",
            Description = "DESCRIPTION",
            RowTemplate = "\"2023-01-16\",\"{0}\"," +
            "\"Prowizja\"," +
            "\"{1}\"," +
            "\"{2}\"," +
            "\"100.25\"," +
            "\"Tytuł : {3}\"",

            ExpectedDescription = "Prowizja; DESCRIPTION"
        };

        public static TestCase PayByCard => new TestCase()
        {
            TransactionDate = new DateTime(2023, 01, 15),
            Amount = -15.20m,
            Currency = "PLN",
            TargetAccount = "01006109774169509063900636600311",
            TargetName = "TEST-COMPANY SP Z O O",
            RowTemplate = "\"2019-03-06\"," +
            "\"{0}\"," +
            "\"Płatność kartą\"," +
            "\"{1}\"," +
            "\"{2}\"," +
            "\"100.25\"," +
            "\"Tytuł : {3}\"," +
            "\"Lokalizacja : Adres : {4} Miasto : TESTOWO Kraj : TEST\"," +
            "\"Data i czas operacji : 2023-01-15\"," +
            "\"Kwota Cash Back : 0.00\"," +
            "\"Oryginalna kwota operacji : 15.20\"," +
            "\"Numer karty : 421234******9876\"",

            ExpectedDescription = string.Empty,
        };

        public static class CashDepositThroughBank
        {
            public static TestCase Case1 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = 1500m,
                Currency = "PLN",
                TargetAccount = string.Empty,
                TargetName = "JOHN DOE",
                Description = "DESCRIPTION",
                

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-16\"," +
                        "\"{0}\"," +
                        "\"Wpłata gotówkowa w kasie\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"100.25\"," +
                        "\"Nazwa nadawcy : {3}\"," +
                        "\"Adres nadawcy : UL.TESTOWA 1 10-222 TEST TEST\"," +
                        "\"Tytuł : {4}\"",
                        CashDepositThroughBank.Case1.TransactionDate,
                        CashDepositThroughBank.Case1.Amount,
                        CashDepositThroughBank.Case1.Currency,
                        CashDepositThroughBank.Case1.TargetName,
                        CashDepositThroughBank.Case1.Description);
                },
            };

            public static TestCase Case2 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = 5000m,
                Currency = "PLN",
                TargetAccount = "01234567890",
                TargetName = "JDG NAME SURNAME",
                Description = "DESCRIPTION",

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-16\"," +
                        "\"{0}\"," +
                        "\"Przelew na konto\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"100.25\"," +
                        "\"Rachunek nadawcy : {3}\"," +
                        "\"Nazwa nadawcy : {4}\"," +
                        "\"Adres nadawcy : UL.TESTOWA 1 10-222 TEST TEST\"," +
                        "\"Tytuł : {5}\"," +
                        "\"Referencje własne zleceniodawcy : 0987654321\"",
                        CashDepositThroughBank.Case2.TransactionDate,
                        CashDepositThroughBank.Case2.Amount,
                        CashDepositThroughBank.Case2.Currency,
                        CashDepositThroughBank.Case2.TargetAccount,
                        CashDepositThroughBank.Case2.TargetName,
                        CashDepositThroughBank.Case2.Description);
                }
            };

            public static TestCase Case3 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = -100m,
                Currency = "PLN",
                TargetAccount = "01234567890",
                TargetName = "NAME SURNAME",
                Description = "DESCRIPTION",

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-15\"," +
                        "\"{0}\"," +
                        "\"Transaction Type\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"100.25\"," +
                        "\"Rachunek odbiorcy : {3}\"," +
                        "\"Nazwa odbiorcy : {4}\"," +
                        "\"Tytuł : {5}OD: 48123456789 DO: 489*****321\"",
                        CashDepositThroughBank.Case3.TransactionDate,
                        CashDepositThroughBank.Case3.Amount,
                        CashDepositThroughBank.Case3.Currency,
                        CashDepositThroughBank.Case3.TargetAccount,
                        CashDepositThroughBank.Case3.TargetName,
                        CashDepositThroughBank.Case3.Description);
                }
            };

            public static TestCase Case4 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = 1000m,
                Currency = "PLN",
                TargetAccount = "01234567890",
                TargetName = "NAME SURNAME",
                Description = "DESCRIPTION",

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-15\"," +
                        "\"{0}\"," +
                        "\"Transaction Type\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"100.25\"," +
                        "\"Rachunek nadawcy : {3}\"," +
                        "\"Nazwa nadawcy : {4}\"," +
                        "\"Tytuł : {5}OD: 48123456789 DO: 489*****321\"",
                        CashDepositThroughBank.Case4.TransactionDate,
                        CashDepositThroughBank.Case4.Amount,
                        CashDepositThroughBank.Case4.Currency,
                        CashDepositThroughBank.Case4.TargetAccount,
                        CashDepositThroughBank.Case4.TargetName,
                        CashDepositThroughBank.Case4.Description);
                }
            };

            public static TestCase Case5 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = 2000m,
                Currency = "PLN",
                TargetAccount = "01234567890",
                TargetName = "NAME SURNAME",
                Description = "DESCRIPTION",

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-15\"," +
                        "\"{0}\"," +
                        "\"Transaction Type\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"0\"," +
                        "\"Rachunek nadawcy : {3}\"," +
                        "\"Nazwa nadawcy : {4}\"," +
                        "\"Adres nadawcy : UL.TESTOWA 1 10-222 TEST TEST\"," +
                        "\"Tytuł : {5}OD: 48123456789 DO: 489*****321\"",
                        CashDepositThroughBank.Case5.TransactionDate,
                        CashDepositThroughBank.Case5.Amount,
                        CashDepositThroughBank.Case5.Currency,
                        CashDepositThroughBank.Case5.TargetAccount,
                        CashDepositThroughBank.Case5.TargetName,
                        CashDepositThroughBank.Case5.Description);
                }
            };
        }

        public static class PayByWeb
        {
            public static TestCase Case1 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = -12.34m,
                Currency = "PLN",
                TargetName = "TEST COMPANY SP. Z O.O.",
                TargetAccount = string.Empty,
                Description = string.Empty,

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-16\"," +
                        "\"{0}\"," +
                        "\"Płatność web - kod mobilny\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"100.25\"," +
                        "\"Tytuł : 00000000000000000\"," +
                        "\"Numer telefonu : 48123456789\"," +
                        "\"Lokalizacja : Adres : {3}\"," +
                        "\"'Operacja : 00000000000000000\"," +
                        "\"Numer referencyjny : 00000000000000000\"",
                        PayByWeb.Case1.TransactionDate.ToString("yyyy-MM-dd"),
                        PayByWeb.Case1.Amount,
                        PayByWeb.Case1.Currency,
                        PayByWeb.Case1.TargetName);
                }
            };

            public static TestCase Case2 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = -12.34m,
                Currency = "PLN",

                TargetName = "TEST COMPANY SP. Z O.O.",
                TargetAccount = string.Empty,
                Description = string.Empty,

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-16\"," +
                        "\"{0}\"," +
                        "\"Płatność web - kod mobilny\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"100.25\"," +
                        "\"Numer telefonu : 48123456789\"," +
                        "\"Lokalizacja : Adres : {3}\"," +
                        "\"Data i czas operacji : 2023-01-15T12:00:00+02:00\"," +
                        "\"Numer referencyjny : 00000000000000000\"",
                        PayByWeb.Case2.TransactionDate.ToString("yyyy-MM-dd"),
                        PayByWeb.Case2.Amount,
                        PayByWeb.Case2.Currency,
                        PayByWeb.Case2.TargetName);
                }
            };

            public static TestCase Case3 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = -12.34m,
                Currency = "PLN",

                TargetName = string.Empty,
                TargetAccount = string.Empty,
                Description = "DESCRIPTION",

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-16\"," +
                        "\"{0}\"," +
                        "\"Płatność web - kod mobilny\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"100.25\"," +
                        "\"Nazwa odbiorcy : TEST\"," +
                        "\"Adres odbiorcy : UL.TESTOWA 1 10-222 TEST TEST\"," +
                        "\"Tytuł : {4}\"," +
                        "\"Referencje własne zleceniodawcy : 00000000000000000\"",
                        PayByWeb.Case3.TransactionDate.ToString("yyyy-MM-dd"),
                        PayByWeb.Case3.Amount,
                        PayByWeb.Case3.Currency,
                        PayByWeb.Case3.TargetName,
                        PayByWeb.Case3.Description);
                }
            };
        }

        public static class TransferFromAccount
        {
            public static TestCase Case1 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = -12.34m,
                Currency = "PLN",

                TargetName = "TEST-COMPANY SP. Z O. O.",
                TargetAccount = string.Empty,
                Description = "DESCRIPTION",

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-16\"," +
                        "\"{0}\"," +
                        "\"Przelew z rachunku\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"100.25\"," +
                        "\"Nazwa odbiorcy : {3}\"," +
                        "\"Adres odbiorcy : UL. TESTOWA 1 10-222 TEST\"," +
                        "\"Tytuł : {4}\"," +
                        "\"Referencje własne zleceniodawcy : 1234567890\"",
                        TransferFromAccount.Case1.TransactionDate.ToString("yyyy-MM-dd"),
                        TransferFromAccount.Case1.Amount,
                        TransferFromAccount.Case1.Currency,
                        TransferFromAccount.Case1.TargetName,
                        TransferFromAccount.Case1.Description);
                }
            };

            public static TestCase Case2 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = -12.34m,
                Currency = "PLN",

                TargetAccount = "990123456789",
                TargetName = "JOHN DOE",
                Description = "DESCRIPTION",

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-16\"," +
                        "\"{0}\"," +
                        "\"Przelew z rachunku\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"100.25\"," +
                        "\"Rachunek odbiorcy : {3}\"," +
                        "\"Nazwa odbiorcy : {4}\"," +
                        "\"Adres odbiorcy : UL. TESTOWA 1 10-222 TEST\"," +
                        "\"Tytuł : {5}\"," +
                        "\"Referencje własne zleceniodawcy : 01234567890\"",
                        TransferFromAccount.Case2.TransactionDate.ToString("yyyy-MM-dd"),
                        TransferFromAccount.Case2.Amount,
                        TransferFromAccount.Case2.Currency,
                        TransferFromAccount.Case2.TargetAccount,
                        TransferFromAccount.Case2.TargetName,
                        TransferFromAccount.Case2.Description);
                }
            };

            public static TestCase Case3 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = -12.34m,
                Currency = "PLN",

                TargetAccount = "990123456789",
                TargetName = "JOHN DOE",
                Description = "DESCRIPTION",

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-16\"," +
                        "\"{0}\"," +
                        "\"Przelew z rachunku\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"100.25\"," +
                        "\"Rachunek odbiorcy : {3}\"," +
                        "\"Nazwa odbiorcy : {4}\"," +
                        "\"Tytuł : {5}\"," +
                        "\"Referencje własne zleceniodawcy : 01234567890\"",
                        TransferFromAccount.Case3.TransactionDate.ToString("yyyy-MM-dd"),
                        TransferFromAccount.Case3.Amount,
                        TransferFromAccount.Case3.Currency,
                        TransferFromAccount.Case3.TargetAccount,
                        TransferFromAccount.Case3.TargetName,
                        TransferFromAccount.Case3.Description);
                }
            };

            public static TestCase Case4 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = -12.34m,
                Currency = "PLN",

                TargetAccount = "990123456789",
                TargetName = "JOHN DOE",
                Description = "DESCRIPTION",

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-16\"," +
                        "\"{0}\"," +
                        "\"Przelew z rachunku\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"100.25\"," +
                        "\"Rachunek odbiorcy : {3}\"," +
                        "\"Nazwa odbiorcy : {4}\"," +
                        "\"Tytuł : {5}\"",
                        TransferFromAccount.Case4.TransactionDate.ToString("yyyy-MM-dd"),
                        TransferFromAccount.Case4.Amount,
                        TransferFromAccount.Case4.Currency,
                        TransferFromAccount.Case4.TargetAccount,
                        TransferFromAccount.Case4.TargetName,
                        TransferFromAccount.Case4.Description);
                }
            };

            public static TestCase Case5 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = -12.34m,
                Currency = "PLN",

                TargetAccount = string.Empty,
                TargetName = "JOHN DOE",
                Description = "DESCRIPTION",

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-16\"," +
                        "\"{0}\"," +
                        "\"Przelew z rachunku\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"100.25\"," +
                        "\"Nazwa odbiorcy : {3}\"," +
                        "\"Tytuł : {4}\"",
                        TransferFromAccount.Case5.TransactionDate.ToString("yyyy-MM-dd"),
                        TransferFromAccount.Case5.Amount,
                        TransferFromAccount.Case5.Currency,
                        TransferFromAccount.Case5.TargetName,
                        TransferFromAccount.Case5.Description);
                }
            };

            public static TestCase Case6 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = 12.34m,
                Currency = "PLN",

                TargetAccount = "9901234567890",
                TargetName = "JOHN DOE",
                Description = "DESCRIPTION",

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-16\"," +
                        "\"{0}\"," +
                        "\"Przelew z rachunku\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"100.25\"," +
                        "\"Rachunek nadawcy : {3}\"," +
                        "\"Nazwa nadawcy : {4}\"," +
                        "\"Adres nadawcy : UL. TESTOWA 1 10-222 TEST\"," +
                        "\"Tytuł : {5}\"",
                        TransferFromAccount.Case6.TransactionDate.ToString("yyyy-MM-dd"),
                        TransferFromAccount.Case6.Amount,
                        TransferFromAccount.Case6.Currency,
                        TransferFromAccount.Case6.TargetAccount,
                        TransferFromAccount.Case6.TargetName,
                        TransferFromAccount.Case6.Description);
                }
            };

            public static TestCase Case7 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = 12.34m,
                Currency = "PLN",

                TargetAccount = string.Empty,
                TargetName = "JOHN DOE",
                Description = "DESCRIPTION",

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-16\"," +
                        "\"{0}\"," +
                        "\"Przelew z rachunku\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"100.25\"," +
                        "\"Nazwa nadawcy : {3}\"," +
                        "\"Adres nadawcy : UL. TESTOWA 1 10-222 TEST\"," +
                        "\"Tytuł : {4}\"",
                        TransferFromAccount.Case7.TransactionDate.ToString("yyyy-MM-dd"),
                        TransferFromAccount.Case7.Amount,
                        TransferFromAccount.Case7.Currency,
                        TransferFromAccount.Case7.TargetName,
                        TransferFromAccount.Case7.Description);
                }
            };

            public static TestCase Case8 => new TestCase()
            {
                TransactionDate = new DateTime(2023, 01, 15),
                Amount = 12.34m,
                Currency = "PLN",

                TargetAccount = "9901234567890",
                TargetName = string.Empty,
                Description = "DESCRIPTION",

                GetRow = () =>
                {
                    return string.Format(CultureInfo.InvariantCulture,
                        "\"2023-01-16\"," +
                        "\"{0}\"," +
                        "\"Przelew z rachunku\"," +
                        "\"{1}\"," +
                        "\"{2}\"," +
                        "\"100.25\"," +
                        "\"Rachunek nadawcy : {3}\"," +
                        "\"Adres nadawcy : UL. TESTOWA 1 10-222 TEST\"," +
                        "\"Tytuł : {4}\"",
                        TransferFromAccount.Case8.TransactionDate.ToString("yyyy-MM-dd"),
                        TransferFromAccount.Case8.Amount,
                        TransferFromAccount.Case8.Currency,
                        TransferFromAccount.Case8.TargetAccount,
                        TransferFromAccount.Case8.Description);
                }
            };
        }
    }
}
