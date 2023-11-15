using CashDispenserConsole.Exceptions;

namespace CashDispenserConsole
{
    public class CommandManager
    {
        private Dictionary<string, Command> _avaibleCommands;

        public CommandManager()
        {
            _avaibleCommands = new();
        }

        public void Execute(string commandName, string[] args)
        {
            if (_avaibleCommands.ContainsKey(commandName) == false) throw new CommandNotFoundException("This command don't exist in current context");

            _avaibleCommands[commandName].Execute(args);
        }

        public void AddCommand(string commandName, string commandInfo, Action<string[]> action)
        {
            _avaibleCommands.Add(commandName, new(commandInfo, action));
        }

        public string GetCommandsInfo()
        {
            return "Command list: \n" + string.Join("\n\n---------------------\n", _avaibleCommands.Values.Select(cmd => cmd.CommandInfo));
        }
    }
}
