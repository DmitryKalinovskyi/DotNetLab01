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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CashDispenserWinApp.Views
{
    /// <summary>
    /// Interaction logic for PayView.xaml
    /// </summary>
    public partial class PayView : UserControl
    {
        private PayViewModel _viewModel;

        public PayView()
        {
            InitializeComponent();

            _viewModel = new();
            DataContext = _viewModel;

            _DecorateViewModel();
        }

        private void _DecorateViewModel()
        {
            _viewModel.OnTransactionCompleted += (s, args) => MessageBox.Show(args.Message, "Transaction information", MessageBoxButton.OK, MessageBoxImage.Information);
            _viewModel.OnTransactionFailed += (s, args) => MessageBox.Show(args.Message, "Transaction information", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.TryMakeTransaction();
        }
    }
}
