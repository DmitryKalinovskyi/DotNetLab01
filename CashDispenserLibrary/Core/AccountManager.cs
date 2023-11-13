using CashDispenserLibrary.Exceptions;

namespace CashDispenserLibrary.Core
{
    public class AccountManager
    {
        private Dictionary<long, Account> _accounts;

        private readonly Bank _relatedBank;

        public AccountManager(Bank bank)
        {
            _relatedBank = bank;
            _accounts = new();
        }

        public AccountManager(Bank bank, Dictionary<long, Account> accounts)
        {
            _relatedBank = bank;
            _accounts = accounts;
        }

        public void AddAccount(Account account)
        {
            _accounts.Add(account.CardID, account);
        }

        internal Account RetriveAccount(long accountID, int pin)
        {
            if (_accounts.ContainsKey(accountID) == false) 
                throw new AccountNotFoundException("Wrong account id!");

            Account account = _accounts[accountID];

            if (account.ComparePIN(pin) == false)
                throw new AccountWrongPINException("Wrong pincode!");

            return account;
        }

        internal Account GetAccount(long accountID)
        {
            if (_accounts.ContainsKey(accountID) == false)
                throw new AccountNotFoundException("Wrong account id!");

            return _accounts[accountID];
        }
    }
}
