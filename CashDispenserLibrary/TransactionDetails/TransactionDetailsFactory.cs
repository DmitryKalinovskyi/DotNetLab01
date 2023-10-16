namespace CashDispenserLibrary.TransactionDetails
{
    /// <summary>
    /// Factory to make TransactionDetais, frindly-ineritable
    /// </summary>

    public class TransactionDetailsFactory
    {
        private AutomatedTellerMachine _machine;

        public event TransactionDetails.TransactionMessage? OnTransactionComplete;
        public event TransactionDetails.TransactionMessage? OnTransactionFail;


        public TransactionDetailsFactory(AutomatedTellerMachine machine)
        {
            _machine = machine;
        }

        public virtual PaymentDetails CreatePaymentDetails(long from, long to, double amount)
        {
            PaymentDetails details = new(_machine.Bank, from, to, amount);

            details.OnTransactionComplete += OnTransactionComplete;
            details.OnTransactionFail += OnTransactionFail;

            return details;
        }

        public virtual WithdrawDetails CreateWithdrawDetails(long from, double amount)
        {
            WithdrawDetails details = new(_machine.Bank, from, amount);

            details.OnTransactionComplete += OnTransactionComplete;
            details.OnTransactionFail += OnTransactionFail;

            return details;
        }

        public virtual TopUpDetails CreateTopUpDetails(long to, double amount)
        {
            TopUpDetails details = new(_machine.Bank, to, amount);

            details.OnTransactionComplete += OnTransactionComplete;
            details.OnTransactionFail += OnTransactionFail;

            return details;
        }

    }
}
