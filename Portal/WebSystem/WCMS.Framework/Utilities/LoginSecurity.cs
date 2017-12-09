using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WCMS.Framework.Utilities
{
    public class LoginSecurity
    {
        public static List<string> Decode(string[] decode)
        {
            string domainKey = HttpContext.Current.Session["KeyPair"] as string; //(string)HttpRuntime.Cache["KeyPair"];
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
            string domainKey = HttpContext.Current.Session["KeyPair"] as string; //(string)HttpRuntime.Cache["KeyPair"];
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
            // Check request number from this ip is in allowed range
            /*
            if (!ActionValidator.IsValid(ActionValidator.ActionTypeEnum.FirstVisit))
                return false;*/

            //read Key Pair (Public + Private Key) from Cache
            string domainKey = HttpContext.Current.Session["KeyPair"] as string; //(string)HttpRuntime.Cache["KeyPair"];
            if (!string.IsNullOrEmpty(domainKey))
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(domainKey);
                string username = Encoding.UTF8.GetString(rsa.Decrypt(SecurityHelper.ToHexByte(encUsername), false));
                string password = Encoding.UTF8.GetString(rsa.Decrypt(SecurityHelper.ToHexByte(encPassword), false));
                //if (Membership.ValidateUser(username, password))
                {
                    //FormsAuthentication.SetAuthCookie(username, rememberMe);
                    return true;
                }
            }

            return false;
        }

        public static string GetLoginKey()
        {
            byte[] data = GetPublicKey();
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                //string[] key = File.ReadAllLines(Server.MapPath("~/App_Data/AESKey.txt"));
                rijAlg.Key = Convert.FromBase64String(SECRET_KEY); //key[0]);
                rijAlg.IV = Convert.FromBase64String(INIT_VECTOR); //key[1]);

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(data))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            //return string.Empty;
        }

        private const string SECRET_KEY = "BjNgDXJT5vfUXDVcHMFYom1fic8MOXoFLi6OOWp3kG4=";
        private const string INIT_VECTOR = "f5Bia7HKcuiGoJEvXDM9ag==";

        public static byte[] GetPublicKey()
        {
            var rsa = new RSACryptoServiceProvider();
            var session = HttpContext.Current.Session;

            var keyPair = session["KeyPair"] as string;
            if (!string.IsNullOrEmpty(keyPair))
                rsa.FromXmlString(keyPair);
            else
                session["KeyPair"] = rsa.ToXmlString(true);

            //Add Key Pair (Public + Private Key) to Cache to be used by ValidateUser
            //HttpContext.Current.Session["KeyPair"] = rsa.ToXmlString(true); //HttpRuntime.Cache["KeyPair"] = rsa.ToXmlString(true);
            RSAParameters param = rsa.ExportParameters(false);

            string keyToSend = SecurityHelper.ToHexString(param.Exponent) + "," +
                 SecurityHelper.ToHexString(param.Modulus);

            // Encrypting public key to block Man-in-the-Middle attack
            byte[] encrypted;
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                //string[] key = File.ReadAllLines(Server.MapPath("~/App_Data/AESKey.txt"));
                myRijndael.Key = Convert.FromBase64String(SECRET_KEY); //key[0]);
                myRijndael.IV = Convert.FromBase64String(INIT_VECTOR); //key[1]);
                ICryptoTransform encryptor = myRijndael.CreateEncryptor();
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

    public static class ActionValidator
    {
        private const int DURATION = 10; // 10 min period

        /*
        * Type of actions and their maximum value per period
        *
        */

        public enum ActionTypeEnum
        {
            None = 0,
            FirstVisit = 20, // The most expensive one, choose the value wisely.
            Revisit = 1000, // Welcome to revisit as many times as user likes
            Postback = 5000, // Not must of a problem for us
            AddNewWidget = 100,
            AddNewPage = 100,
        }

        private class HitInfo
        {
            public int Hits;
            private DateTime _ExpiresAt = DateTime.Now.AddMinutes(DURATION);

            public DateTime ExpiresAt
            {
                get { return _ExpiresAt; }
                set { _ExpiresAt = value; }
            }
        }

        public static bool IsValid(ActionTypeEnum actionType)
        {
            HttpContext context = HttpContext.Current;
            if (context.Request.Browser.Crawler) return false;

            string key = actionType.ToString() + context.Request.UserHostAddress;

            HitInfo hit = (HitInfo)(context.Cache[key] ?? new HitInfo());

            if (hit.Hits > (int)actionType) return false;
            else hit.Hits++;

            if (hit.Hits == 1)
                context.Cache.Add(key, hit, null, DateTime.Now.AddMinutes(DURATION),
                                  System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal,
                                  null);

            return true;
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
