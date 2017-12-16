using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.Controls;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Contact
{
    /// <summary>
    ///		Summary description for CMS_Inquiries_01.
    /// </summary>
    public partial class AdminInquiriesList : System.Web.UI.UserControl
    {
        protected TabControl TabControl1;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //this.BindGrid();
                cboSites.Items.AddRange(WebSiteViewModel.GenerateListItem(-1).ToArray());

                string sSiteId = Request[WebColumns.SiteId];
                if (!string.IsNullOrEmpty(sSiteId))
                {
                    try
                    {
                        cboSites.SelectedValue = sSiteId;
                    }
                    catch { }
                }
            }
        }

        protected void cmdDelete_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            var items = DataHelper.ParseCommaSeparatedIdList(sChecked);
            if (items.Count > 0)
            {
                foreach (int id in items)
                {
                    ContactInquiry.Delete(id);
                }

                GridView1.DataBind();
            }
        }

        protected void cmdActive_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            var items = DataHelper.ParseCommaSeparatedIdList(sChecked);
            if (items.Count > 0)
            {
                foreach (int id in items)
                {
                    var item = ContactInquiry.Get(id);
                    if (item.IsActive != 1)
                    {
                        item.IsActive = 1;
                        item.Update();
                    }
                }

                GridView1.DataBind();
            }
        }

        protected void cmdDeactivate_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            var items = DataHelper.ParseCommaSeparatedIdList(sChecked);
            if (items.Count > 0)
            {
                foreach (int id in items)
                {
                    var item = ContactInquiry.Get(id);
                    if (item.IsActive != 0)
                    {
                        item.IsActive = 0;
                        item.Update();
                    }
                }

                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            QueryParser query = new QueryParser(this);
            string sID = e.CommandArgument.ToString();

            switch (e.CommandName)
            {
                case "edit_item":
                    query["Load"] = "AdminInquiriesDetails.ascx";
                    query["__id"] = sID;
                    query.Redirect();
                    break;
            }
        }

        protected void cboSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        //public void BindGrid() //(int objectId, int recordId)
        //{
        //    GridView1.DataSource = ContactInquiry.GetList();
        //    GridView1.DataBind();
        //}

        public DataSet Select()
        {
            return DataHelper.ToDataSet(from item in ContactInquiry.GetList()
                                        select new
                                        {
                                            item.InquiryId,
                                            item.IsActive,
                                            item.SenderName,
                                            item.InquiryType,
                                            Subject = item.Subject.Length > WConstants.PreviewChars ? item.Subject.Substring(0, WConstants.PreviewChars) + "..." : item.Subject,
                                            item.SendTo,
                                            item.InqDateTime,
                                            item.Phone,
                                            item.Email
                                        });
        }

        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            string sFile = "ContactUs_Inquiries.xls";
            string sFilePath = MapPath("~/Admin/Data/" + sFile);
            DataSet ds = null;
            int siteId = DataHelper.GetId(cboSites.SelectedValue);

            string sDir = Path.GetDirectoryName(sFilePath);
            if (!Directory.Exists(sDir))
                Directory.CreateDirectory(sDir);

            if (siteId > 0)
                ds = DataHelper.ToDataSet(ContactInquiry.GetList(WebObjects.WebSite, siteId));
            else
                ds = DataHelper.ToDataSet(ContactInquiry.GetList());

            ds.DataSetName = "ContactUs";
            ds.Tables[0].TableName = "Inquiries";
            ds.WriteXml(sFilePath);

            Response.AppendHeader("content-disposition", "attachment; filename=" + sFile);
            Response.WriteFile(sFilePath);
            Response.End();
        }
    }
}
