using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Client.Helper
{
    public static class Validator
    {
        internal static bool IsEmail(string inputEmail)
        {
            const string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);

            return !string.IsNullOrEmpty(inputEmail) && re.IsMatch(inputEmail);
        }

        internal static bool ValidateFields(string[] value)
        {
            return value.All(s => !string.IsNullOrEmpty(s));
        }
    }
}
