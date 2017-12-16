using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Article
{

    public partial class AdminTemplates : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            foreach (string strFile in Directory.GetFiles(MapPath("Templates/Single/")))
            {
                FileInfo fi = new FileInfo(strFile);
                if (fi.Extension == ".ascx")
                {
                    DataRow dr = dt.NewRow();
                    dr["Name"] = fi.Name;
                    dt.Rows.Add(dr);
                }
            }
            ds.Tables.Add(dt);
            grvContent.DataSource = ds;
            grvContent.DataBind();
        }

        protected void grvContent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strPath = e.CommandArgument.ToString();
            if (e.CommandName == "EditSingle")
            {
                strPath = MapPath("Templates/Single/") + strPath;
                hidValue.Value = strPath;
                fckContent.Value = FileHelper.ReadFile(hidValue.Value);
                mtvTemplate.SetActiveView(viwEdit);
            }
            else if (e.CommandName == "EditMultiple")
            {
                strPath = MapPath("Templates/Multiple/") + strPath;
                hidValue.Value = strPath;
                fckContent.Value = FileHelper.ReadFile(hidValue.Value);
                mtvTemplate.SetActiveView(viwEdit);
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mtvTemplate.SetActiveView(viwContent);
        }

        protected void btnAddTemplate_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            mtvTemplate.SetActiveView(viwAdd);
        }

        protected void btnSaveRegistration_Click(object sender, EventArgs e)
        {
            FileHelper.WriteFile("<div id=\"divTitle\" runat=\"server\"></div>", MapPath("Templates/Single/") + txtName.Text + ".ascx");
            FileHelper.WriteFile("<div id=\"divContent\" runat=\"server\"></div>", MapPath("Templates/Multiple/") + txtName.Text + ".ascx");
            LoadData();
            mtvTemplate.SetActiveView(viwContent);
        }

        protected void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            FileHelper.WriteFile(fckContent.Value, hidValue.Value);
            mtvTemplate.SetActiveView(viwContent);
        }
    }
}