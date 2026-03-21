using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using WCMS.Framework;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataSync" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataSync.svc or DataSync.svc.cs at the Solution Explorer and start debugging.
    public class DataSync : IDataSync
    {
        public void DoWork()
        {
        }

        public List<MemberLinkContainer> GetMemberLinkList()
        {
            var links = MemberLink.Provider.GetList();
            WebUser user = null;
            List<MemberLinkContainer> items = new List<MemberLinkContainer>();

            items.AddRange(
                from l in links
                select new MemberLinkContainer
                {
                    ItemType = RemoteItemTypes.REMOTE,
                    UserName = (user = l.User) != null ? user.UserName : string.Empty,
                    Link = l
                }
            );

            return items;
        }

        public MemberLinkContainer GetMemberLinkComplete(string userName)
        {
            var user = WebUser.Get(userName);
            if (user != null)
            {
                var link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null)
                    return new MemberLinkContainer(link, RemoteItemTypes.REMOTE);
            }

            return null;
        }

        public void SetMemberLinkComplete(MemberLinkContainer container)
        {
            if (container != null && !string.IsNullOrEmpty(container.UserName) && container.Link != null
                && (container.ItemType == RemoteItemTypes.REMOTE || container.ItemType == RemoteItemTypes.IDENTICAL))
            {
                var user = WebUser.Get(container.UserName);
                if (user != null)
                {
                    var item = MemberLink.Provider.GetByUserId(user.Id);
                    if (item == null)
                    {
                        item = container.Link;
                        item.Id = -1;
                        item.UserId = user.Id;
                        item.Update();
                    }
                }
            }
        }
    }
}
