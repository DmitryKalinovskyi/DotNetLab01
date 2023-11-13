using CashDispenserLibrary.TransactionDetails;
using System;
using System.Windows;
using static CashDispenserLibrary.TransactionDetails.TransactionDetails;

namespace CashDispenserWinApp.ViewModels
{
    public abstract class TransactionViewModel: NotifyViewModel
    {
        public event TransactionEventHandler? OnTransactionCompleted;
        public event TransactionEventHandler? OnTransactionFailed;

        protected void _OnTransactionCompleted(TransactionDetails details,  TransactionEventArgs e)
        {
            OnTransactionCompleted?.Invoke(details, e);
        }

        protected void _OnTransactionFailed(TransactionDetails details, TransactionEventArgs e)
        {
            OnTransactionFailed?.Invoke(details, e);
        }

        protected abstract void _MakeTransaction();
        
        protected void _DecorateTransaction(TransactionDetails details)
        {
            details.OnTransactionCompleted += _OnTransactionCompleted;
            details.OnTransactionFailed += _OnTransactionFailed;
        }

        public void TryMakeTransaction()
        {
            try
            {
                _MakeTransaction();
            }
            catch(Exception ex)
            {
             //   MessageBox.Show($"Catched error: {ex.Message}");
            }
        }
    }
}
