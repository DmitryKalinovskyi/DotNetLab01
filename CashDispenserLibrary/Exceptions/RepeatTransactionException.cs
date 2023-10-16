using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashDispenserLibrary.Exceptions
{
    public class RepeatTransactionException: Exception
    {
        public RepeatTransactionException() { }
        public RepeatTransactionException(string message): base(message) { }
    }
}
