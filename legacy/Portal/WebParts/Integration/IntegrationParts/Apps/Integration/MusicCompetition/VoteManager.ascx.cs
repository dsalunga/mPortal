using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.Controls;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.MusicCompetition
{
    public partial class VoteManagerView : System.Web.UI.UserControl
    {
        private const string TAB_VOTES = "tabVotes";
        private const string TAB_CODES = "tabCodes";

        protected TabControl TabControl1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TabControl1.AddTab(TAB_VOTES, "Member Votes");
                //TabControl1.AddTab(TAB_CODES, "Voting Codes");

                var competitions = MCCompetition.Provider.GetList();
                cboCompetition.DataSource = competitions;
                cboCompetition.DataBind();

                var competitionId = DataUtil.GetId(Request, "CompetitionId");
                if (competitionId > 0)
                    WebUtil.SetCboValue(cboCompetition, competitionId);

                GridView1.DataBind();
            }
        }

        protected void TabControl1_SelectedTabChanged(object oSender, TabEventArgs args)
        {
            switch (args.TabName)
            {
                case TAB_VOTES:
                    MultiView1.SetActiveView(viewVotes);
                    GridView1.DataBind();
                    break;

                //case TAB_CODES:
                //    MultiView1.SetActiveView(viewCodes);
                //    GridView2.DataBind();
                //    break;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataUtil.GetId(e.CommandArgument);
            QueryParser query = new QueryParser(this);

            switch (e.CommandName)
            {
                case "Custom_Delete":
                    var item = MCVote.Provider.Get(id);
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

        public DataSet SelectVotes(int competitionId, string keyword)
        {
            string kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
            var candidates = MCCandidate.Provider.GetList(competitionId);
            MCCandidate candidate = null;

            return DataUtil.ToDataSet(
                from i in MCVote.Provider.GetList(competitionId)
                where i.CandidateId > 0
                    && (string.IsNullOrEmpty(kwl) ||
                       i.Email.ToLower().Contains(kwl) ||
                       i.FirstName.ToLower().Contains(kwl) ||
                       i.LastName.ToLower().Contains(kwl) ||
                       i.UserName.ToLower().Contains(kwl))
                orderby i.DateVoted descending
                select new
                {
                    i.Id,
                    i.FirstName,
                    i.LastName,
                    i.Code,
                    i.MobileNumber,
                    i.Email,
                    i.DateVoted,
                    i.UserName,
                    CandidateSong = (candidate = candidates.FirstOrDefault(c => c.Id == i.CandidateId)) != null ? candidate.Entry : "",
                    CandidateComposer = candidate != null ? candidate.Name : "",
                    Confirmed = i.Status == 1 ? "Yes" : "",
                    i.IPAddress
                });
        }

        //public DataSet SelectCodes(int competitionId, string keyword)
        //{
        //    string kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();

        //    return DataHelper.ToDataSet(
        //        from item in MCVote.Provider.GetList(competitionId)
        //        where item.CandidateId <= 0
        //        select new
        //        {
        //            item.Id,
        //            item.Code
        //        });
        //}

        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            var competitionId = DataUtil.GetId(cboCompetition.SelectedValue);
            var candidates = MCCandidate.Provider.GetList();
            MCCandidate candidate = null;

            var ds = DataUtil.ToDataSet(
                from item in MCVote.Provider.GetList(competitionId)
                where item.CandidateId > 0
                select new
                {
                    item.Id,
                    item.FirstName,
                    item.LastName,
                    item.Code,
                    item.MobileNumber,
                    item.Email,
                    item.DateVoted,
                    item.UserName,
                    CandidateSong = (candidate = candidates.FirstOrDefault(i => i.Id == item.CandidateId)) != null ? candidate.Entry : "",
                    CandidateComposer = candidate != null ? candidate.Name : "",
                    Confirmed = item.Status == 1 ? "Yes" : "",
                    item.IPAddress,
                    item.Spam
                });

            WebUtil.DownloadAsCsv(ds, "ASOP-Votes");
        }

        //protected void cmdDownloadCodes_Click(object sender, EventArgs e)
        //{
        //    var ds = DataHelper.ToDataSet(
        //        from item in MCVote.Provider.GetList()
        //        where item.CandidateId <= 0
        //        select new
        //        {
        //            item.Id,
        //            item.Code
        //        });

        //    WebHelper.DownloadAsCsv(ds, "ASOP-Codes");
        //}

        //protected void cmdGenerate_Click(object sender, EventArgs e)
        //{
        //    int count = DataHelper.GetInt32(cboCodes.SelectedValue);
        //    var now = DateTime.Now;

        //    for (int i = 0; i < count; i++)
        //    {
        //        string dateCode = string.Format("{0:D2}{1:D2}{2:D2}", Convert.ToInt32(now.ToString("yy")), now.Month, now.Day);
        //        string timeCode = string.Format("{0:D2}{1:D2}{2:D2}", now.Hour, now.Minute, now.Second);
        //        string countCode = string.Format("{0:X13}", Convert.ToInt64(i + dateCode + timeCode));

        //        //string code = string.Format("{0}{1}{2}", DataHelper.ReverseString(dateCode), countCode, DataHelper.ReverseString(timeCode));
        //        var code = string.Format("ASOP-{1}-{0}", countCode.Substring(0, 6), countCode.Substring(6));

        //        MCVote item = new MCVote();
        //        item.Code = code;
        //        item.Update();
        //    }

        //    GridView2.DataBind();
        //}

        protected void cmdHardReset_Click(object sender, EventArgs e)
        {
            //SqlHelper.ExecuteNonQuery(CommandType.Text, "TRUNCATE TABLE MCVote");

            var competitionId = DataUtil.GetId(cboCompetition.SelectedValue);
            var items = MCVote.Provider.GetList(competitionId);
            foreach (var item in items)
                item.Delete();

            GridView1.DataBind();
            //GridView2.DataBind();
        }

        protected void cboCompetition_SelectedIndexChanged(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            query.Set("CompetitionId", cboCompetition.SelectedValue);
            query.Redirect();
        }

    }
}