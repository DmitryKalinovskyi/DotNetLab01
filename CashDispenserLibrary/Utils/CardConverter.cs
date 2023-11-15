using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashDispenserLibrary.Utils
{
    public static class CardConverter
    {
        public static string DefaultCardSeparators = " -";

        public static long ParseCard(string cardID)
        {
            // Remove spaces and separators
            string ans = "";

            foreach(char c in cardID)
            {
                if(DefaultCardSeparators.IndexOf(c) == -1) ans += c;
            }

            return long.Parse(ans);
        }

        public static string CardToString(long cardIDLong)
        {
            string cardID = cardIDLong.ToString();

            if (cardID.Length > 16) throw new ArgumentException("Card id is too long!");

            if(cardID.Length < 16) cardID.PadLeft(16 - cardID.Length, '0');

            string formated = $"{cardID.Substring(0, 4)} {cardID.Substring(4, 4)} {cardID.Substring(8, 4)} {cardID.Substring(12)}";

            return formated;
        }

    }
}
