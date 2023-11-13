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
    /// Interaction logic for TopupView.xaml
    /// </summary>
    public partial class TopupView : UserControl
    {
        private TopUpViewModel _viewModel;
        private MainWindowViewModel _mainWindowsViewModel;

        public TopupView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _mainWindowsViewModel = mainWindowViewModel;
            _viewModel = new();

            DataContext = _viewModel;

            _DecorateViewModel();
        }

        private void _DecorateViewModel()
        {
            _viewModel.OnTransactionCompleted += (s, args) => MessageBox.Show(args.Message, "Transaction information", MessageBoxButton.OK);
            _viewModel.OnTransactionFailed += (s, args) => MessageBox.Show(args.Message, "Transaction information", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void TopUpButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.TryMakeTransaction();

            // depends from result, show message
        }
    }
}
