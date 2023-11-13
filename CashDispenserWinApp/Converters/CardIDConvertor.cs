using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CashDispenserWinApp.Converters
{
    public class CardIDConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long cardIDLong)
            {
                string cardID = cardIDLong.ToString();
                if (cardID.Length >= 16)
                {
                    string formated = $"{cardID.Substring(0, 4)} {cardID.Substring(4, 4)} {cardID.Substring(8, 4)} {cardID.Substring(12)}";
                    return formated;
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
