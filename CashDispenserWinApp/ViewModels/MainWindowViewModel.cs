using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CashDispenserWinApp.ViewModels
{
    public class MainWindowViewModel
    {
        public LoginViewModel LoginViewModel { get; private set; }


        public MainWindowViewModel(LoginViewModel loginViewModel)
        {
            LoginViewModel = loginViewModel;
        }
    }
}
