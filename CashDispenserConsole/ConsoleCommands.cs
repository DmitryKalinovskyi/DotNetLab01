using CashDispenserLibrary.Core;
using CashDispenserLibrary.Exceptions;
using CashDispenserLibrary.Utils;

namespace CashDispenserConsole
{
    public static class ConsoleCommands
    {
        public static void Pay(string[] args)
        {
            if (args.Length < 2) throw new ArgumentException("Too few arguments has been passed");

            if (Session.CurrentSession == null) throw new EmptySessionException("Session was empty!");

            long from = Session.CurrentSession.Account.CardID;
            long to = CardConverter.ParseCard(args[0]);
            double amount = double.Parse(args[1]);

            Session.CurrentSession.Machine.ProcessTransaction(Session.CurrentSession.DetailsFactory.CreatePaymentDetails(from, to, amount));
        }

        public static void TopUp(string[] args)
        {
            if (args.Length < 1) throw new ArgumentException("Too few arguments has been passed");
            if (Session.CurrentSession == null) throw new EmptySessionException("Session was empty!");

            long to = Session.CurrentSession.Account.CardID;
            double amount = double.Parse(args[0]);

            Session.CurrentSession.Machine.ProcessTransaction(Session.CurrentSession.DetailsFactory.CreateTopUpDetails(to, amount));
        }

        public static void Withdraw(string[] args)
        {
            if (args.Length < 1) throw new ArgumentException("Too few arguments has been passed");
            if (Session.CurrentSession == null) throw new EmptySessionException("Session was empty!");

            long from = Session.CurrentSession.Account.CardID;
            double amount = double.Parse(args[0]);

            Session.CurrentSession.Machine.ProcessTransaction(Session.CurrentSession.DetailsFactory.CreateWithdrawDetails(from, amount));
        }

        public static void Exit(string[] args)
        {
            if (Session.CurrentSession == null) throw new EmptySessionException("Session was empty!");

            Session.CurrentSession.EndSession();
        }

        public static void ShowInfo(string[] args)
        {
            if (Session.CurrentSession == null) throw new EmptySessionException("Session was empty!");

            Console.WriteLine(Session.CurrentSession.Account.GetInfo());
        }
    }
}
