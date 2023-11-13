using CashDispenserLibrary.Exceptions;
using CashDispenserLibrary.TransactionDetails;
using System.Transactions;

namespace CashDispenserLibrary.Core
{
    /// <summary>
    /// Makes common operation, top up, withdraw, payment, wraps interaction with bank
    /// </summary>
    public class AutomatedTellerMachine
    {
        public readonly int MachineID;
        public readonly Bank Bank;

        public string Address { get; set; }

        public double Balance { get; private set; }

        public AutomatedTellerMachine(int machineID, string adress, Bank bank) : this(machineID, adress, bank, 0) { }

        public AutomatedTellerMachine(int machineID, string adress, Bank bank, double balance)
        {
            MachineID = machineID;
            Address = adress;
            Balance = balance;
            Bank = bank;
        }

        public Session TryLogin(long cardID, int pin)
        {
            Account account = Bank.AccountManager.RetriveAccount(cardID, pin);

            return new(this, account);
        }

        public void ProcessTransaction(WithdrawDetails details)
        {
            if (details.Amount > Balance)
            {
                details.CancelTransaction("Terminal don't have enough balance!");
                throw new InsufficientBalanceException("Terminal don't have enought balance!");
            }

            details.OnTransactionCompleted += (d, message) =>
            {
                // Output money
                Balance -= details.Amount;
            };
                
            Bank.TransactionManager.ProcessTransaction(details);
        }

        public void ProcessTransaction(TopUpDetails details)
        {
            details.OnTransactionCompleted += (d, message) =>
            {
                Balance += details.Amount;
            };

            Bank.TransactionManager.ProcessTransaction(details);
        }

        public void ProcessTransaction(PaymentDetails details)
        {
            Bank.TransactionManager.ProcessTransaction(details);
        }
    }
}
