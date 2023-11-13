using CashDispenserLibrary.Core;

namespace CashDispenserWinApp.ViewModels
{
    public class MainWindowViewModel: NotifyViewModel
    {
        public LoginViewModel LoginViewModel { get; private set; }

        private object _selectedView;
        public object SelectedView {
            get { return _selectedView; }
            set {
                _selectedView = value;
                OnPropertyChanged(nameof(SelectedView));
            }
        }

        public MainWindowViewModel(LoginViewModel loginViewModel)
        {
            LoginViewModel = loginViewModel;
        }

        public void Logout()
        {
            Session.CurrentSession.EndSession();
        }
    }
}
