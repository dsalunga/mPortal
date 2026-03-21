using System;
using System.Text;
using System.Security.Cryptography;

namespace WCMS
{
    namespace Security
    {
        public enum Algorithm : int
        {
            MD5 = 128,
            SHA1 = 160,
            SHA256 = 256,
            SHA384 = 384,
            SHA512 = 512
        };

        public abstract class Hashing
        {
            private static HashAlgorithm BuildAlgorithm(Algorithm algo)
            {
                switch (algo)
                {
                    case Algorithm.MD5:
                        return new MD5CryptoServiceProvider();

                    case Algorithm.SHA1:
                        return new SHA1Managed();

                    case Algorithm.SHA256:
                        return new SHA256Managed();

                    case Algorithm.SHA384:
                        return new SHA384Managed();

                    case Algorithm.SHA512:
                        return new SHA512Managed();

                    default:
                        return new MD5CryptoServiceProvider();
                }
            }

            public static byte[] CreateSalt(int intSize)
            {
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] byteBuff = new byte[intSize];

                rng.GetBytes(byteBuff);

                return byteBuff;
            }

            public static string ComputeHash(string hash, Algorithm algo, byte[] byteSalt)
            {
                HashAlgorithm algoHash = BuildAlgorithm(algo);
                if (byteSalt == null)
                    byteSalt = CreateSalt(255);

                byte[] byteText = Encoding.UTF8.GetBytes(hash);
                byte[] byteTextSalt = new byte[byteText.Length + byteSalt.Length];

                byteText.CopyTo(byteTextSalt, 0);
                byteSalt.CopyTo(byteTextSalt, byteText.Length);

                

                byte[] byteHash = algoHash.ComputeHash(byteTextSalt);
                byte[] byteHashSalt = new byte[byteHash.Length + byteSalt.Length];

                byteHash.CopyTo(byteHashSalt, 0);
                byteSalt.CopyTo(byteHashSalt, byteHash.Length);

                return Convert.ToBase64String(byteHashSalt);
            }

            public static string ComputeHash(string hash, Algorithm algo)
            {
                HashAlgorithm algoHash = BuildAlgorithm(algo);
                byte[] byteHash = algoHash.ComputeHash(Encoding.UTF8.GetBytes(hash));

                return Convert.ToBase64String(byteHash);
            }

            public static bool VerifyHash(string hash, Algorithm algo, string strHash, bool isSalted)
            {
                if (isSalted)
                {
                    int intBits = (int)algo;
                    int intBytes = intBits / 8;

                    try
                    {
                        byte[] byteHashSalt = Convert.FromBase64String(strHash);
                        byte[] byteSalt = new byte[byteHashSalt.Length - intBytes];
                        
                        for (int x = 0; x < byteSalt.Length; x++)
                            byteSalt[x] = byteHashSalt[intBytes + x];
                        
                        string strMatch = ComputeHash(hash, algo, byteSalt);
                        return (strHash == strMatch);
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        string strMatch = ComputeHash(hash, algo);
                        return (strHash == strMatch);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
    }
}