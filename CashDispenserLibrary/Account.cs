using CashDispenserLibrary.Exceptions;

namespace CashDispenserLibrary
{
    public class Account
    {
        public long CardID { get; set; }

        public string OwnerName { get; set; }

        public string OwnerSurname { get; set; }

        public double Balance { get; private set; }

        private int _pin { get; set; }

        public Account(long cardID, int pin, string ownerName, string ownerSurname, double balance = 0)
        {
            CardID = cardID;
            OwnerName = ownerName;
            OwnerSurname = ownerSurname;
            Balance = balance;

            _pin = pin;
        }
      
        internal bool ComparePIN(int pin)
        {
            return pin == _pin;
        }

        internal void Withdraw(double amount)
        {
            if(amount > Balance)
            {
                throw new InsufficientBalanceException("Don't enough balance in the account!");
            }
            Balance -= amount;
        }

        internal void TopUp(double amount)
        {
            Balance += amount;
        }

        public string GetInfo()
        {
            return @$"Account owner: {OwnerName} {OwnerSurname}
Balance: {Balance}";
        }
    }
}