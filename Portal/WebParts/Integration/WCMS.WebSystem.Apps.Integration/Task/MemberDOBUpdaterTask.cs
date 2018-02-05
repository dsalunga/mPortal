using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;

namespace WCMS.WebSystem.Apps.Integration.Task
{
    public class MemberDOBUpdaterTask : AgentTaskBase
    {
        public override void Execute()
        {
            try
            {
                if (Logger.Loggers.Count == 0)
                    Logger.AddConsoleLogger();

                this.Logger.WriteLine("[{0}] {1} Fetching bulk data...", TaskName, DateTime.Now);


                //ExternalDBEntities db = new ExternalDBEntities();
                var items = MemberLink.Provider.GetList();
                //var members = db.MemberStatuses;
                var client = MemberSoapClient.GetNewClientInstance();

                foreach (var item in items)
                {
                    if (item.MemberId > 0)
                    {
                        //var member = members.FirstOrDefault(i => i.MemberID == item.MemberId);
                        var member = client.GetMembershipStatus(item.MemberId);
                        if (member != null && member.MembershipDate != null && item.MembershipDate.Date != member.MembershipDate.Date)
                        {
                            var user = item.User;
                            if (user != null)
                            {
                                Logger.WriteLine("Updating DOB of {0}", user.FirstAndLastName);

                                item.MembershipDate = member.MembershipDate;
                                item.Update();
                            }
                        }
                    }
                }

                this.Logger.WriteLine("[{0}] {1} Execution completed.", TaskName, DateTime.Now);
            }
            catch (Exception ex)
            {
                this.Logger.WriteLine("[{0}] {1} An error has encountered, execution terminated:", TaskName, DateTime.Now);
                this.Logger.WriteLine(ex.ToString());
            }
        }
    }
}
