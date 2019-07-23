using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.MusicCompetition
{
    public partial class AdminInterpreterScoreManager : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var competitions = MCCompetition.Provider.GetList();
                cboCompetition.DataSource = competitions;
                cboCompetition.DataBind();

                var competitionId = DataUtil.GetId(Request, "CompetitionId");
                if (competitionId > 0)
                    WebUtil.SetCboValue(cboCompetition, competitionId);

                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var id = DataUtil.GetId(e.CommandArgument);
            var query = new QueryParser(this);

            switch (e.CommandName)
            {
                case "Custom_Delete":
                    var item = MCInterpreterScore.Provider.Get(id);
                    if (item != null)
                        item.Delete();

                    GridView1.DataBind();
                    break;
            }
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GridView1.DataBind();
        }

        public DataSet Select(int competitionId, string keyword)
        {
            var kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
            var scores = MCInterpreterScore.Provider.GetList(competitionId);
            var candidates = MCCandidate.Provider.GetList(competitionId);

            WebUser judge = null;
            MCCandidate candidate = null;

            var result = from i in scores
                         select new
                         {
                             i.Id,
                             Judge = (judge = i.JudgeId > 0 ? WebUser.Get(i.JudgeId) : null) != null ? AccountHelper.GetPrefixedName(judge, false) : string.Empty,
                             Interpreter = (candidate = i.CandidateId > 0 ? candidates.FirstOrDefault(c => c.Id == i.CandidateId) : null) != null ? candidate.Interpreter : string.Empty,
                             VoiceQuality = i.VoiceQuality >= 0 ? i.VoiceQuality.ToString() : string.Empty,
                             Interpretation = i.Interpretation >= 0 ? i.Interpretation.ToString() : string.Empty,
                             StagePresence = i.StagePresence >= 0 ? i.StagePresence.ToString() : string.Empty,
                             OverallImpact = i.OverallImpact >= 0 ? i.OverallImpact.ToString() : string.Empty,
                             Total = (i.VoiceQuality >= 0 && i.Interpretation >= 0 && i.StagePresence >= 0 && i.OverallImpact >= 0) ?
                                         (i.VoiceQuality * 0.4 + i.Interpretation * 0.3 + i.StagePresence * 0.2 + i.OverallImpact * 0.1).ToString("N2") + "%" : string.Empty
                         };

            return DataUtil.ToDataSet(result);
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            var checkedValues = Request.Form.Get("chkChecked");
            if (!string.IsNullOrEmpty(checkedValues))
            {
                var ids = DataUtil.ParseCommaSeparatedIdList(checkedValues);
                if (ids.Count > 0)
                {
                    foreach (var id in ids)
                        MCInterpreterScore.Provider.Delete(id);

                    GridView1.DataBind();
                }
            }
        }

        protected void cboCompetition_SelectedIndexChanged(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            query.Set("CompetitionId", cboCompetition.SelectedValue);
            query.Redirect();
        }
    }
}