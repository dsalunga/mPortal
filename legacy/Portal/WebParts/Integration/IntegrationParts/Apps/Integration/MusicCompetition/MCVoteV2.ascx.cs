using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;

using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.MusicCompetition
{
    public partial class MCVoteV2 : System.Web.UI.UserControl
    {
        public string DoneUrl { get; set; }
        public string ResultsUrl { get; set; }

        public string EventInfoContent { get; set; }

        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public string EventTitle { get; set; }
        public string ShortEventTitle { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var context = new WContext(this);
                var paramObj = context.ParameterizedObject;

                DoneUrl = paramObj.GetParameterValue("DoneUrl", "/Vote-Wall/");
                ResultsUrl = paramObj.GetParameterValue("ResultsUrl", "/Vote-Results/");

                EventTitle = paramObj.GetParameterValue("EventTitle", "ASOP Asia & Oceania District 3 - People's Choice Nomination");
                ShortEventTitle = paramObj.GetParameterValue("ShortEventTitle", "ASOP Asia & Oceania District 3 - People's Choice Nomination");

                EventInfoContent = paramObj.GetParameterValue("EventInfoContent");

                var competitionId = DataUtil.GetId(paramObj.GetParameterValue("CompetitionId"));
                hCompetitionId.Value = competitionId.ToString();
                Repeater1.DataBind();

                if (WSession.Current.IsLoggedIn)
                {
                    var user = WSession.Current.User;

                    UserName = user.UserName;
                    Mobile = user.MobileNumber;
                    Email = user.Email;

                    textFirstName.Value = user.FirstName;
                    textLastName.Value = user.LastName;
                }
            }
        }

        public IEnumerable<MCCandidate> GetCandidates(int competitionId)
        {
            return from i in MCCandidate.Provider.GetList(competitionId)
                   orderby i.Entry
                   select i;
        }
    }
}