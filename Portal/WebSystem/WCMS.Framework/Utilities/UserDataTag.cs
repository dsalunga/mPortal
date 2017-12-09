using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Common;

namespace WCMS.Framework.Utilities
{
    public class UserDataTag : INamedValueProvider
    {
        public const string NAME_PREFIX = "NAME_PREFIX";
        public const string FIRST_NAME = "FIRST_NAME";
        public const string LAST_NAME = "LAST_NAME";

        public WebUser User;
        public string Tag;

        public UserDataTag(string tag, WebUser user = null)
        {
            this.User = user;
            this.Tag = tag;
        }

        #region INamedValueProvider Members

        public string GetValue(string key)
        {
            switch (key.ToUpper())
            {
                case NAME_PREFIX:
                    var prefix = AccountHelper.GetNamePrefix(User, NamePrefixes.Brotherhood);
                    return string.IsNullOrEmpty(prefix) ? prefix : string.Format("{0}. ", prefix);

                case FIRST_NAME:
                    return User != null ? User.FirstName : string.Empty;

                case LAST_NAME:
                    return User != null ? User.LastName : string.Empty;
            }

            return string.Empty;
        }

        public bool ContainsKey(string key)
        {
            switch (key.ToUpper())
            {
                case NAME_PREFIX:
                case FIRST_NAME:
                case LAST_NAME:
                    return true;

                default:
                    return false;
            }
        }

        public string this[string key]
        {
            get { return GetValue(key); }
            set { throw new NotImplementedException(); }
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
