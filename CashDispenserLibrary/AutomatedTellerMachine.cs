using CashDispenserLibrary.Exceptions;
using CashDispenserLibrary.TransactionDetails;

namespace CashDispenserLibrary
{
    /// <summary>
    /// Makes common operation, top up, withdraw, payment
    /// </summary>
    public class AutomatedTellerMachine
    {
        public readonly int MachineID;
        public readonly Bank Bank;

        public string Address { get; set; }

        private double _balance;

        public AutomatedTellerMachine(int machineID, string adress, Bank bank)
        {
            MachineID = machineID;
            Address = adress;
            _balance = 0;
            Bank = bank;
        }

        public Session TryLogin(long cardID, int pin)
        {
            Account account = Bank.AccountManager.RetriveAccount(cardID, pin);

            return new(this, account);
        }

        public void ProcessTransaction(WithdrawDetails details)
        {
            details.OnTransactionComplete += (d, message) =>
            {
                // Output money
                _balance -= details.Amount;
            };

            try
            {
                if(details.Amount > _balance)
                {
                    throw new InsufficientBalanceException("Terminal don't have enought balance!");
                }
                
                Bank.TransactionManager.ProcessTransaction(details);
            }
            catch(Exception e){
                Console.WriteLine($"During transaction processing happened error: {e.Message}");
            }
        }

        public void ProcessTransaction(TopUpDetails details)
        {
            details.OnTransactionComplete += (d, message) =>
            {
                _balance += details.Amount;
            };

            try
            {
                Bank.TransactionManager.ProcessTransaction(details);
            }
            catch (Exception e)
            {
                Console.WriteLine($"During transaction processing happened error: {e.Message}");
            }
        }

        public void ProcessTransaction(PaymentDetails details)
        {
            try
            {
                Bank.TransactionManager.ProcessTransaction(details);
            }
            catch (Exception e)
            {
                Console.WriteLine($"During transaction processing happened error: {e.Message}");
            }
        }
    }
}
