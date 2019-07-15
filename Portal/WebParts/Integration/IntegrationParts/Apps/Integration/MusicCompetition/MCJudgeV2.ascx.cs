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
    public partial class MCJudgeV2 : System.Web.UI.UserControl
    {
        public string View { get; set; }
        public string Action { get; set; }
        public string JudgeName { get; set; }
        public string CompetitionName { get; set; }
        public string FinalistUrl { get; set; }

        public bool IsEdit { get; set; }
        public bool IsLocked { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var query = context.Query;
            var element = context.Element;
            var set = context.GetParameterSet();

            Action = query.Get("Action", "");
            View = query.Get("View", "");
            FinalistUrl = ParameterizedWebObject.GetValue("FinalistUrl", "#", element, set);

            var competitionId = DataUtil.GetId(ParameterizedWebObject.GetValue("CompetitionId", element, set));
            if (competitionId == -1)
                throw new Exception("CompetitionId is required.");

            var user = WSession.Current.User;
            JudgeName = user == null ? "Bro. Anonymous" : AccountHelper.GetPrefixedName(user, NamePrefixes.Brotherhood);

            var item = MCCompetition.Provider.Get(competitionId);
            CompetitionName = item.Name;
            IsLocked = item.IsScoreLocked; //GetLockStatus();
            IsEdit = Action.Equals("Edit", StringComparison.InvariantCultureIgnoreCase);

            if (IsLocked && !string.IsNullOrEmpty(Action))
            {
                query.Remove("Action");
                query.Redirect();
            }

            if (!Page.IsPostBack)
            {
                formMain.Action = query.BuildQuery();

                hCompetitionId.Value = competitionId.ToString();

                Repeater1.DataBind();
                Repeater2.DataBind();
                Repeater3.DataBind();
                Repeater4.DataBind();

                var q = query.Clone();

                //q.Remove("Action");
                q.Remove("View");
                linkSong.HRef = q.BuildQuery();

                q.Set("View", "Interpreter");
                linkInterpreter.HRef = q.BuildQuery();


                q = query.Clone();

                if (View.Equals("Interpreter", StringComparison.InvariantCultureIgnoreCase))
                {
                    MultiViewMain.SetActiveView(viewInterpreter);
                    if (Action.Equals("Edit", StringComparison.InvariantCultureIgnoreCase))
                    {
                        MultiViewInterpreter.SetActiveView(viewInterpreterEdit);
                        linkSong.Visible = false;
                    }
                    else
                    {
                        MultiViewInterpreter.SetActiveView(viewInterpreterShow);
                        linkSong.Visible = true;
                    }
                }
                else
                {
                    MultiViewMain.SetActiveView(viewSong);
                    if (Action.Equals("Edit", StringComparison.InvariantCultureIgnoreCase))
                    {
                        MultiViewSong.SetActiveView(viewSongEdit);
                        linkInterpreter.Visible = false;
                    }
                    else
                    {
                        MultiViewSong.SetActiveView(viewSongShow);
                        linkInterpreter.Visible = true;
                    }
                }


                if (string.IsNullOrEmpty(Action))
                {
                    // View Mode
                    ToggleButtons(false);

                    q.Set("Action", "Edit");
                    linkEdit.HRef = q.BuildQuery();
                    linkEdit2.HRef = q.BuildQuery();

                    q.Remove("Action");
                }
                else if (Action.Equals("Edit", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Edit Mode
                    ToggleButtons(true);

                    q.Remove("Action");

                    linkCancel.HRef = q.BuildQuery();
                    linkCancel2.HRef = q.BuildQuery();
                }
                else
                {
                    q.Remove("Action");
                    q.Redirect();
                }

                if (string.IsNullOrEmpty(View))
                {
                    // Song
                }
            }
        }

        //private bool GetLockStatus()
        //{
        //    var id = DataHelper.GetId(hCompetitionId.Value);
        //    var item = MCCompetition.Provider.Get(id);

        //    // Check Lock
        //    return item.IsScoreLocked;
        //}

        private void ToggleButtons(bool isEditMode)
        {
            linkCancel.Visible = isEditMode;
            cmdUpdate.Visible = isEditMode;

            //linkRefresh.Visible = !isEditMode;
            linkEdit.Visible = !isEditMode;

            linkCancel2.Visible = isEditMode;
            cmdUpdate2.Visible = isEditMode;

            //linkRefresh2.Visible = !isEditMode;
            linkEdit2.Visible = !isEditMode;
        }

        public DataSet GetSongScores(int competitionId)
        {
            var userId = WSession.Current.UserId;
            IEnumerable<MCSongScore> scores = userId > 0 ? MCSongScore.Provider.GetList(competitionId, -2, userId) : new List<MCSongScore>();
            MCSongScore score = null;
            var candidates = MCCandidate.Provider.GetList(competitionId).OrderBy(i=>i.Rank);
            int index = 1;

            var result = from i in candidates
                         orderby i.Rank, i.Entry
                         select new
                         {
                             i.Id,
                             i.Entry,
                             i.Name,
                             i.Interpreter,
                             i.PhotoFile,
                             EvalPhotoFile = i.GetPhotoFile(),
                             Index = index++,
                             Score = (score = scores.FirstOrDefault(s => s.CandidateId == i.Id)) != null ? score : (score = new MCSongScore()),
                             Musicality = score.Musicality >= 0 ? score.Musicality.ToString() : string.Empty,
                             LyricsMessage = score.LyricsMessage >= 0 ? score.LyricsMessage.ToString() : string.Empty,
                             OverallImpact = score.OverallImpact >= 0 ? score.OverallImpact.ToString() : string.Empty,
                             Total = (score.Musicality >= 0 && score.LyricsMessage >= 0 && score.OverallImpact >= 0) ?
                                (score.Musicality * 0.3 + score.LyricsMessage * 0.4 + score.OverallImpact * 0.3).ToString("N2") + "%" : string.Empty
                         };

            return DataHelper.ToDataSet(result);
        }

        public DataSet GetInterpreterScores(int competitionId)
        {
            var userId = WSession.Current.UserId;
            IEnumerable<MCInterpreterScore> scores = userId > 0 ? MCInterpreterScore.Provider.GetList(competitionId, -2, userId) : new List<MCInterpreterScore>();
            MCInterpreterScore score = null;
            var candidates = MCCandidate.Provider.GetList(competitionId);
            int index = 1;

            return DataHelper.ToDataSet(
                from i in candidates
                orderby i.Rank, i.Entry
                select new
                {
                    i.Id,
                    i.Entry,
                    i.Name,
                    i.Interpreter,
                    i.PhotoFile,
                    EvalPhotoFile = i.GetPhotoFile(),
                    Index = index++,
                    Score = (score = scores.FirstOrDefault(s => s.CandidateId == i.Id)) != null ? score : (score = new MCInterpreterScore()),
                    VoiceQuality = score.VoiceQuality >= 0 ? score.VoiceQuality.ToString() : string.Empty,
                    Interpretation = score.Interpretation >= 0 ? score.Interpretation.ToString() : string.Empty,
                    StagePresence = score.StagePresence >= 0 ? score.StagePresence.ToString() : string.Empty,
                    OverallImpact = score.OverallImpact >= 0 ? score.OverallImpact.ToString() : string.Empty,
                    Total = (score.VoiceQuality >= 0 && score.Interpretation >= 0 && score.StagePresence >= 0 && score.OverallImpact >= 0) ?
                                (score.VoiceQuality * 0.4 + score.Interpretation * 0.3 + score.StagePresence * 0.2 + score.OverallImpact * 0.1).ToString("N2") + "%" : string.Empty
                }
            );
        }

        protected void cmdUpdate_ServerClick(object sender, EventArgs e)
        {
            var query = new QueryParser(this);

            var competitionId = DataUtil.GetId(hCompetitionId.Value);
            var userId = WSession.Current.UserId;
            if (userId > 0 && !IsLocked && competitionId > 0)
            {
                var candidates = MCCandidate.Provider.GetList(competitionId);
                if (View.Equals("Interpreter", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Update Interpreter Scores
                    var scores = MCInterpreterScore.Provider.GetList(competitionId, -2, userId);
                    foreach (var candidate in candidates)
                    {
                        var score = scores.FirstOrDefault(i => i.CandidateId == candidate.Id);
                        if (score == null)
                            score = new MCInterpreterScore
                            {
                                CandidateId = candidate.Id,
                                DateModified = DateTime.Now,
                                JudgeId = userId,
                                CompetitionId = competitionId
                            };

                        score.VoiceQuality = ProcessValue(Request.Form.Get("__txt_vq_" + candidate.Id));
                        score.Interpretation = ProcessValue(Request.Form.Get("__txt_in_" + candidate.Id));
                        score.StagePresence = ProcessValue(Request.Form.Get("__txt_sp_" + candidate.Id));
                        score.OverallImpact = ProcessValue(Request.Form.Get("__txt_oi_" + candidate.Id));

                        score.Update();
                    }
                }
                else
                {
                    // Update Song Scores
                    var scores = MCSongScore.Provider.GetList(competitionId, -2, userId);
                    foreach (var candidate in candidates)
                    {
                        var score = scores.FirstOrDefault(i => i.CandidateId == candidate.Id);
                        if (score == null)
                            score = new MCSongScore
                            {
                                CandidateId = candidate.Id,
                                DateModified = DateTime.Now,
                                JudgeId = userId,
                                CompetitionId = competitionId
                            };

                        score.Musicality = ProcessValue(Request.Form.Get("__txt_mu_" + candidate.Id));
                        score.LyricsMessage = ProcessValue(Request.Form.Get("__txt_lm_" + candidate.Id));
                        score.OverallImpact = ProcessValue(Request.Form.Get("__txt_oi_" + candidate.Id));

                        score.Update();
                    }
                }
            }

            query.Remove("Action");
            query.Redirect();
        }

        private int ProcessValue(string stringValue)
        {
            int value = !string.IsNullOrEmpty(stringValue) ? DataUtil.GetInt32(stringValue.TrimEnd('%'), -1) : -1;

            if (value > 100)
                value = 100;

            if (value < -1)
                value = -1;

            return value;
        }
    }
}