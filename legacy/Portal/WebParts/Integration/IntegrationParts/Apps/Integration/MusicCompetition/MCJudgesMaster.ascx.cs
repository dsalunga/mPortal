using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.MusicCompetition
{
    public partial class MCJudgesMaster : System.Web.UI.UserControl
    {
        public string View { get; set; }
        public string Action { get; set; }
        public string JudgeName { get; set; }
        public string CompetitionName { get; set; }
        public int CompetitionId { get; set; }

        public bool IsLocked { get; set; }
        public bool VotesLocked { get; set; }
        public bool VotesMasked { get; set; }

        public IEnumerable<WebUser> Judges { get; set; }

        public MCJudgesMaster()
        {
            CompetitionId = -1;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var element = context.Element;
            var query = context.Query;

            View = query.Get("View", "");

            var user = WSession.Current.User;
            JudgeName = user == null ? "Mr. Synthetic294de3" : AccountHelper.GetPrefixedName(user, NamePrefixes.Salutation);

            // Check Lock
            var set = context.GetParameterSet();
            //if (set == null)
            //    throw new Exception("ParameterSet is required.");

            CompetitionId = DataUtil.GetId(ParameterizedWebObject.GetValue("CompetitionId", element, set));
            var item = MCCompetition.Provider.Get(CompetitionId);

            CompetitionName = item.Name;
            IsLocked = item.IsScoreLocked; //DataHelper.GetBool(ParameterizedWebObject.GetValue(MCConstants.SCORE_LOCK, item), false);
            VotesLocked = item.IsVoteLocked;
            VotesMasked = item.IsVoteMasked;

            hCompetitionId.Value = CompetitionId.ToString();
            cmdLockUnlock.Attributes["class"] = IsLocked ? "btn btn-danger" : "btn btn-success";
            cmdLockUnlockVotes.Attributes["class"] = VotesLocked ? "btn btn-danger" : "btn btn-success";
            cmdMaskUnmaskVotes.Attributes["class"] = VotesMasked ? "btn btn-danger" : "btn btn-success";

            //var judgesGroupPath = ParameterizedWebObject.GetValue("Judges", item, element, set);
            if (!string.IsNullOrEmpty(item.Judges))
                Judges = AccountHelper.CollectUsersHierarchal(item.Judges); //WebGroup.SelectNode(judgesGroupPath).Users;
            else
                throw new Exception("Judges parameter is required.");

            if (!Page.IsPostBack)
            {
                formMain.Action = query.BuildQuery();
            }
        }

        public DataSet GetSongScores(int competitionId)
        {
            var userId = WSession.Current.UserId;
            var scores = userId > 0 ? MCSongScore.Provider.GetList(competitionId, -2, userId) : new List<MCSongScore>();
            MCSongScore score = null;
            var candidates = MCCandidate.Provider.GetList();

            var result = from i in candidates
                         orderby i.Entry
                         select new
                         {
                             i.Id,
                             i.Entry,
                             i.Name,
                             i.Interpreter,
                             i.PhotoFile,
                             Score = (score = scores.FirstOrDefault(s => s.CandidateId == i.Id)) != null ? score : (score = new MCSongScore()),
                             Total = (score.Musicality >= 0 && score.LyricsMessage >= 0 && score.OverallImpact >= 0) ?
                                (score.Musicality * 0.3 + score.LyricsMessage * 0.4 + score.OverallImpact * 0.3).ToString("N2") + "%" : string.Empty
                         };

            return DataUtil.ToDataSet(result);
        }

        public DataSet GetInterpreterScores(int competitionId)
        {
            var userId = WSession.Current.UserId;
            IEnumerable<MCInterpreterScore> scores = userId > 0 ? MCInterpreterScore.Provider.GetList(competitionId, -2, userId) : new List<MCInterpreterScore>();
            MCInterpreterScore score = null;
            var candidates = MCCandidate.Provider.GetList();

            return DataUtil.ToDataSet(
                from i in candidates
                orderby i.Entry
                select new
                {
                    i.Id,
                    i.Entry,
                    i.Name,
                    i.Interpreter,
                    i.PhotoFile,
                    Score = (score = scores.FirstOrDefault(s => s.CandidateId == i.Id)) != null ? score : (score = new MCInterpreterScore()),
                    Total = (score.VoiceQuality >= 0 && score.Interpretation >= 0 && score.StagePresence >= 0 && score.OverallImpact >= 0) ?
                                (score.VoiceQuality * 0.4 + score.Interpretation * 0.3 + score.StagePresence * 0.2 + score.OverallImpact * 0.1).ToString("N2") + "%" : string.Empty
                }
            );
        }

        public string ComputePercentage(int total, int count)
        {
            double per = 0;
            if (count > 0)
                per = (count / (double)total) * 100;

            return per.ToString("0.00") + "%";
        }

        protected void cmdLockUnlock_ServerClick(object sender, EventArgs e)
        {
            var context = new WContext(this);
            
            var competitionId = DataUtil.GetId(hCompetitionId.Value);
            var item = MCCompetition.Provider.Get(competitionId);

            //var p = ParameterizedWebObject.GetParameter(MCConstants.SCORE_LOCK, item);
            //if (p == null)
            //    p = item.CreateParameter(MCConstants.SCORE_LOCK);

            //var isLocked = DataHelper.GetBool(p.Value, false);
            //p.Value = isLocked ? "0" : "1";
            //p.Update();

            item.IsScoreLocked = !item.IsScoreLocked;
            item.Update();

            context.Redirect();
        }

        protected void cmdLockUnlockVotes_ServerClick(object sender, EventArgs e)
        {
            var context = new WContext(this);

            var competitionId = DataUtil.GetId(hCompetitionId.Value);
            var item = MCCompetition.Provider.Get(competitionId);

            //var p = ParameterizedWebObject.GetParameter(MCConstants.SCORE_LOCK, item);
            //if (p == null)
            //    p = item.CreateParameter(MCConstants.SCORE_LOCK);

            //var isLocked = DataHelper.GetBool(p.Value, false);
            //p.Value = isLocked ? "0" : "1";
            //p.Update();

            item.IsVoteLocked = !item.IsVoteLocked;
            item.Update();

            context.Redirect();
        }

        protected void cmdMaskUnmaskVotes_ServerClick(object sender, EventArgs e)
        {
            var context = new WContext(this);

            var competitionId = DataUtil.GetId(hCompetitionId.Value);
            var item = MCCompetition.Provider.Get(competitionId);

            //var p = ParameterizedWebObject.GetParameter(MCConstants.SCORE_LOCK, item);
            //if (p == null)
            //    p = item.CreateParameter(MCConstants.SCORE_LOCK);

            //var isLocked = DataHelper.GetBool(p.Value, false);
            //p.Value = isLocked ? "0" : "1";
            //p.Update();

            item.IsVoteMasked = !item.IsVoteMasked;
            item.Update();

            context.Redirect();
        }
    }
}