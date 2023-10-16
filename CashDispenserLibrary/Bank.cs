namespace CashDispenserLibrary
{
    /// <summary>
    /// Holds all basic information about bank and services related to it
    /// </summary>
    public class Bank
    {
        public string Name { get; set; }
        public TransactionManager TransactionManager { get; set; }
        public AccountManager AccountManager { get; set; }

        public Bank(string name)
        {
            Name = name;
            TransactionManager = new(this);
            AccountManager = new(this);
        }

        public Bank(string name, Dictionary<long, Account> accounts) : this(name)
        {
            AccountManager = new(this, accounts);
        }
    }
}
