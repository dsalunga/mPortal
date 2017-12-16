using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.WebParts.Article
{
    public partial class TemplatesController : System.Web.UI.UserControl
    {
        protected TextEditor txtItemTemplate;
        protected TextEditor txtListTemplate;
        protected TextEditor txtDetailsTemplate;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                btnDelete.Attributes.Add("onclick", "return confirm('Delete selected item(s)?');");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strIDs = Request.Form["grvContent"];
            if (!string.IsNullOrEmpty(strIDs))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(strIDs);
                if (ids.Count > 0)
                    foreach (var id in ids)
                        ArticleTemplate.Delete(id);

                grvContent.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            litID.Text = "";
            txtName.Text = "";
            fckImage.Value = "";
            txtItemTemplate.Text = "";
            txtListTemplate.Text = "";
            txtDetailsTemplate.Text = "";
            txtDateFormat.Text = string.Empty;

            pnlContent.Visible = false;
            pnlContentEdit.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int templateId = DataHelper.GetId(litID.Text);
            ArticleTemplate item = null;

            if (templateId > 0 && (item = ArticleTemplate.Get(templateId)) != null)
            { }
            else
            {
                item = new ArticleTemplate();
            }

            item.Name = txtName.Text.Trim();
            //item.File = txtTemplate.Text.Trim();
            item.DateFormat = txtDateFormat.Text.Trim();
            item.ImageUrl = fckImage.Value.Trim();
            item.Date = DateTime.Now;
            item.ListItemTemplate = txtItemTemplate.Text;
            item.ListTemplate = txtListTemplate.Text;
            item.DetailsTemplate = txtDetailsTemplate.Text;
            item.Update();

            pnlContent.Visible = true;
            pnlContentEdit.Visible = false;
            grvContent.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlContent.Visible = true;
            pnlContentEdit.Visible = false;
        }


        protected void grvContent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);
            if (e.CommandName == "ContentEdit")
                LoadDataEdit(id);
        }

        private void LoadDataEdit(int intID)
        {
            var item = ArticleTemplate.Get(intID);
            if (item != null)
            {
                litID.Text = item.Id.ToString();
                txtName.Text = item.Name;
                fckImage.Value = item.ImageUrl;
                txtDateFormat.Text = item.DateFormat;
                txtItemTemplate.Text = item.ListItemTemplate;
                txtListTemplate.Text = item.ListTemplate;
                txtDetailsTemplate.Text = item.DetailsTemplate;

                pnlContent.Visible = false;
                pnlContentEdit.Visible = true;
            }
        }

        public DataSet GetTemplates()
        {
            return DataHelper.ToDataSet(ArticleTemplate.GetList());
        }
    }
}