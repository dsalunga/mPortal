using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WCMS.Framework.Utilities
{
    public class LoginSecurity
    {
        private static ISession GetSession()
        {
            return WCMS.Common.Utilities.HttpContextHelper.Current?.Session;
        }

        public static List<string> Decode(string[] decode)
        {
            var session = GetSession();
            string domainKey = session?.GetString("KeyPair");
            if (!string.IsNullOrEmpty(domainKey))
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(domainKey);

                var items = new List<string>();
                foreach (var d in decode)
                {
                    if (!string.IsNullOrEmpty(d))
                        items.Add(Encoding.UTF8.GetString(rsa.Decrypt(SecurityHelper.ToHexByte(d), false)));
                    else
                        items.Add(string.Empty);
                }

                return items;
            }

            return new List<string>();
        }

        public static string[] DecodeLogin(string encUserName, string encPassword)
        {
            var session = GetSession();
            string domainKey = session?.GetString("KeyPair");
            if (!string.IsNullOrEmpty(domainKey))
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(domainKey);
                string username = Encoding.UTF8.GetString(rsa.Decrypt(SecurityHelper.ToHexByte(encUserName), false));
                string password = Encoding.UTF8.GetString(rsa.Decrypt(SecurityHelper.ToHexByte(encPassword), false));

                return new string[] { username, password };
            }

            return new string[] { };
        }

        public bool ValidateUser(string encUsername, string encPassword, bool rememberMe)
        {
            var session = GetSession();
            string domainKey = session?.GetString("KeyPair");
            if (!string.IsNullOrEmpty(domainKey))
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(domainKey);
                string username = Encoding.UTF8.GetString(rsa.Decrypt(SecurityHelper.ToHexByte(encUsername), false));
                string password = Encoding.UTF8.GetString(rsa.Decrypt(SecurityHelper.ToHexByte(encPassword), false));

                var user = AccountHelper.ValidateLogin(username, password);
                return user != null && user.IsActive;
            }

            return false;
        }

        public static string GetLoginKey()
        {
            byte[] data = GetPublicKey();
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(SECRET_KEY);
                aes.IV = Convert.FromBase64String(INIT_VECTOR);

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream msDecrypt = new MemoryStream(data))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        private const string SECRET_KEY = "<redacted-login-secret-key>";
        private const string INIT_VECTOR = "<redacted-login-init-vector>=";

        public static byte[] GetPublicKey()
        {
            var rsa = new RSACryptoServiceProvider();
            var session = GetSession();
            if (session == null) return Array.Empty<byte>();

            var keyPair = session.GetString("KeyPair");
            if (!string.IsNullOrEmpty(keyPair))
                rsa.FromXmlString(keyPair);
            else
                session.SetString("KeyPair", rsa.ToXmlString(true));

            RSAParameters param = rsa.ExportParameters(false);

            string keyToSend = SecurityHelper.ToHexString(param.Exponent) + "," +
                 SecurityHelper.ToHexString(param.Modulus);

            // Encrypting public key to block Man-in-the-Middle attack
            byte[] encrypted;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(SECRET_KEY);
                aes.IV = Convert.FromBase64String(INIT_VECTOR);
                ICryptoTransform encryptor = aes.CreateEncryptor();
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(keyToSend);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }
    }



    /*
                function validateUser() {
                    var pkey = $('#hKey').val().split(',');
                    var rsa = new RSAKey();

                    rsa.setPublic(pkey[1], pkey[0]);

                    var username = rsa.encrypt($('#txtUserName').val());
                    var pass = rsa.encrypt($('#txtPassword').val());

                    $('.FailureText').css('color', 'blue');
                    $('.FailureText div').text('Checking User/Pass....');
                    $.ajax({
                        type: "POST",
                        url: "Services/LoginService.asmx/ValidateUser",
                        data: "{'encUsername':'" + username + "','encPassword':'" + pass + "','rememberMe':'" + $('#chbRememberMe').attr('checked') + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data, status) { OnSuccessLogin(data, status); },
                        error: OnErrorLogin
                    });
                }

                function OnSuccessLogin(data, status) {
                    if (data.d) {
                        $('#FailureText').css('color', 'green');
                        $('#FailureText div').text('Authentication was successfull. you will redirect to previous page');
                        var firstPage = getParameterByName("ReturnUrl");
                        if (firstPage)
                            window.location.href = firstPage;
                    }
                    else {
                        $('#FailureText').css('color', 'red');
                        $('#FailureText div').text('username / password is invalid');
                    }
                }

                function OnErrorLogin(request, status, error) {
                    $('.FailureText div').text('There is an error in aythenticating process');
                }

                function getParameterByName(name) {
                    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
                    var regexS = "[\\?&]" + name + "=([^&#]*)";
                    var regex = new RegExp(regexS);
                    var results = regex.exec(window.location.href);
                    if (results == null)
                        return "";
                    else
                        return decodeURIComponent(results[1].replace(/\+/g, " "));
                }
                */
}
