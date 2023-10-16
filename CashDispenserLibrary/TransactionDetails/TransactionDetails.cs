namespace CashDispenserLibrary.TransactionDetails
{
    /// <summary>
    /// Abstract class of TransactionDetails. Each transaction will have amount of money procceded.
    /// </summary>
    public abstract class TransactionDetails
    {
        public delegate void TransactionMessage(TransactionDetails details, string? message);

        public event TransactionMessage? OnTransactionComplete;
        public event TransactionMessage? OnTransactionFail;

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
            OnTransactionComplete?.Invoke(this, message);
        }

        internal void CancelTransaction(string? message)
        {
            IsFailed = false;
            OnTransactionFail?.Invoke(this, message);
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
