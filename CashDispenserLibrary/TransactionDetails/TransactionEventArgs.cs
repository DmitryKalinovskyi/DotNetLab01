namespace CashDispenserLibrary.TransactionDetails
{
    public class TransactionEventArgs: EventArgs
    {
        public string? Message;
        public TransactionEventArgs(string? message)
        {
            Message = message;
        }
    }
}
