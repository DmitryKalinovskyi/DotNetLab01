using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashDispenserLibrary.Exceptions
{
    public class SelfPaymentException: Exception
    {
        public SelfPaymentException() { }
        public SelfPaymentException(string message) : base(message) { }
    }
}
