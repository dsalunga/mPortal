using System;
using System.Collections.Generic;
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
    public partial class CandidateEdit : System.Web.UI.UserControl
    {
        protected TextEditor txtLyrics;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int competitionId = -1;
                var context = new WContext(this);

                var competitions = MCCompetition.Provider.GetList();
                cboCompetition.DataSource = competitions;
                cboCompetition.DataBind();

                var id = DataUtil.GetId(Request, "Id");
                var item = id > 0 ? MCCandidate.Provider.Get(id) : null;
                if (item != null)
                {
                    txtName.Text = item.Name;
                    txtEntry.Text = item.Entry;
                    txtSourceUrl.Text = item.SourceUrl;
                    txtSourceUrl2.Text = item.SourceUrl2;
                    txtLyrics.Text = item.Lyrics;
                    txtLyricist.Text = item.Lyricist;
                    txtInterpreter.Text = item.Interpreter;
                    txtPhotoFile.Text = item.PhotoFile;
                    txtRank.Text = item.Rank.ToString();
                    txtWinnerRank.Text = item.WinnerRank.ToString();

                    competitionId = item.CompetitionId;
                }
                else
                {
                    competitionId = context.GetId("CompetitionId");
                }

                if (competitionId > 0)
                {
                    WebHelper.SetCboValue(cboCompetition, competitionId);
                    hCompetitionId.Value = competitionId.ToString();
                }

                var query = context.Query;
                query.Remove("Id");
                query.Remove(WConstants.Load);

                linkCancel.HRef = query.BuildQuery();
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ReturnPage();
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            var id = DataUtil.GetId(Request, "Id");
            var item = id > 0 ? MCCandidate.Provider.Get(id) : new MCCandidate();

            item.Name = txtName.Text.Trim();
            item.Entry = txtEntry.Text.Trim();
            item.Lyrics = txtLyrics.Text.Trim();
            item.SourceUrl = txtSourceUrl.Text.Trim();
            item.SourceUrl2 = txtSourceUrl2.Text.Trim();
            item.Lyricist = txtLyricist.Text.Trim();
            item.Interpreter = txtInterpreter.Text.Trim();
            item.PhotoFile = txtPhotoFile.Text.Trim();
            item.Rank = DataUtil.GetInt32(txtRank.Text.Trim(), 0);
            item.WinnerRank = DataUtil.GetInt32(txtWinnerRank.Text.Trim(), 0);
            item.CompetitionId = DataUtil.GetId(cboCompetition.SelectedValue);
            item.Update();

            this.ReturnPage();
        }

        private void ReturnPage()
        {
            var query = new WQuery(this);
            query.Remove("Id");
            query.Remove(WConstants.Load);
            query.Redirect();
        }
    }
}