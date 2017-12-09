using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WCMS.Common.Utilities;
using WCMS.Framework.Utilities;

namespace WCMS.Framework.Security
{
    public class LoginCookieManager
    {
        private string _salt = SecurityHelper.SALT;

        public LoginCookieManager()
        {

        }

        public LoginCookieManager(string salt)
        {
            this._salt = salt;
        }

        // Create a hash of the given password and salt.
        private string CreateHash(string password)
        {
            return SecurityHelper.CreatePasswordHash(password, _salt);
        }

        // Check to see if the given password and salt hash to the same value
        // as the given hash.
        public bool IsMatchingHash(string password, string hash)
        {
            // Recompute the hash from the given auth details, and compare it to
            // the hash provided by the cookie.
            return SecurityHelper.CreatePasswordHash(password, _salt) == hash;
        }

        // Create an authentication cookie that stores the username and a hash of
        // the password and salt.
        //public HttpCookie CreateAuthCookie(string username, string password)
        //{
        //    // Create the cookie and set its value to the username and a hash of the
        //    // password and salt. Use a pipe character as a delimiter so we can
        //    // separate these two elements later.
        //    HttpCookie cookie = new HttpCookie(_siteName); //"YourSiteCookieNameHere");
        //    cookie.Value = username + "|" + SecurityHelper.CreatePasswordHash(password, _salt);
        //    return cookie;
        //}

        public string GetAuthCookieValue(int userId, string password)
        {
            return userId + "|" + SecurityHelper.CreatePasswordHash(password, _salt);
        }

        // Determine whether the given authentication cookie is valid by
        // extracting the username, retrieving the saved password, recomputing its
        // hash, and comparing the hashes to see if they match. If they match,
        // then this authentication cookie is valid.
        private bool IsValidAuthCookieValue(string value, string password)
        {
            // Split the cookie value by the pipe delimiter.
            var values = value.Split('|');
            if (values.Length != 2) return false;

            // Retrieve the username and hash from the split values.
            int userId = DataHelper.GetInt32(values[0], -1);
            string hash = values[1];

            // You'll have to provide your GetPasswordForUser function.
            //string password = ""; //GetPasswordForUser(username);

            // Check the password and salt against the hash.
            return IsMatchingHash(password, hash);
        }

        public WebUser IsValidAuthCookieValue(string value)
        {
            // Split the cookie value by the pipe delimiter.
            var values = value.Split('|');
            if (values.Length != 2) return null;

            // Retrieve the username and hash from the split values.
            int userId = DataHelper.GetInt32(values[0], -1);
            string hash = values[1];

            WebUser user = userId > 0 ? WebUser.Get(userId) : null;
            if (user != null)
            {
                // You'll have to provide your GetPasswordForUser function.
                //string password = ""; //GetPasswordForUser(username);

                // Check the password and salt against the hash.
                if (user.ProviderId == AccountConstants.DefaultExternalProvider)
                    return user.Password.Equals(hash) ? user : null;
                else
                    return IsMatchingHash(user.Password, hash) ? user : null;
            }

            return null;
        }
    }
}
