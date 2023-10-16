using CashDispenserLibrary.Exceptions;
using CashDispenserLibrary.TransactionDetails;

namespace CashDispenserLibrary
{
    public class TransactionManager
    {
        private Bank _relatedBank;

        internal TransactionManager(Bank relatedBank)
        {
            _relatedBank = relatedBank;
        }

        internal void ProcessTransaction(PaymentDetails paymentDetails)
        {
            if (paymentDetails.IsCompleted) throw new RepeatTransactionException("This transaction already processed!");

            try
            {
                Account from = _relatedBank.AccountManager.GetAccount(paymentDetails.FromAccountID),
                        to = _relatedBank.AccountManager.GetAccount(paymentDetails.ToAccountID);

                if (from == to)
                    throw new SelfPaymentException("Payment source and destination should be different");

                from.Withdraw(paymentDetails.Amount);
                to.TopUp(paymentDetails.Amount);
            }
            catch(Exception ex)
            {
                paymentDetails.CancelTransaction(ex.Message);
                throw;
            }

            paymentDetails.CompleteTransaction("Payment transaction completed!");
        }

        internal void ProcessTransaction(TopUpDetails topUpDetails)
        {
            if (topUpDetails.IsCompleted) throw new RepeatTransactionException("This transaction already processed!");
            try
            {
                Account target = _relatedBank.AccountManager.GetAccount(topUpDetails.ToAccountID);

                target.TopUp(topUpDetails.Amount);
            }
            catch (Exception ex)
            {
                topUpDetails.CancelTransaction(ex.Message);
                throw;
            }
            topUpDetails.CompleteTransaction("Top up transaction completed!");
        }

        internal void ProcessTransaction(WithdrawDetails withdrawDetails)
        {
            if (withdrawDetails.IsCompleted) throw new RepeatTransactionException("This transaction already processed!");

            try
            {
                Account target = _relatedBank.AccountManager.GetAccount(withdrawDetails.FromAccountID);

                target.Withdraw(withdrawDetails.Amount);
            }
            catch (Exception ex)
            {
                withdrawDetails.CancelTransaction(ex.Message);
                throw;
            }
         
            withdrawDetails.CompleteTransaction("Withdraw transaction completed!");
        }
    }
}
