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
    public partial class AdminComposerEdit : System.Web.UI.UserControl
    {
        protected TextEditor txtDescription;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int competitionId = -1;
                var context = new WContext(this);

                var competitions = MCCompetition.Provider.GetList();
                cboCompetition.DataSource = competitions;
                cboCompetition.DataBind();

                var id = DataUtil.GetId(Request, "Id");
                var item = id > 0 ? MCComposer.Provider.Get(id) : null;

                if (item != null)
                {
                    txtName.Text = item.Name;
                    txtEntry.Text = item.Entry;
                    txtLocale.Text = item.Locale;
                    txtWork.Text = item.Work;
                    txtDescription.Text = item.Description;
                    txtPhotoFile.Text = item.PhotoFile;
                    txtNickName.Text = item.NickName;
                    chkActive.Checked = item.IsActive;

                    competitionId = item.CompetitionId;
                }
                else
                {
                    competitionId = context.GetId("CompetitionId");
                }

                if (competitionId > 0)
                    WebUtil.SetCboValue(cboCompetition, competitionId);
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ReturnPage();
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            var id = DataUtil.GetId(Request, "Id");
            var item = id > 0 ? MCComposer.Provider.Get(id) : new MCComposer();

            item.Name = txtName.Text.Trim();
            item.Entry = txtEntry.Text.Trim();
            item.Locale = txtLocale.Text.Trim();
            item.Work = txtWork.Text.Trim();
            item.Description = txtDescription.Text.Trim();
            item.PhotoFile = txtPhotoFile.Text.Trim();
            item.NickName = txtNickName.Text.Trim();
            item.CompetitionId = DataUtil.GetId(cboCompetition.SelectedValue);
            item.IsActive = chkActive.Checked;
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