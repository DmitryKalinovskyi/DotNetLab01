using System;
using System.Globalization;
using System.Windows.Controls;

namespace CashDispenserWinApp.ValidationRules
{
    internal class CardIDValidationRule: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string s = (string)value;

                long cardID = long.Parse(s);
                return ValidationResult.ValidResult;
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }
        }
    }
}
