using CashDispenserLibrary.Exceptions;

namespace CashDispenserLibrary.Core
{
    public class Account
    {
        public long CardID { get; set; }

        public string OwnerName { get; set; }

        public string OwnerSurname { get; set; }

        public double Balance { get; private set; }

        private int _pin { get; set; }

        public event EventHandler<EventArgs>? OnTopUp;
        public event EventHandler<EventArgs>? OnWithdraw;
        public event EventHandler<EventArgs>? OnInfoRequested;

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

            OnWithdraw?.Invoke(this, new());
            Balance -= amount;
        }

        internal void TopUp(double amount)
        {
            OnTopUp?.Invoke(this, new());
            Balance += amount;
        }

        public string GetInfo()
        {
            OnInfoRequested?.Invoke(this, new());
            return @$"Account owner: {OwnerName} {OwnerSurname}
Balance: {Balance}";
        }
    }
}