namespace CashDispenserLibrary.Exceptions
{
    public class AccountWrongPINException : Exception
    {
        public AccountWrongPINException() { }

        public AccountWrongPINException(string message): base(message) { }

        public AccountWrongPINException(string message, Exception inner) : base(message, inner) { }

    }
}
