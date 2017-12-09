using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    [Serializable]
    public abstract class UserInfo : ParameterizedWebObject
    {
        public UserInfo()
        {
            MobileNumber = string.Empty;
            TelephoneNumber = string.Empty;
        }

        [ObjectColumn]
        public string UserName { get; set; }

        [ObjectColumn]
        public string FirstName { get; set; }

        [ObjectColumn]
        public string MiddleName { get; set; }

        [ObjectColumn]
        public string LastName { get; set; }

        [ObjectColumn]
        public string Email { get; set; }

        [ObjectColumn]
        public DateTime LastUpdate { get; set; }

        [ObjectColumn]
        public int Status { get; set; }

        [ObjectColumn]
        public DateTime DateCreated { get; set; }

        [ObjectColumn]
        public char Gender { get; set; }

        [ObjectColumn]
        public string MobileNumber { get; set; }

        [ObjectColumn]
        public string TelephoneNumber { get; set; }
    }
}
