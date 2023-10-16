using CashDispenserLibrary.TransactionDetails;

namespace CashDispenserLibrary
{
    public class Session
    {
        public delegate void SessionUpdate();

        public event SessionUpdate? SessionStart;
        public event SessionUpdate? SessionEnd;

        public Account Account { get; private set; }
        public AutomatedTellerMachine Machine { get; private set; }
        public TransactionDetailsFactory DetailsFactory { get; private set;}

        public Session(AutomatedTellerMachine workingMachine, Account account)
        {
            Machine = workingMachine;
            Account = account;
            DetailsFactory = new(workingMachine);
        }

        public void StartSession()
        {
            SessionStart?.Invoke();
        }

        public void EndSession()
        {
            SessionEnd?.Invoke();    
        }
    }
}
