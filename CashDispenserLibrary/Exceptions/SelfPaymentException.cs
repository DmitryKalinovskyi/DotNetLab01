namespace CashDispenserLibrary.Exceptions
{
    public class SelfPaymentException: Exception
    {
        public SelfPaymentException() { }

        public SelfPaymentException(string message) : base(message) { }

        public SelfPaymentException(string message, Exception inner) : base(message, inner) { }

    }
}
