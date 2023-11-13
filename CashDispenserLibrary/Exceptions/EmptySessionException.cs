namespace CashDispenserLibrary.Exceptions
{
    public class EmptySessionException: Exception
    {
        public EmptySessionException() { }

        public EmptySessionException(string message) :base(message){ }

        public EmptySessionException(string message, Exception inner) : base(message, inner) { }
    }
}
