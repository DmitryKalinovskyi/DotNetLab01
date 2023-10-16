using CashDispenserLibrary;
using CashDispenserWinApp.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;

namespace CashDispenserWinApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private AutomatedTellerMachine GetMachine()
        {
            Bank bank = new("Privat24");

            bank.AccountManager.AddAccount(
                new(1234_1234_1234_1234, 1111, "Dmitry", "Kalinovskyi", 100));
            bank.AccountManager.AddAccount(
                new(1111_2222_3333_4444, 1111, "Dmitry", "Moroz", 8888));
            bank.AccountManager.AddAccount(
                new(1111_1111_1111_1111, 1111, "Andriy", "Morozov", 99999));

            AutomatedTellerMachine machine = new(0, "Ukraine Zhytomyr st. Chudnivska 97", bank);

            return machine;
        }

        private void BeginLoginSession()
        {
            LoginView loginView = new LoginView(GetMachine());
            loginView.Show();
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            BeginLoginSession();
        }
    }
}
