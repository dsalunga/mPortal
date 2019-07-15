using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.MusicCompetition
{
    public partial class ASOPMobile : System.Web.UI.UserControl
    {
        public string UserName { get; set; }
        public string DoneUrl { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public string EventTitle { get; set; }
        public string ShortEventTitle { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (WSession.Current.IsLoggedIn)
                {
                    var user = WSession.Current.User;

                    UserName = user.UserName;
                    Mobile = user.MobileNumber;
                    Email = user.Email;

                    textFirstName.Value = user.FirstName;
                    textLastName.Value = user.LastName;

                    var context = new WContext(this);
                    var paramObj = context.ParameterizedObject;
                    var element = context.Element;

                    var competitionId = DataUtil.GetId(element.GetParameterValue("CompetitionId"));
                    hCompetitionId.Value = competitionId.ToString();
                    Repeater1.DataBind();

                    DoneUrl = paramObj.GetParameterValue("DoneUrl", "/Vote-Wall/");
                    EventTitle = paramObj.GetParameterValue("EventTitle", "ASOP Asia & Oceania District 3 - People's Choice Nomination");
                    ShortEventTitle = paramObj.GetParameterValue("ShortEventTitle", "ASOP Asia & Oceania District 3 - People's Choice Nomination");
                }
            }
        }

        public IEnumerable<MCCandidate> GetCandidates(int competitionId)
        {
            return MCCandidate.Provider.GetList(competitionId);
        }
    }
}