using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;

namespace WCMS.WebSystem.Apps.Integration
{
    public class ProfileExtSyncTask : AgentTaskBase
    {
        public string FilterGroup { get; set; }

        public override void Execute()
        {
            var client = new MemberSoapClient(false);
            var items = MemberLink.Provider.GetList();
            bool hasFilter = !string.IsNullOrEmpty(FilterGroup);

            foreach (var item in items)
            {
                bool hasUpdates = false;

                var user = item.User;
                if (user != null)
                {
                    if (hasFilter && !user.IsMemberOf(FilterGroup))
                        continue;

                    Logger.WriteLine("Syncing {0}", user.FirstAndLastName);
                }

                // Update Profile information
                if (!string.IsNullOrEmpty(item.ExternalIdNo))
                {
                    item.TryPopulateProfileFromExt(client);
                    item.TryStatusFromExt(client);
                    hasUpdates = true;
                }

                if (item.MemberId > 0)
                {
                    //item.TryPopulatePhotoFromExt(client);
                    item.TryPopulateGroupsFromExt(client);
                    item.TryPopulateHomeAddressFromExt(client);
                    item.TryStatusFromExt(client);
                    hasUpdates = true;
                }

                if (hasUpdates)
                    item.Update();
            }
        }
    }
}
