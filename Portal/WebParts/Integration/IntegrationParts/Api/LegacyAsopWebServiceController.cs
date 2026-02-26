using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Net;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Agent;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible replacement for MusicCompetition ASOP-WS.asmx.
    /// Returns ASMX script-service payload shape: { d: ... }.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyAsopWebServiceController : ControllerBase
    {
        [HttpGet("/Content/Parts/MusicCompetition/ASOP-WS.asmx/HelloWorld")]
        [HttpPost("/Content/Parts/MusicCompetition/ASOP-WS.asmx/HelloWorld")]
        [HttpGet("/Content/Parts/Integration/MusicCompetition/ASOP-WS.asmx/HelloWorld")]
        [HttpPost("/Content/Parts/Integration/MusicCompetition/ASOP-WS.asmx/HelloWorld")]
        public IActionResult HelloWorld()
        {
            return new JsonResult(new { d = "Hello World" });
        }

        [HttpPost("/Content/Parts/MusicCompetition/ASOP-WS.asmx/GetCandidate")]
        [HttpPost("/Content/Parts/Integration/MusicCompetition/ASOP-WS.asmx/GetCandidate")]
        public IActionResult GetCandidate([FromBody] CandidateRequest request)
        {
            var candidate = request != null && request.Id > 0
                ? MCCandidate.Provider.Get(request.Id)
                : null;

            return new JsonResult(new { d = candidate });
        }

        [HttpPost("/Content/Parts/MusicCompetition/ASOP-WS.asmx/IsEmailNotTaken")]
        [HttpPost("/Content/Parts/Integration/MusicCompetition/ASOP-WS.asmx/IsEmailNotTaken")]
        public IActionResult IsEmailNotTaken([FromBody] EmailCheckRequest request)
        {
            var competitionId = request?.CompetitionId ?? 0;
            var email = request?.Email ?? string.Empty;
            var available = MCVote.Provider.GetByEmail(competitionId, email, 1) == null;

            return new JsonResult(new { d = available });
        }

        [HttpPost("/Content/Parts/MusicCompetition/ASOP-WS.asmx/Vote")]
        [HttpPost("/Content/Parts/Integration/MusicCompetition/ASOP-WS.asmx/Vote")]
        public IActionResult Vote([FromBody] VoteRequest request)
        {
            var resultCode = VoteCore(request?.Vote);
            return new JsonResult(new { d = resultCode });
        }

        private static int VoteCore(MCVote vote)
        {
            if (vote == null || vote.CompetitionId < 1)
                return 1;

            var competition = MCCompetition.Provider.Get(vote.CompetitionId);
            if (competition == null)
                return 1;

            if (competition.IsVoteLocked)
                return 3;

            var existing = MCVote.Provider.GetByEmail(vote.CompetitionId, vote.Email);
            if (existing != null && existing.Status != 1)
            {
                existing.Delete();
                existing = null;
            }

            if (existing == null && !string.IsNullOrEmpty(vote.Email) && !vote.Email.Contains(".."))
            {
                vote.DateVoted = DateTime.Now;
                vote.IPAddress = WHelper.GetUserHostAddress();
                vote.Status = 1;
                vote.Update();

                var candidate = MCCandidate.Provider.Get(vote.CandidateId);
                var set = WebParameterSet.Get("ASOP Nomination");
                if (candidate != null && set != null)
                {
                    var emailTemplate = set.GetParameterValue("Vote-Email-Template");
                    var emailSubjTemplate = set.GetParameterValue("Vote-Email-Subject");
                    var smsTemplate = set.GetParameterValue("Vote-SMS-Template");

                    var baseAddress = set.GetParameterValue("BaseAddress");
                    if (string.IsNullOrEmpty(baseAddress))
                        baseAddress = WConfig.BaseAddress;

                    var values = new NamedValueProvider();
                    values.Add("BaseAddress", baseAddress);
                    values.Add("FIRST_NAME", vote.FirstName);
                    values.Add("LAST_NAME", vote.LastName);
                    values.Add("CANDIDATE_NAME", candidate.Name);
                    values.Add("CANDIDATE_ENTRY", candidate.Entry);
                    values.Add("VOTING_CODE", vote.Code);
                    values.Add("DATE_VOTED", vote.DateVoted);
                    values.Add("EMAIL", vote.Email);

                    var emailContent = Substituter.Substitute(emailTemplate, values);
                    var emailSubject = Substituter.Substitute(emailSubjTemplate, values);
                    var smsMessage = Substituter.Substitute(smsTemplate, values);

                    var sendVia = DataUtil.GetInt32(set.GetParameterValue("Send-Via", "2"));
                    var message = WebMessageQueue.Create(emailContent, smsMessage, sendVia, vote.Email, emailSubject, null);
                    message.AddTo(vote.MobileNumber);
                    message.Update();

                    try
                    {
                        AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(ex);
                    }
                }

                return 0;
            }

            return 2;
        }
    }

    public class CandidateRequest
    {
        public int Id { get; set; }
    }

    public class EmailCheckRequest
    {
        public int CompetitionId { get; set; }
        public string Email { get; set; }
    }

    public class VoteRequest
    {
        public MCVote Vote { get; set; }
    }
}
