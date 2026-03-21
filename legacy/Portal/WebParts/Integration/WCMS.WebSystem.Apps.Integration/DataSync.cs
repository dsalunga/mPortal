using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration
{
    [Serializable]
    public class MemberLinkContainer
    {
        public MemberLinkContainer()
        {
            UserName = string.Empty;
        }

        public MemberLinkContainer(MemberLink link, int itemType)
            : this()
        {
            Link = link;
            ItemType = itemType;

            if (link != null)
            {
                var user = link.User;
                if (user != null)
                    UserName = user.UserName;
            }
        }

        public string UserName { get; set; }
        public MemberLink Link { get; set; }
        public int ItemType { get; set; }
    }

    public class MemberLinkEqualityComparer : IEqualityComparer<MemberLinkContainer>
    {
        public bool Equals(MemberLinkContainer x, MemberLinkContainer y)
        {
            return x.UserName.Equals(y.UserName, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode(MemberLinkContainer obj)
        {
            return obj.UserName.ToLower().GetHashCode();
        }
    }
}
