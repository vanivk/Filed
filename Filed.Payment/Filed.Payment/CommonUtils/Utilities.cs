using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Filed.Payment.API.CommonUtils
{
	public static class Utilities
	{
        public static bool DateFormatValidator(string date)
        {
            bool dateValid = DateTime.TryParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateConvert);

            bool returnDateResponse = false;
            if (dateValid)
            {
                returnDateResponse = IsDateInThePast(dateConvert);
            }

            return returnDateResponse;
        }

        public static bool IsDateInThePast(DateTime date)
        {
            return date >= DateTime.Today;
        }

        /// <summary>
        /// Referred from : https://www.codeproject.com/Articles/36377/Validating-Credit-Card-Numbers
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static bool IsCardNumberValid(string cardNumber)
        {
            int i, checkSum = 0;
            for (i = cardNumber.Length - 1; i >= 0; i -= 2)
                checkSum += (cardNumber[i] - '0');
            for (i = cardNumber.Length - 2; i >= 0; i -= 2)
            {
                int val = ((cardNumber[i] - '0') * 2);
                while (val > 0)
                {
                    checkSum += (val % 10);
                    val /= 10;
                }
            }
            return ((checkSum % 10) == 0);
        }
    }
}
