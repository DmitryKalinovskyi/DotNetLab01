using CashDispenserLibrary.Core;
using CashDispenserWinApp.Models;
using System;

namespace CashDispenserWinApp.ViewModels
{
    public class LoginViewModel: NotifyViewModel
    {
        public LoginModel Model { get; private set; }

        public AutomatedTellerMachine Machine { get; private set; } 

        public LoginViewModel(AutomatedTellerMachine machine)
        {
            Model = new LoginModel();
            Machine = machine;
        }

        public string CardID
        {
            get
            {
                return Model.CardID == 0?"":Model.CardID.ToString();
            }
            set
            {
                Model.CardID = long.Parse(value);
                OnPropertyChanged(nameof(CardID));
            }
        }

        public string PIN
        {
            get
            {
                return Model.PIN == 0?"":Model.PIN.ToString();
            }
            set
            {
                Model.PIN = int.Parse(value);
                OnPropertyChanged(nameof(PIN));
            }
        }

        public delegate void LoginEventHandler(LoginViewModel sender, string message);

        public event LoginEventHandler? LoginCompleted; 
        public event LoginEventHandler? LoginFailed;

        public void StartSession()
        {
            Session session;
            try
            {
                session = Machine.TryLogin(Model.CardID, Model.PIN);
            }
            catch (Exception ex)
            {
                LoginFailed?.Invoke(this, ex.Message);
                return;
            }

            LoginCompleted?.Invoke(this, "Login completed!");
            session.StartSession();
        }
    }
}
