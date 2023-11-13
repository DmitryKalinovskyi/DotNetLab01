using CashDispenserConsole;
using CashDispenserConsole.Exceptions;
using CashDispenserLibrary;
using CashDispenserLibrary.Exceptions;

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
    new(1234_1234_1234_1234, 1111, "Dmitry", "Kalinovskyi", 100));
bank.AccountManager.AddAccount(
    new(1111_2222_3333_4444, 1111, "Dmitry", "Moroz", 8888));
bank.AccountManager.AddAccount(
    new(1111_1111_1111_1111, 1111, "Andriy", "Morozov", 99999));

AutomatedTellerMachine machine = new(0, "Ukraine Zhytomyr st. Chudnivska 97", bank);

long ParseCard(string card)
{
    return long.Parse(card.Replace("_", ""));
}

Session RequestSession()
{
    while (true)
    {
        try
        {
            Console.Write("Enter card id: ");
            long cardID = ParseCard(Console.ReadLine());

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
Example of usage: pay 1111_1111_1111_1111 100",

    (string[] args) =>
    {
        if (args.Length < 2) throw new ArgumentException("Too few arguments has been passed");
        // parse inputs
        long from = session.Account.CardID;
        long to = ParseCard(args[0]);
        double amount = double.Parse(args[1]);

        session.Machine.ProcessTransaction(session.DetailsFactory.CreatePaymentDetails(from, to, amount));
    });

    commandManager.AddCommand("topup",
        @"topup @amount - Command to top up inserted card with @amount of money
Example of usage: topup 100",

        (string[] args) =>
    {
        if (args.Length < 1) throw new ArgumentException("Too few arguments has been passed");

        // parse inputs
        long to = session.Account.CardID;
        double amount = double.Parse(args[0]);

        session.Machine.ProcessTransaction(session.DetailsFactory.CreateTopUpDetails(to, amount));
    });

    commandManager.AddCommand("withdraw",
        @"withdraw @amount - Command to withdraw @amount of money from inserted card
Example of usage: withdraw 100",
        (string[] args) =>
    {
        if (args.Length < 1) throw new ArgumentException("Too few arguments has been passed");

        // parse inputs
        long from = session.Account.CardID;
        double amount = double.Parse(args[0]);

        session.Machine.ProcessTransaction(session.DetailsFactory.CreateWithdrawDetails(from, amount));
    });

    commandManager.AddCommand("exit",
        @"exit - Command to exit from logged account",

        (string[] args) =>
        {
            session.EndSession();
        });

    commandManager.AddCommand("help",
        @"help - Command to display information about commands",

        (string[] args) =>
        {
            Console.WriteLine(commandManager.GetCommandsInfo());
        });

    commandManager.AddCommand("showinfo",
        @"showinfo - Command to display information about account",
        (string[] args) =>
        {
            Console.WriteLine(session.Account.GetInfo());
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
    session.SessionEnd += () =>
    {
        ended = true;
    };

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
}


while (true)
{
    HandleSession();
}






