using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework
{
    public class WSUserInfo
    {
        public WSUserInfo()
        {
            IsServiceAccount = false;
        }

        public WSUserInfo(WebUser user)
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.UserName = user.UserName;
            this.MobileNumber = user.MobileNumber;
            this.Email = user.Email;
            this.Gender = user.Gender;

            if (user.IsServiceAccount())
                this.IsServiceAccount = true;
        }

        public int Id { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public DateTime LastUpdate { get; set; }

        public int Active { get; set; }
        public bool IsServiceAccount { get; set; }

        public DateTime DateCreated { get; set; }

        public char Gender { get; set; }

        public string MobileNumber { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
