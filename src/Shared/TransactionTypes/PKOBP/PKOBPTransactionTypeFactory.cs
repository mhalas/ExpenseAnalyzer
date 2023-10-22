namespace Shared.TransactionTypes.PKOBP
{
    public class PKOBPTransactionTypeFactory
    {
        public ITransactionType GetTransactionType(string transactionType)
        {
            switch (transactionType)
            {
                case "Naliczenie odsetek":
                case "Opłata":
                case "Opłata za użytkowanie karty":
                case "Prowizja":
                case "Wpłata gotówki we wpłatomacie":
                case "Wypłata gotówkowa z kasy":
                case "Podatek od odsetek":
                    return new BasicTransactionType();

                case "MOBILE_PAYMENT_C2C_EXTERNAL":
                case "MOBILE_PAYMENT_C2C":
                case "Przelew na konto":
                case "Przelew natychmiastowy":
                case "Przelew Natychmiastowy na konto":
                case "Przelew Paybynet":
                case "Przelew zagraniczny i walutowy":
                case "Zlecenie stałe":
                case "Wpłata gotówkowa w kasie":
                    return new BankTransferTransactionType();

                case "Przelew z rachunku":
                    return new TransferFromAccountTransactionType();

                case "Płatność kartą":
                    return new PayByCardTransactionType();

                case "Płatność web - kod mobilny":
                    return new PayByWebTransactionType();

                case "Wypłata z bankomatu":
                case "Zwrot płatności kartą":
                case "Wypłata w bankomacie - kod mobilny":

                    return new WithdrawTransactionType();

                case "Autooszczędzanie":
                case "Uznanie":
                case "Obciążenie":
                    return new IgnoredTransactionType();

                default:
                    return new OtherTransactionType();
            }
        }
    }
}
