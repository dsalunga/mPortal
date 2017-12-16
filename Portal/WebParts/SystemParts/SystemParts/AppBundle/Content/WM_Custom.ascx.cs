using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;

using WCMS.WebSystem.Controls;
using WCMS.Common.Utilities;

using WCMS.Framework;

using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Content
{
    public partial class AdminContent : System.Web.UI.UserControl
    {
        protected TabControl TabControl1;
        protected TextEditor txtContent;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!TabControl1.HasTabs)
            {
                TabControl1.AddTab("tabBasic", "Basic");
                TabControl1.AddTab("tabAdvanced", "Advanced");
            }

            if (!Page.IsPostBack)
            {
                // Default Editor
                var isPlainTextDefault = ContentHelper.IsPlainTextDefault();

                hIsPlainTextDefault.Value = DataHelper.ToString(isPlainTextDefault);
                txtContent.IsPlainTextDefault = isPlainTextDefault;
                

                var sites = WebSiteViewModel.GenerateListItem(-1).ToArray();

                cboSites.Items.AddRange(sites);
                cboSiteID.Items.AddRange(sites);

                var siteId = DataHelper.GetId(Request, WebColumns.SiteId);
                if (siteId > 0)
                {
                    WebHelper.SetCboValue(cboSites, siteId);
                    grvContent.DataBind();
                }
            }
        }

        public void TabControl1_SelectedTabChanged(object oSender, TabEventArgs args)
        {
            switch (args.TabName)
            {
                case "tabBasic":
                    mtvContent.SetActiveView(viwBasic);
                    break;

                case "tabAdvanced":
                    mtvContent.SetActiveView(viwAdvance);
                    break;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            mtvContent.SetActiveView(viwBasic);

            var siteId = DataHelper.GetId(cboSites.SelectedValue);
            if (siteId > 0)
                WebHelper.SetCboValue(cboSiteID, siteId);

            litID.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtContent.Text = string.Empty;

            pnlContent.Visible = false;
            pnlContentEdit.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlContent.Visible = true;
            pnlContentEdit.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id = DataHelper.GetId(litID.Text.Trim());

            WebContent content = id > 0 ? WebContent.Get(id) : new WebContent();
            content.Title = txtTitle.Text.Trim();
            content.Content = txtContent.Text;
            content.IsActive = chkIsActive.Checked;
            content.IsEditorSensitive = chkEditorSensitive.Checked;
            content.VersionNo = 1;
            content.IsActiveContent = chkActiveContent.Checked;
            content.SiteId = DataHelper.GetId(cboSiteID.SelectedValue);
            content.Update();

            pnlContent.Visible = true;
            pnlContentEdit.Visible = false;
            grvContent.DataBind();
        }

        protected void grvContent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Toggle_Active":
                    var content = WebContent.Get(id);
                    if (content != null)
                    {
                        content.IsActive = !content.IsActive;
                        content.Update();
                    }
                    grvContent.DataBind();
                    break;

                case "ContentEdit":
                    LoadDataEdit(id);
                    break;

                case "Custom_Delete":
                    WebContent.Delete(id);
                    grvContent.DataBind();
                    break;
            }
        }

        public void LoadDataEdit(int id)
        {
            WebContent content = WebContent.Get(id);
            if (content != null)
            {
                if (content.IsEditorSensitive || DataHelper.GetBool(hIsPlainTextDefault.Value))
                    txtContent.SetPlainTextEditor();

                pnlContent.Visible = false;
                pnlContentEdit.Visible = true;
                litID.Text = content.Id.ToString();
                txtTitle.Text = content.Title;
                txtContent.Text = content.Content;
                chkIsActive.Checked = content.IsActive;
                chkEditorSensitive.Checked = content.IsEditorSensitive;
                chkActiveContent.Checked = content.IsActiveContent;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["grvContent"];
            if (!string.IsNullOrEmpty(ids))
            {
                var items = DataHelper.ParseCommaSeparatedIdList(ids);
                foreach (var item in items)
                    WebContent.Delete(item);

                grvContent.DataBind();
            }
        }

        protected void cboSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvContent.DataBind();
        }

        protected void cmdDuplicate_Click(object sender, EventArgs e)
        {
            //string sChecked = Request.Form["grvContent"];
            //if (!string.IsNullOrEmpty(sChecked))
            //{
            //    SqlHelper.ExecuteNonQuery(CommandType.Text,
            //        "INSERT INTO C.Items " +
            //        "SELECT Title + ' - Copy' AS Title,description,Date,Content,SiteID,IsActive,@UserId AS UserId,GETDATE() AS DateModified,@UserId AS ModifiedUserId " +
            //        "FROM C.Items WHERE ID IN (" + sChecked + ");",
            //        new SqlParameter("@UserId", Membership.GetUser().ProviderUserKey)
            //    );

            //    grvContent.DataBind();
            //}
        }

        public DataSet Select(int siteId)
        {
            return DataHelper.ToDataSet(
                from i in WebContent.GetListActive()
                where siteId <=0 || i.SiteId == siteId
                select new
                {
                    i.Id,
                    i.Title,
                    i.Active,
                    i.DateModified,
                    Content = DataHelper.GetStringPreview(i.Content, WConstants.PreviewChars)
                }
            );
        }
    }
}