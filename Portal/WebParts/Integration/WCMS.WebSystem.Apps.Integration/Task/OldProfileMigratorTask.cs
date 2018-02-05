using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;


namespace WCMS.WebSystem.Apps.Integration
{
    public class OldProfileMigratorTask : AgentTaskBase
    {
        public override void Execute()
        {
            this.Logger.WriteLine("[{0}] {1} Execution started.", TaskName, DateTime.Now);

            try
            {
                var items = MemberLink.Provider.GetList();
                foreach (var item in items)
                {
                    var user = item.User;
                    if (user != null)
                    {
                        this.Logger.WriteLine("Updating information for {0}", user.FirstAndLastName);

                        var addresses = user.Addresses;

                        // Home Address
                        var homeAddress = addresses.FirstOrDefault(a => a.Tag.Equals(AddressTags.Home, StringComparison.InvariantCultureIgnoreCase));
                        homeAddress = MemberLink.WebAddressFromMemberLink(item, AddressTags.Home, homeAddress);
                        homeAddress.Update();

                        // Work Address
                        var workAddress = addresses.FirstOrDefault(a => a.Tag.Equals(AddressTags.Work, StringComparison.InvariantCultureIgnoreCase));
                        workAddress = MemberLink.WebAddressFromMemberLink(item, AddressTags.Work, workAddress);
                        workAddress.Update();

                        bool updateUser = false;
                        if (string.IsNullOrEmpty(user.MobileNumber))
                        {
                            user.MobileNumber = item.MobileNumber;
                            updateUser = true;
                        }

                        if (string.IsNullOrEmpty(user.TelephoneNumber))
                        {
                            user.TelephoneNumber = item.HomePhone;
                            updateUser = true;
                        }

                        if (updateUser)
                            user.Update(true);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Logger.WriteLine("[{0}] {1} An error has encountered:", TaskName, DateTime.Now);
                this.Logger.WriteLine(ex.ToString());
            }

            this.Logger.WriteLine("[{0}] {1} Execution completed.", TaskName, DateTime.Now);
        }
    }
}
