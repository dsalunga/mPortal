using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.WebSystem.Controls;

using WCMS.Framework;

using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.MusicCompetition
{
    public partial class AdminComposerManagerView : System.Web.UI.UserControl
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
            int id = DataUtil.GetId(e.CommandArgument);
            var query = new WQuery(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    var competitionId = DataUtil.GetId(cboCompetition.SelectedValue);
                    if (competitionId > 0)
                        query.Set("CompetitionId", competitionId);

                    query.Set(WebColumns.Id, id);
                    query.LoadAndRedirect("AdminComposerEdit.ascx");
                    break;

                case "Custom_Delete":
                    var item = MCComposer.Provider.Get(id);
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
            string kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
            MCCompetition competition = null;

            return DataUtil.ToDataSet(
                from item in MCComposer.Provider.GetList(competitionId)
                select new
                {
                    item.Id,
                    item.Name,
                    item.Entry,
                    item.Locale,
                    item.Work,
                    item.PhotoFile,
                    item.NickName,
                    IsActive = item.IsActive ? "Yes" : string.Empty,
                    Competition = (competition = item.CompetitionId > 0 ? MCCompetition.Provider.Get(item.CompetitionId) : null) != null ? competition.Name : string.Empty
                });
        }

        protected void cmdNew_Click(object sender, EventArgs e)
        {
            var competitionId = DataUtil.GetId(cboCompetition.SelectedValue);
            var query = new WQuery(this);

            if (competitionId > 0)
                query.Set("CompetitionId", competitionId);

            query.LoadAndRedirect("AdminComposerEdit.ascx");
        }

        protected void cboCompetition_SelectedIndexChanged(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            query.Set("CompetitionId", cboCompetition.SelectedValue);
            query.Redirect();
        }
    }
}