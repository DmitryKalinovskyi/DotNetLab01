namespace CashDispenserLibrary.Exceptions
{
    public class MultipleSessionException: Exception
    {
        public MultipleSessionException() { }

        public MultipleSessionException(string message): base(message) { }

        public MultipleSessionException(string message, Exception inner) : base(message, inner) { }
    }
}
