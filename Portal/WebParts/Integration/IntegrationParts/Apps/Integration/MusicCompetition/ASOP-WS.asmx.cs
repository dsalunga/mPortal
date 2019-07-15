using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Net;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

using WCMS.WebSystem.Agent;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.MusicCompetition
{
    /// <summary>
    /// Summary description for ASOP_WS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ASOP_WS : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public MCCandidate GetCandidate(int id)
        {
            if (id > 0)
                return MCCandidate.Provider.Get(id);

            return null;
        }

        [WebMethod]
        public bool IsEmailNotTaken(int competitionId, string email)
        {
            return MCVote.Provider.GetByEmail(competitionId, email, 1) == null;
        }

        [WebMethod(EnableSession = true)]
        public int Vote(MCVote vote)
        {
            if (vote.CompetitionId < 1)
                return 1;

            var competition = MCCompetition.Provider.Get(vote.CompetitionId);
            if (competition.IsVoteLocked)
                return 3;

            var item = MCVote.Provider.GetByEmail(vote.CompetitionId, vote.Email);
            if (item != null && item.Status != 1)
            {
                item.Delete();
                item = null;
            }

            if (item == null && !vote.Email.Contains(".."))
            {
                //var user = string.IsNullOrEmpty(vote.UserName) ? null : WebUser.Get(vote.UserName);
                //if (user != null && WSession.Current.UserId == user.Id)
                //{
                vote.DateVoted = DateTime.Now;
                vote.IPAddress = WHelper.GetUserHostAddress();

                //var isServiceAccount = user.IsServiceAccount();
                var candidate = MCCandidate.Provider.Get(vote.CandidateId);

                // Send Voting Notification
                var set = WebParameterSet.Get("ASOP Nomination");

                string emailTemplate = set.GetParameterValue("Vote-Email-Template");
                string emailSubjTemplate = set.GetParameterValue("Vote-Email-Subject");
                string smsTemplate = set.GetParameterValue("Vote-SMS-Template");
                //string cc = set.GetParameterValue("Vote-Alert-CC");

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

                //if (isServiceAccount)
                //{
                //    vote.Status = 0;
                //    vote.Code = ASOPHelper.GenerateCode(vote.DateVoted);
                //    vote.Update();

                //    var confirmUrl = set.GetParameterValue("Vote-Confirm-Url");

                //    var query = new QueryParser(confirmUrl);
                //    query.Set("Code", vote.Code);
                //    query.Set("Id", vote.Id);

                //    provider.Add("CONFIRM_LINK", query.BuildQuery());
                //}

                vote.Status = 1;
                vote.Update();

                var emailContent = Substituter.Substitute(emailTemplate, provider);
                var emailSubject = Substituter.Substitute(emailSubjTemplate, provider);
                var smsMessage = Substituter.Substitute(smsTemplate, provider);

                var sendVia = DataUtil.GetInt32(set.GetParameterValue("Send-Via", "2")); // Default is Both

                var message = WebMessageQueue.Create(emailContent, smsMessage, sendVia, vote.Email, emailSubject, null);
                message.AddTo(vote.MobileNumber); // cc, 
                message.Update();

                try
                {
                    AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(ex);
                }

                return 0; // Success
                //}
                //else
                //{
                //    return 1; // Error 
                //}
            }
            //else
            //{
            //    return 1; // Error
            //}

            //return 2; // Code does not exist. Invalid code.

            return 2; // Error
        }
    }
}
