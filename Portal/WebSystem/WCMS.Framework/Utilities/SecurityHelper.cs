using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WCMS.Framework.Utilities
{
    public static class SecurityHelper
    {
        public const string SALT = "/1O+3xLOnBAqRf+qSWKqZi1jTFNF8BFg8ZtJz56lhXi72OWINdYS8NLq9Okx/rfg/7ke3rOKcfATSZEKYy66QA==";

        public static string ToHexString(byte[] byteValue)
        {
            char[] lookup = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            int i = 0, p = 0, l = byteValue.Length;
            char[] c = new char[l * 2];
            while (i < l)
            {
                byte d = byteValue[i++];
                c[p++] = lookup[d / 0x10];
                c[p++] = lookup[d % 0x10];
            }
            return new string(c, 0, c.Length);
        }

        public static byte[] ToHexByte(string str)
        {
            byte[] b = new byte[str.Length / 2];
            for (int y = 0, x = 0; x < str.Length; ++y, x++)
            {
                byte c1 = (byte)str[x];
                if (c1 > 0x60) c1 -= 0x57;
                else if (c1 > 0x40) c1 -= 0x37;
                else c1 -= 0x30;
                byte c2 = (byte)str[++x];
                if (c2 > 0x60) c2 -= 0x57;
                else if (c2 > 0x40) c2 -= 0x37;
                else c2 -= 0x30;
                b[y] = (byte)((c1 << 4) + c2);
            }
            return b;
        }

        public static string GetSalt(int length)
        {
            //Create and populate random byte array
            byte[] randomArray = new byte[length];
            string randomString;

            //Create random salt and convert to string
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomArray);
            randomString = Convert.ToBase64String(randomArray);
            return randomString;
        }

        public static string CreatePasswordHash(string password, string salt)
        {
            // Get a byte array containing the combined password + salt.
            string authDetails = password + salt;
            byte[] authBytes = System.Text.Encoding.ASCII.GetBytes(authDetails);

            // Use MD5 to compute the hash of the byte array, and return the hash as
            // a Base64-encoded string.
            var md5 = new MD5CryptoServiceProvider();
            byte[] hashedBytes = md5.ComputeHash(authBytes);
            string hash = Convert.ToBase64String(hashedBytes);

            return hash;
        }
    }
}
