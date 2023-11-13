using CashDispenserLibrary.Exceptions;
using CashDispenserLibrary.Core;

namespace CashDispenserWinApp.ViewModels
{
    public class WithdrawViewModel: TransactionViewModel
    {
        public double Amount { get; set; }

        protected override void _MakeTransaction()
        {
            var currentSession = Session.CurrentSession;

            if (currentSession == null) throw new EmptySessionException("Session was empty!");

            var details = currentSession.DetailsFactory.CreateWithdrawDetails(currentSession.Account.CardID, Amount);

            _DecorateTransaction(details);

            currentSession.Machine.ProcessTransaction(details);
        }
    }
}
