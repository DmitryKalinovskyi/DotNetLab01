using CashDispenserLibrary.Core;

namespace CashDispenserLibrary.TransactionDetails
{
    public class WithdrawDetails: TransactionDetails
    {
        public readonly long FromAccountID;

        public WithdrawDetails(Bank bank, long fromAccountID, double amount): base(bank, amount)
        {
            FromAccountID = fromAccountID;
        }
    }
}
