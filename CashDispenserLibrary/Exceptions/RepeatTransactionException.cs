namespace CashDispenserLibrary.Exceptions
{
    public class RepeatTransactionException: Exception
    {
        public RepeatTransactionException() { }

        public RepeatTransactionException(string message): base(message) { }

        public RepeatTransactionException(string message, Exception inner) : base(message, inner) { }

    }
}
