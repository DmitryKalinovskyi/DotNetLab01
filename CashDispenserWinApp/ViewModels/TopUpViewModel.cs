using CashDispenserLibrary.Core;
using CashDispenserLibrary.Exceptions;

namespace CashDispenserWinApp.ViewModels
{
    /// <summary>
    /// Data context for top up view 
    /// </summary>
    public class TopUpViewModel: TransactionViewModel
    {
        public double Amount { get; set; } = 0;

        protected override void _MakeTransaction()
        {
            var currentSession = Session.CurrentSession;

            if (currentSession == null) throw new EmptySessionException("Session was empty!");

            var details = currentSession.DetailsFactory.CreateTopUpDetails(currentSession.Account.CardID, Amount);

            _DecorateTransaction(details);

            currentSession.Machine.ProcessTransaction(details);
        }
    }
}
