using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashDispenserLibrary.Exceptions
{
    public class AccountNotFoundException: Exception
    {
        public AccountNotFoundException(){ }
        public AccountNotFoundException(string? message) : base(message){ }
    }
}
