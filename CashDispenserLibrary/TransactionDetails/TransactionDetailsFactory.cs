using CashDispenserLibrary.Core;

namespace CashDispenserLibrary.TransactionDetails
{
    /// <summary>
    /// Factory to make TransactionDetais, frindly-ineritable
    /// </summary>

    public class TransactionDetailsFactory
    {
        private AutomatedTellerMachine _machine;

        public event TransactionDetails.TransactionEventHandler? OnTransactionComplete;
        public event TransactionDetails.TransactionEventHandler? OnTransactionFail;


        public TransactionDetailsFactory(AutomatedTellerMachine machine)
        {
            _machine = machine;
        }

        public virtual PaymentDetails CreatePaymentDetails(long from, long to, double amount)
        {
            PaymentDetails details = new(_machine.Bank, from, to, amount);

            details.OnTransactionCompleted += OnTransactionComplete;
            details.OnTransactionFailed += OnTransactionFail;

            return details;
        }

        public virtual WithdrawDetails CreateWithdrawDetails(long from, double amount)
        {
            WithdrawDetails details = new(_machine.Bank, from, amount);

            details.OnTransactionCompleted += OnTransactionComplete;
            details.OnTransactionFailed += OnTransactionFail;

            return details;
        }

        public virtual TopUpDetails CreateTopUpDetails(long to, double amount)
        {
            TopUpDetails details = new(_machine.Bank, to, amount);

            details.OnTransactionCompleted += OnTransactionComplete;
            details.OnTransactionFailed += OnTransactionFail;

            return details;
        }

    }
}
