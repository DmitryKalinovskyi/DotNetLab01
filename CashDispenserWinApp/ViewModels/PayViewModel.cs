using CashDispenserLibrary.Exceptions;
using CashDispenserLibrary.Core;

namespace CashDispenserWinApp.ViewModels
{
    public class PayViewModel: TransactionViewModel
    {
        public long CardID { get; set; }
        public double Amount { get; set; }

        protected override void _MakeTransaction()
        {
            var currentSession = Session.CurrentSession;

            if (currentSession == null) throw new EmptySessionException("Session was empty!");

            var details = currentSession.DetailsFactory.CreatePaymentDetails(currentSession.Account.CardID, CardID, Amount);

            _DecorateTransaction(details);

            currentSession.Machine.ProcessTransaction(details);
        }
    }
}
