using System;
using System.Text.RegularExpressions;

namespace WCMS.Common.Utilities
{
    public enum RegexPresets { Email, MobileNumber };

    /// <summary>
    /// Summary description for Validator.
    /// </summary>
    public abstract class Validator
    {
        public static bool IsRegexMatch(string testString, RegexPresets rp)
        {
            Regex regEx;

            switch (rp)
            {
                case RegexPresets.Email:
                    regEx = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                    return regEx.IsMatch(testString);

                case RegexPresets.MobileNumber:
                    return IsMobileNumber(testString);

                default:
                    return false;
            }
        }

        private static bool IsMobileNumber(string test)
        {
            if (test.Length < 8)
                return false;

            var chars = test.ToCharArray();

            foreach (var c in chars)
            {
                if (!char.IsNumber(c) && c != '+' && c != '-' && c != ' ')
                    return false;
            }

            return true;
        }
    }
}
