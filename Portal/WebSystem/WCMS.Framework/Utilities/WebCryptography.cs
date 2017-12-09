using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public static class WCryptography
    {
        private static KeyPair _keyPair = null;
        public static KeyPair KeyPair
        {
            get
            {
                if (_keyPair == null)
                {
                    _keyPair = new KeyPair();
                    _keyPair.PrivateKey = WebRegistry.SelectNode("/System/Security/PrivateKey").Value;
                    _keyPair.PublicKey = WebRegistry.SelectNode("/System/Security/PublicKey").Value;
                }

                return _keyPair;
            }
        }

        //static WebCryptography()
        //{
        //    // Load keys here
        //    keyPair = GenerateKeyPair();
        //}

        public static string DecryptString(string inputString)
        {
            //int dwKeySize = 1024;
            //if (KeyPair == null) return null; //keyPair = GenerateKeyPair();

            string fileString = KeyPair.PrivateKey;
            string bitStrengthString = fileString.Substring(0, fileString.IndexOf("</BitStrength>") + 14);
            fileString = fileString.Replace(bitStrengthString, "");
            int dwKeySize = Convert.ToInt32(bitStrengthString.Replace("<BitStrength>", "").Replace("</BitStrength>", ""));

            // TODO: Add Proper Exception Handlers
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
            rsaCryptoServiceProvider.FromXmlString(fileString);
            int base64BlockSize = ((dwKeySize / 8) % 3 != 0) ? (((dwKeySize / 8) / 3) * 4) + 4 : ((dwKeySize / 8) / 3) * 4;
            int iterations = inputString.Length / base64BlockSize;
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < iterations; i++)
            {
                byte[] encryptedBytes = Convert.FromBase64String(inputString.Substring(base64BlockSize * i, base64BlockSize));
                // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
                // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
                // Comment out the next line and the corresponding one in the EncryptString function.
                Array.Reverse(encryptedBytes);
                arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(encryptedBytes, true));
            }
            return Encoding.UTF32.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
        }

        public static string EncryptString(string inputString)
        {
            //int dwKeySize = 1024;
            //if (keyPair == null) return null; // keyPair = GenerateKeyPair();

            string fileString = KeyPair.PublicKey;
            string bitStrengthString = fileString.Substring(0, fileString.IndexOf("</BitStrength>") + 14);
            fileString = fileString.Replace(bitStrengthString, "");
            int dwKeySize = Convert.ToInt32(bitStrengthString.Replace("<BitStrength>", "").Replace("</BitStrength>", ""));

            // TODO: Add Proper Exception Handlers
            var rsaCryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
            rsaCryptoServiceProvider.FromXmlString(fileString);
            int keySize = dwKeySize / 8;
            byte[] bytes = Encoding.UTF32.GetBytes(inputString);
            // The hash function in use by the .NET RSACryptoServiceProvider here is SHA1
            // int maxLength = ( keySize ) - 2 - ( 2 * SHA1.Create().ComputeHash( rawBytes ).Length );
            int maxLength = keySize - 42;
            int dataLength = bytes.Length;
            int iterations = dataLength / maxLength;
            var stringBuilder = new StringBuilder();
            for (int i = 0; i <= iterations; i++)
            {
                byte[] tempBytes = new byte[(dataLength - maxLength * i > maxLength) ? maxLength : dataLength - maxLength * i];
                Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0, tempBytes.Length);
                byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes, true);
                // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
                // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
                // Comment out the next line and the corresponding one in the DecryptString function.
                Array.Reverse(encryptedBytes);
                // Why convert to base 64?
                // Because it is the largest power-of-two base printable using only ASCII characters
                stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
            }
            return stringBuilder.ToString();
        }

        public static KeyPair GenerateKeyPair()
        {
            int bitStrength = 1024;

            //CspParameters cspParams = new CspParameters();
            //cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
            var RSAProvider = new RSACryptoServiceProvider(bitStrength);
            string publicAndPrivateKeys = "<BitStrength>" + bitStrength.ToString() + "</BitStrength>" + RSAProvider.ToXmlString(true);
            string justPublicKey = "<BitStrength>" + bitStrength.ToString() + "</BitStrength>" + RSAProvider.ToXmlString(false);

            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspParams);

            KeyPair keyPair = new KeyPair();
            keyPair.PrivateKey = publicAndPrivateKeys;
            keyPair.PublicKey = justPublicKey;
            //keyPair.CspParams = cspParams;

            return keyPair;
        }
    }

    public class KeyPair
    {
        private string _publicKey;

        public string PublicKey
        {
            get { return _publicKey; }
            set { _publicKey = value; }
        }

        private string _privateKey;

        public string PrivateKey
        {
            get { return _privateKey; }
            set { _privateKey = value; }
        }
    }
}
