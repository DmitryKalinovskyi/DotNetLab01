namespace CashDispenserConsole.Exceptions
{
    public class CommandNotFoundException: Exception
    {
        public CommandNotFoundException() { }

        public CommandNotFoundException(string message) : base(message) { }
    }
}
