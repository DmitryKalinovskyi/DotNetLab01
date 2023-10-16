namespace CashDispenserConsole
{
    internal class Command
    {
        private Action<string[]> _action;
        public readonly string CommandInfo;
        
        public Command(string info, Action<string[]> action)
        {
            _action = action;
            CommandInfo = info;
        }

        public void Execute(string[] args)
        {
            _action.Invoke(args);
        }
    }
}
