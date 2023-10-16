using CashDispenserLibrary;
using CashDispenserWinApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

            MainWindow window = new MainWindow(sender);
            window.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var context = (LoginViewModel)DataContext;

            context.StartSession();
        }
    }
}
