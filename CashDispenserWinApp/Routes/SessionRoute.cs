using CashDispenserLibrary.Core;

namespace CashDispenserWinApp.Routes
{
    public class SessionRoute
    {
        public static Session CurrentSession
        {
            get
            {
                return Session.CurrentSession;
            }
        }
    }
}
