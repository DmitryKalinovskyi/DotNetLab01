using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashDispenserLibrary.Exceptions
{
    public class AccountPINException : Exception
    {
        public AccountPINException() { }
        public AccountPINException(string? message): base(message) { }
    }
}
