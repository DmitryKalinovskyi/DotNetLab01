using CashDispenserConsole;

using CashDispenserConsole.Exceptions;
using CashDispenserLibrary.Core;
using CashDispenserLibrary.Exceptions;
using CashDispenserLibrary.Utils;
using static CashDispenserLibrary.Core.Session;

Console.WriteLine("Cash dispenser terminal by Kalinovskyi Dmitry");

/*
 * Proccess of working in the cash dispenser machine
 * 
 * Beginning of the session, initialize session using actual machine class instance.
 * Session manages user autentification.
 * 
 * After user successfully logined, session invokes OnLogin event 
 * There are some user interaction, if needed teller machine can require reauntification
 * 
 * when interaction ended, will be caused SessionEnd event.
 *  
 */

Bank bank = new("Privat24");

bank.AccountManager.AddAccount(
    new(1234_1234_1234_1234, 1111, "Dmytro", "Kalinovskyi", 100));
bank.AccountManager.AddAccount(
    new(1111_2222_3333_4444, 1111, "Dmytro", "Moroz", 8888));
bank.AccountManager.AddAccount(
    new(1111_1111_1111_1111, 1111, "Andriy", "Morozov", 99999));

AutomatedTellerMachine machine = new(0, "Ukraine Zhytomyr st. Chudnivska 97", bank);

Session RequestSession()
{
    while (true)
    {
        try
        {
            Console.Write("Enter card id: ");
            long cardID = CardConverter.ParseCard(Console.ReadLine());

            Console.Write("Enter pin code: ");
            int pin = int.Parse(Console.ReadLine());

            return machine.TryLogin(cardID, pin);
        }
        catch (AccountNotFoundException e)
        {
            Console.WriteLine("Given card id not founded!");
        }
        catch (AccountWrongPINException e)
        {
            Console.WriteLine("Entered wrong pin code!");
        }
        catch(Exception e)
        {
            Console.WriteLine("You entered wrong data, try again!");
        }
    }
}

CommandManager InitializeCommander(Session session)
{
    CommandManager commandManager = new();

    // initialize command manager with own functions

    commandManager.AddCommand("pay",
        @"pay @card-id @amount - Command to pay person with @card-id @amount of money
Example of usage: pay 1111_1111_1111_1111 100", ConsoleCommands.Pay);

    commandManager.AddCommand("topup",
        @"topup @amount - Command to top up inserted card with @amount of money
Example of usage: topup 100", ConsoleCommands.TopUp);

    commandManager.AddCommand("withdraw",
        @"withdraw @amount - Command to withdraw @amount of money from inserted card
Example of usage: withdraw 100", ConsoleCommands.Withdraw);

    commandManager.AddCommand("exit",
        @"exit - Command to exit from logged account", ConsoleCommands.Exit);

    commandManager.AddCommand("showinfo",
        @"showinfo - Command to display information about account", ConsoleCommands.ShowInfo);

    commandManager.AddCommand("help",
       @"help - Command to display information about commands",

       (string[] args) =>
       {
           Console.WriteLine(commandManager.GetCommandsInfo());
       });

    return commandManager;
}

void HandleSession()
{

    // get user login  
    Session session = RequestSession();

    // initialize session messages
    session.DetailsFactory.OnTransactionComplete += (details, message) =>
    {
        Console.WriteLine(details.GetTransactionInfo());
    };

    // initialize command manager from current session
    CommandManager commandManager = InitializeCommander(session);

    Console.WriteLine("Login completed! Write help to see the comand list");

    bool ended = false;
    SessionUpdate onEnd = () =>
    {
        ended = true;
    };
    Session.SessionEnd += onEnd;

    while (!ended)
    {
        Console.Write("\nEnter command: ");
        string[] strings = Console.ReadLine().Split(" ");

        try
        {
            commandManager.Execute(strings[0], strings[1..]);
        }
        catch(CommandNotFoundException e)
        {
            Console.WriteLine("During proceding comand happened error: " + e.Message);
        }
        catch(Exception e)
        {
            Console.WriteLine("During proceding comand happened error: " + e.Message);
        }
    }

    Session.SessionEnd -= onEnd;
}


while (true)
{
    HandleSession();
}






