using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.Apps.Integration
{
    public class MemberProfileModel
    {
        public MemberProfileModel()
        {
            
        }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string HomePhone { get; set; }
        public string Nickname { get; set; }



        // From ONEUserInfo
        public string UserName { get; set; }
        public string ExternalId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfMembership { get; set; }
    }
}
