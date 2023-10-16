using System;
using System.Globalization;
using System.Windows.Controls;

namespace CashDispenserWinApp.ValidationRules
{
    public class PINValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string s = (string)value;

                int pin = int.Parse(s);
                return ValidationResult.ValidResult;
            }
            catch(Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }
        }
    }
}
