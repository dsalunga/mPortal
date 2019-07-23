using System;
using System.IO;
using System.Linq;
using System.Web.UI;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.Controls;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.MusicCompetition
{
    public partial class AdminCompetitionEdit : System.Web.UI.UserControl
    {
        protected TextEditor txtDescription;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var context = new WContext(this);

                var id = DataUtil.GetId(Request, "Id");
                if (id > 0)
                {
                    var item = MCCompetition.Provider.Get(id);
                    if (item != null)
                    {
                        txtName.Text = item.Name;
                        txtJudges.Text = item.Judges;
                        txtDate.Text = item.CompetitionDate.ToString();
                        chkLocked.Checked = item.IsScoreLocked;
                        chkVotingLocked.Checked = item.IsVoteLocked;

                        var candidates = MCCandidate.Provider.GetList(item.Id);
                        cboInterpreters.DataSource = from i in candidates
                                                     select new
                                                     {
                                                         i.Id,
                                                         Name = string.Format("{0}, {1}", i.Interpreter, i.Entry)
                                                     };
                        cboInterpreters.DataBind();

                        if (item.BestInterpreterId > 0)
                            WebUtil.SetCboValue(cboInterpreters, item.BestInterpreterId);

                        cboPeoplesChoice.DataSource = from i in candidates
                                                      select new
                                                      {
                                                          i.Id,
                                                          Name = i.Entry
                                                      };
                        cboPeoplesChoice.DataBind();

                        if (item.PeoplesChoiceId > 0)
                            WebUtil.SetCboValue(cboPeoplesChoice, item.PeoplesChoiceId);

                        panelBestInterpreter.Visible = true;
                        panelPeoplesChoice.Visible = true;
                    }
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ReturnPage();
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            var id = DataUtil.GetId(Request, "Id");
            var item = id > 0 ? MCCompetition.Provider.Get(id) : new MCCompetition();

            item.Name = txtName.Text.Trim();
            item.Judges = txtJudges.Text.Trim();
            item.CompetitionDate = DataUtil.GetDateTime(txtDate.Text.Trim());
            item.IsScoreLocked = chkLocked.Checked;
            item.IsVoteLocked = chkVotingLocked.Checked;
            item.BestInterpreterId = DataUtil.GetId(cboInterpreters.SelectedValue);
            item.PeoplesChoiceId = DataUtil.GetId(cboPeoplesChoice.SelectedValue);
            item.Update();

            var MCBasePath = IntegrationConstants.MCBasePath;
            var asopAbsPath = MapPath(MCBasePath);
            var albumAbsPath = FileHelper.Combine(asopAbsPath, item.Id.ToString());
            var photosAbsPath = FileHelper.Combine(albumAbsPath, "Photos");
            var photoAbsThumbPath = FileHelper.Combine(photosAbsPath, "thumb");
            var musicAbsPath = FileHelper.Combine(albumAbsPath, "Music");

            if (!Directory.Exists(photosAbsPath))
                Directory.CreateDirectory(photosAbsPath);

            if (!Directory.Exists(photoAbsThumbPath))
                Directory.CreateDirectory(photoAbsThumbPath);

            if (!Directory.Exists(musicAbsPath))
                Directory.CreateDirectory(musicAbsPath);

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