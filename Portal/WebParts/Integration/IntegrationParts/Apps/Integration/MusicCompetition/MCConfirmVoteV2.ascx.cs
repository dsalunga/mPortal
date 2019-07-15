using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Net;

using WCMS.WebSystem.Agent;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.MusicCompetition
{
    public partial class MCConfirmVoteV2 : System.Web.UI.UserControl
    {
        public string VoteUrl { get; set; }

        public string EventTitle { get; set; }
        public string ShortEventTitle { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var set = WebParameterSet.Get("ASOP Nomination");
                var code = DataHelper.Get(Request, "Code");
                var id = DataUtil.GetId(Request, "Id");

                VoteUrl = set.GetParameterValue("Vote-Url");

                EventTitle = set.GetParameterValue("EventTitle", "ASOP Asia & Oceania - People's Choice Nomination");
                ShortEventTitle = set.GetParameterValue("ShortEventTitle", "ASOP Asia & Oceania - People's Choice Nomination");

                if (id > 0 && !string.IsNullOrEmpty(code))
                {
                    var vote = MCVote.Provider.Get(id);
                    if (vote != null)
                    {
                        var ticks = vote.DateVoted.Date.Ticks.ToString("X").ToLower();
                        if (vote.Status == 0 && ticks.Equals(code, StringComparison.InvariantCultureIgnoreCase))
                        {
                            // Valid request

                            vote.Status = 1;
                            vote.Update();

                            var candidate = MCCandidate.Provider.Get(vote.CandidateId);

                            // Send Voting Notification

                            string emailTemplate = set.GetParameterValue("Vote-Email-Template");
                            string emailSubjTemplate = set.GetParameterValue("Vote-Email-Subject");
                            string smsTemplate = set.GetParameterValue("Vote-SMS-Template");
                            string cc = set.GetParameterValue("Vote-Alert-CC");

                            var baseAddress = set.GetParameterValue("BaseAddress");
                            if (string.IsNullOrEmpty(baseAddress))
                                baseAddress = WConfig.BaseAddress;

                            var provider = new NamedValueProvider();
                            provider.Add("BaseAddress", baseAddress);
                            provider.Add("FIRST_NAME", vote.FirstName);
                            provider.Add("LAST_NAME", vote.LastName);
                            provider.Add("CANDIDATE_NAME", candidate.Name);
                            provider.Add("CANDIDATE_ENTRY", candidate.Entry);
                            provider.Add("VOTING_CODE", vote.Code);
                            provider.Add("DATE_VOTED", vote.DateVoted);
                            provider.Add("EMAIL", vote.Email);

                            var emailContent = Substituter.Substitute(emailTemplate, provider);
                            var emailSubject = Substituter.Substitute(emailSubjTemplate, provider);
                            var smsMessage = Substituter.Substitute(smsTemplate, provider);

                            var sendVia = DataUtil.GetInt32(set.GetParameterValue("Send-Via", "2")); // Default is Both

                            var message = WebMessageQueue.Create(emailContent, smsMessage, sendVia, vote.Email, emailSubject, null);
                            message.AddTo(cc, vote.MobileNumber);
                            message.Update();

                            try
                            {
                                AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
                            }
                            catch (Exception ex)
                            {
                                LogHelper.WriteLog(ex);
                            }

                            return;
                        }
                    }
                }

                MultiView1.SetActiveView(ViewFailed);
            }
        }
    }
}