using CashDispenserLibrary.Core;
using CashDispenserWinApp.ViewModels;

using System.Windows;


namespace CashDispenserWinApp.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView(AutomatedTellerMachine machine): this(new LoginViewModel(machine)) { }

        public LoginView(LoginViewModel model)
        {
            InitializeComponent();
            DataContext = model;

            model.LoginCompleted += OnLoginCompleted;
            model.LoginFailed += OnLoginFail;
        }

        private void OnLoginFail(LoginViewModel sender, string message)
        {
            MessageBox.Show(message);
        }

        private void OnLoginCompleted(LoginViewModel sender, string message)
        {
            MessageBox.Show(message);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var context = (LoginViewModel)DataContext;

            context.StartSession();
        }
    }
}
