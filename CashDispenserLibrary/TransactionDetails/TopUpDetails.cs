using CashDispenserLibrary.Core;

namespace CashDispenserLibrary.TransactionDetails
{
    public class TopUpDetails: TransactionDetails
    {
        public readonly long ToAccountID; 

        public TopUpDetails(Bank bank, long toAccountID, double amount): base(bank, amount)
        {
            ToAccountID = toAccountID;
        }
    }
}
