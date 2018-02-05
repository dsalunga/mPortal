using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Net;
using WCMS.WebSystem.Agent;

namespace WCMS.WebSystem.Apps.Integration.Task
{
    public class MCVoteReminderTask : AgentTaskBase
    {
        public override void Execute()
        {
            Console.WriteLine("[{0}] {1} Execution started.", TaskName, DateTime.Now);

            var votes = MCVote.Provider.GetList();
            foreach (var vote in votes)
            {
                if (vote.Status == 0)
                {
                    try
                    {
                        var user = WebUser.Get(vote.UserName);
                        if (user != null)
                        {
                            //vote.DateVoted = DateTime.Now;

                            var isServiceAccount = user.IsServiceAccount();
                            var candidate = MCCandidate.Provider.Get(vote.CandidateId);

                            // Send Voting Notification
                            var set = WebParameterSet.Get("ASOP Nomination");

                            string emailTemplate = set.GetParameterValue(isServiceAccount ? "Vote-Email-Confirm-Template" : "Vote-Email-Template");
                            string emailSubjTemplate = set.GetParameterValue(isServiceAccount ? "Vote-Email-Confirm-Subject" : "Vote-Email-Subject");
                            string smsTemplate = set.GetParameterValue("Vote-SMS-Template");
                            string cc = set.GetParameterValue("Vote-Alert-CC");

                            var baseAddress = set.GetParameterValue("BaseAddress");
                            if (string.IsNullOrEmpty(baseAddress))
                                baseAddress = WConfig.BaseAddress;

                            NamedValueProvider provider = new NamedValueProvider();
                            provider.Add("BaseAddress", baseAddress);
                            provider.Add("FIRST_NAME", vote.FirstName);
                            provider.Add("LAST_NAME", vote.LastName);
                            provider.Add("CANDIDATE_NAME", candidate.Name);
                            provider.Add("CANDIDATE_ENTRY", candidate.Entry);
                            provider.Add("VOTING_CODE", vote.Code);
                            provider.Add("DATE_VOTED", vote.DateVoted);
                            provider.Add("EMAIL", vote.Email);

                            if (isServiceAccount)
                            {
                                //vote.Update();

                                string confirmUrl = set.GetParameterValue("Vote-Confirm-Url");

                                QueryParser query = new QueryParser(confirmUrl);
                                query.Set("Code", vote.DateVoted.Date.Ticks.ToString("X").ToLower());
                                query.Set("Id", vote.Id);

                                provider.Add("CONFIRM_LINK", query.BuildQuery());
                            }
                            else
                            {
                                vote.Status = 1;
                                vote.Update();
                            }

                            var emailContent = Substituter.Substitute(emailTemplate, provider);
                            var emailSubject = Substituter.Substitute(emailSubjTemplate, provider);
                            var smsMessage = Substituter.Substitute(smsTemplate, provider);

                            var sendVia = DataUtil.GetInt32(set.GetParameterValue("Send-Via", "2")); // Default is Both

                            WebMessageQueue message = WebMessageQueue.Create(emailContent, smsMessage, sendVia, vote.Email, emailSubject, null);
                            message.AddTo(cc, vote.MobileNumber);
                            message.Update();

                            //try
                            //{
                            //    AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
                            //}
                            //catch (Exception ex)
                            //{
                            //    LogHelper.WriteLog(ex);
                            //}

                            Console.WriteLine("[{0}] {1} Sent to: {2}", TaskName, DateTime.Now, vote.Email);

                            Thread.Sleep(500);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(ex);

                        Console.WriteLine("[{0}] {1} An error has encountered:", TaskName, DateTime.Now);
                        Console.WriteLine(ex);
                    }
                }
            }


            Console.WriteLine("[{0}] {1} Execution completed.", TaskName, DateTime.Now);
        }
    }
}
