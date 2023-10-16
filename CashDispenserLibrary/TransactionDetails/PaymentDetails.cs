namespace CashDispenserLibrary.TransactionDetails
{
    public class PaymentDetails : TransactionDetails
    {
        public readonly long FromAccountID;
        public readonly long ToAccountID;

        public PaymentDetails(Bank bank, long fromAccountID, long toAccountID, double amount) : base(bank, amount)
        {
            FromAccountID = fromAccountID;
            ToAccountID = toAccountID;
        }
    }
}
