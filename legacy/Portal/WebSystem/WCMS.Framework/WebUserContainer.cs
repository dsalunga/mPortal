using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    [Serializable]
    public class WebUserContainer
    {
        public WebUserContainer()
        {
            Addresses = new List<WebAddress>();
            Groups = new List<WebGroup>();
        }

        public WebUserContainer(WebUser user, int itemType)
            : this()
        {
            User = user;
            ItemType = itemType;

            if (user != null)
            {
                Addresses.AddRange(user.Addresses);
                Groups.AddRange(user.Groups);
            }
        }

        public WebUser User { get; set; }
        public int ItemType { get; set; }

        public List<WebGroup> Groups { get; set; }
        public List<WebAddress> Addresses { get; set; }
    }
}
