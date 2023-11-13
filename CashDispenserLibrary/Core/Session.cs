using CashDispenserLibrary.Exceptions;
using CashDispenserLibrary.TransactionDetails;

namespace CashDispenserLibrary.Core
{
    public class Session
    {
        public static Session? CurrentSession { get; private set; }

        public delegate void SessionUpdate();

        public static event SessionUpdate? SessionStart;
        public static event SessionUpdate? SessionEnd;

        public Account Account { get; private set; }
        public AutomatedTellerMachine Machine { get; private set; }
        public TransactionDetailsFactory DetailsFactory { get; private set;}

        public Session(AutomatedTellerMachine workingMachine, Account account): this(account, workingMachine, new(workingMachine)) { }

        public Session(Account account, AutomatedTellerMachine workingMachine, TransactionDetailsFactory detailsFactory)
        {
            if (CurrentSession != null) throw new MultipleSessionException("You can't begin new sesion, please end previous!");

            Account = account;
            Machine = workingMachine;
            DetailsFactory = detailsFactory;

            CurrentSession = this;
        }

        public void StartSession()
        {
            SessionStart?.Invoke();
        }

        public void EndSession()
        {
            SessionEnd?.Invoke();
            CurrentSession = null;
        }
    }
}
