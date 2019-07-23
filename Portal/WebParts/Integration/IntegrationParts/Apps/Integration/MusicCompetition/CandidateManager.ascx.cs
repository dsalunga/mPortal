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
using WCMS.WebSystem.Apps.Integration;
using System.IO;

namespace WCMS.WebSystem.Apps.MusicCompetition
{
    public partial class CandidateManagerView : System.Web.UI.UserControl
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

                SetNewLink(competitionId);
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
                    query.LoadAndRedirect("CandidateEdit.ascx");
                    break;

                case "Custom_Delete":
                    var item = MCCandidate.Provider.Get(id);
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

            var query = new WQuery(true);
            query.Set("CompetitionId", competitionId);
            query.Set(WConstants.Load, "CandidateEdit.ascx");

            return DataUtil.ToDataSet(
                from item in MCCandidate.Provider.GetList(competitionId)
                select new
                {
                    item.Id,
                    item.Name,
                    item.Entry,
                    item.SourceUrl,
                    item.SourceUrl2,
                    item.Interpreter,
                    item.Lyricist,
                    item.Rank,
                    ComposerLyricist = string.IsNullOrEmpty(item.Lyricist)
                        || item.Lyricist.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase)
                        ? item.Name : string.Format("{0} / {1}", item.Name, item.Lyricist),
                    item.PhotoFile,
                    Competition = (competition = item.CompetitionId > 0 ? MCCompetition.Provider.Get(item.CompetitionId) : null) != null ? competition.Name : string.Empty,
                    EntryUrl = query.Set(WebColumns.Id, item.Id).BuildQuery()
                });
        }

        private void SetNewLink(int competitionId)
        {
            competitionId = DataUtil.GetId(cboCompetition.SelectedValue);
            var query = new WQuery(this);

            if (competitionId > 0)
                query.Set("CompetitionId", competitionId);

            query.Set(WConstants.Load, "CandidateEdit.ascx");
            linkNew.HRef = query.BuildQuery();
        }

        protected void cboCompetition_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SetNewLink(DataHelper.GetId(cboCompetition.SelectedValue));
            var query = new WQuery(this);
            var competitionId = DataUtil.GetId(cboCompetition.SelectedValue);

            var copyMode = hCopyMode.Value == "1";
            if (copyMode)
            {
                var competition = MCCompetition.Provider.Get(competitionId);
                if (competition != null)
                {
                    var checkedIds = Request.Form["chkChecked"];
                    var ids = DataUtil.ParseCommaSeparatedIdList(checkedIds);
                    if (ids.Count > 0)
                    {
                        bool hasCopyError = false;
                        foreach (var id in ids)
                        {
                            var candidate = MCCandidate.Provider.Get(id);
                            if (candidate != null)
                            {
                                MCCandidate newCandidate = candidate.Copy();
                                newCandidate.Id = -1;
                                newCandidate.CompetitionId = competitionId;

                                // Copy resources
                                // IntegrationConstants.MCBasePath + CompetitionId + /Photos/thumb/ + finalist.GetPhotoFile()
                                if (!hasCopyError)
                                {
                                    try
                                    {
                                        var photoFile = candidate.GetPhotoFile();

                                        // Copy Photo Thumbnail
                                        var relSourceThumbPath = string.Format("{0}{1}/Photos/thumb/{2}", IntegrationConstants.MCBasePath, candidate.CompetitionId, photoFile);
                                        var absSourceThumbPath = MapPath(relSourceThumbPath);
                                        if (File.Exists(absSourceThumbPath))
                                        {
                                            var relTargetThumbPath = string.Format("{0}{1}/Photos/thumb/{2}", IntegrationConstants.MCBasePath, competitionId, photoFile);
                                            var absTargetThumbPath = MapPath(relTargetThumbPath);
                                            if (!File.Exists(absTargetThumbPath))
                                            {
                                                var absTargetFolder = Path.GetDirectoryName(absTargetThumbPath);
                                                if (!Directory.Exists(absTargetFolder))
                                                    Directory.CreateDirectory(absTargetFolder);

                                                File.Copy(absSourceThumbPath, absTargetThumbPath);
                                            }
                                        }

                                        // Copy Photo
                                        var relSourcePhotoPath = string.Format("{0}{1}/Photos/{2}", IntegrationConstants.MCBasePath, candidate.CompetitionId, photoFile);
                                        var absSourcePhotoPath = MapPath(relSourcePhotoPath);
                                        if (File.Exists(absSourcePhotoPath))
                                        {
                                            var relTargetPhotoPath = string.Format("{0}{1}/Photos/{2}", IntegrationConstants.MCBasePath, competitionId, photoFile);
                                            var absTargetPhotoPath = MapPath(relTargetPhotoPath);
                                            if (!File.Exists(absTargetPhotoPath))
                                                File.Copy(absSourcePhotoPath, absTargetPhotoPath);
                                        }

                                        // Copy Music
                                        var relSourceMusicDir = string.Format("{0}{1}/Music/", IntegrationConstants.MCBasePath, candidate.CompetitionId);
                                        var musicFile = candidate.SourceUrl;
                                        var relSourceMusicPath = "";
                                        if (!candidate.SourceUrl.Contains("/"))
                                        {
                                            relSourceMusicPath = string.Format("{0}{1}/Music/{2}", IntegrationConstants.MCBasePath, candidate.CompetitionId, candidate.SourceUrl);
                                        }
                                        else if (candidate.SourceUrl.StartsWith(relSourceMusicDir, StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            musicFile = candidate.SourceUrl.ToLower().Replace(relSourceMusicDir.ToLower(), "");
                                            relSourceMusicPath = candidate.SourceUrl;
                                        }

                                        var absSourceMusicPath = MapPath(relSourceMusicPath);
                                        if (File.Exists(absSourceMusicPath))
                                        {
                                            var relTargetMusicPath = string.Format("{0}{1}/Music/{2}", IntegrationConstants.MCBasePath, competitionId, musicFile);
                                            var absTargetMusicPath = MapPath(relTargetMusicPath);
                                            if (!File.Exists(absTargetMusicPath))
                                            {
                                                var absTargetFolder = Path.GetDirectoryName(absTargetMusicPath);
                                                if (!Directory.Exists(absTargetFolder))
                                                    Directory.CreateDirectory(absTargetFolder);

                                                File.Copy(absSourceMusicPath, absTargetMusicPath);
                                                newCandidate.SourceUrl = musicFile;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        hasCopyError = true;
                                        LogHelper.WriteLog(ex);
                                    }
                                }

                                newCandidate.Update();
                            }
                        }
                    }
                }
            }
            else
            {
                query.Set("CompetitionId", competitionId);
            }

            query.Redirect();
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            var checkedIds = Request.Form["chkChecked"];
            var ids = DataUtil.ParseCommaSeparatedIdList(checkedIds);
            if (ids.Count > 0)
            {
                foreach (var id in ids)
                {
                    var candidate = MCCandidate.Provider.Get(id);
                    if (candidate != null)
                    {
                        candidate.Delete();
                    }
                }

                GridView1.DataBind();
            }
        }
    }
}