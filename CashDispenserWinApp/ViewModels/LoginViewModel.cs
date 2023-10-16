using CashDispenserLibrary;
using CashDispenserWinApp.Models;
using System;

namespace CashDispenserWinApp.ViewModels
{
    public class LoginViewModel: NotifyModelView
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

        public delegate void LoginEvent(LoginViewModel sender, string message);

        public event LoginEvent? LoginCompleted; 
        public event LoginEvent? LoginFailed;

        public Session? Session { get; private set; }

        public void StartSession()
        {
            try
            {
                Session = Machine.TryLogin(Model.CardID, Model.PIN);  
            }
            catch(Exception ex)
            {
                LoginFailed?.Invoke(this, ex.Message);
                return;
            }

            LoginCompleted?.Invoke(this, "Login completed!");
        }
    }
}
