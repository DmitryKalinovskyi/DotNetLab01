using CashDispenserLibrary.Core;
using CashDispenserWinApp.ViewModels;
using CashDispenserWinApp.Views;
using System;
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
                new(1234_1234_1234_1234, 1111, "Dmytro", "Kalinovskyi", 100));
            bank.AccountManager.AddAccount(
                new(1111_2222_3333_4444, 1111, "Dmytro", "Moroz", 8888));
            bank.AccountManager.AddAccount(
                new(1111_1111_1111_1111, 1111, "Andriy", "Morozov", 99999));

            AutomatedTellerMachine machine = new(0, "Ukraine Zhytomyr st. Chudnivska 97", bank);

            return machine;
        }

        public static AutomatedTellerMachine WorkingMachine { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Current.DispatcherUnhandledException += (sender, args) =>
            {
                // Handle the exception (log, display user-friendly message, etc.)
                Exception exception = args.Exception;
                MessageBox.Show($"An unhandled exception occurred: {exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // Optionally, log the exception for debugging purposes
            };


            WorkingMachine = GetMachine();

            Session.SessionStart += StartInteraction;
            Session.SessionStart += EndLoginSession;

            Session.SessionEnd += StartLoginSession;
            Session.SessionEnd += EndInteraction;

            StartLoginSession();

        }

        private LoginView? _loginView;
        private MainWindow? _mainWindowView;

        public void StartLoginSession()
        {
            _loginView = new LoginView(WorkingMachine);
            _loginView.Show();
        }

        public void EndLoginSession()
        {
            _loginView?.Close();
            _loginView = null;
        }

        public void StartInteraction()
        {
            _mainWindowView = new MainWindow((LoginViewModel)_loginView.DataContext);
            _mainWindowView.Show();
        }

        public void EndInteraction()
        {
            _mainWindowView?.Close();
            _mainWindowView = null;
        }
    }
}
