namespace CashDispenserLibrary.Core
{
    public class AuthenticateEventArgs: EventArgs
    {
        public Account Account { get; set; }

        public AuthenticateEventArgs(Account account)
        {
            Account = account;
        }   
    }
}
