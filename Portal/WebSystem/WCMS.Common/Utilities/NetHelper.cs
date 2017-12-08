using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Common.Utilities
{
    public static class NetHelper
    {
        public static string GetEncodedString(string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < chars.Length; index++)
            {
                string encodedString = EncodeChar(chars[index]);
                sb.Append(encodedString);
            }

            return sb.ToString();
        }
        private static string EncodeChar(char chr)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            StringBuilder sb = new StringBuilder();

            byte[] bytes = encoding.GetBytes(chr.ToString());
            for (int index = 0; index < bytes.Length; index++)
                sb.AppendFormat("%{0}", Convert.ToString(bytes[index], 16));

            return sb.ToString();
        }
    }
}
