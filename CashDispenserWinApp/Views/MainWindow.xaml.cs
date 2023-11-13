using CashDispenserWinApp.ViewModels;
using CashDispenserWinApp.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CashDispenserWinApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel(loginViewModel);
            DataContext = _viewModel;

            // initialize view page
            _viewModel.SelectedView = new HomeView();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedView = new HomeView();
        }

        private void Withdraw_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedView = new WithdrawView();
        }

        private void TopUpButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedView = new TopupView(_viewModel);
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedView = new PayView();
        }
    }
}
