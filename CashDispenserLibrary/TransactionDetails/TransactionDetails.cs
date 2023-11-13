using CashDispenserLibrary.Core;

namespace CashDispenserLibrary.TransactionDetails
{
    /// <summary>
    /// Abstract class of TransactionDetails. Each transaction will have amount of money procceded.
    /// </summary>
    public abstract class TransactionDetails: EventArgs
    {
        public delegate void TransactionEventHandler(TransactionDetails details, TransactionEventArgs args);

        public event TransactionEventHandler? OnTransactionCompleted;
        public event TransactionEventHandler? OnTransactionFailed;

        public readonly double Amount;
        public readonly Bank Bank;
        public readonly DateTime Created;

        public bool IsCompleted { get; private set; }
        public bool IsFailed { get; private set; }

        public TransactionDetails(Bank bank, double amount)
        {
            Amount = amount;
            Bank = bank;
            Created = DateTime.Now;
            IsCompleted = false;
        }

        internal void CompleteTransaction(string? message)
        {
            IsCompleted = true;
            OnTransactionCompleted?.Invoke(this, new(message));
        }

        internal void CancelTransaction(string? message)
        {
            IsFailed = false;
            OnTransactionFailed?.Invoke(this, new(message));
        }

        public virtual string GetTransactionInfo()
        {
            return @$"{Created}
__________________________
{Bank.Name}
Transaction completed in 
total amount of {Amount:0.00}


Thanks for using us!
";
        }
    }       
}
