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
                        return MD5.Create();

                    case Algorithm.SHA1:
                        return SHA1.Create();

                    case Algorithm.SHA256:
                        return SHA256.Create();

                    case Algorithm.SHA384:
                        return SHA384.Create();

                    case Algorithm.SHA512:
                        return SHA512.Create();

                    default:
                        return MD5.Create();
                }
            }

            public static byte[] CreateSalt(int intSize)
            {
                byte[] byteBuff = new byte[intSize];
                RandomNumberGenerator.Fill(byteBuff);
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
