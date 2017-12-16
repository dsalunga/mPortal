using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.ViewModel;

using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.WebParts.Contact
{
    /// <summary>
    ///		Summary description for CMS_Inquiries_01.
    /// </summary>
    public partial class ConfigInquiriesList : System.Web.UI.UserControl, IUpdatable
    {
        protected TabControl TabControl1;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                var pair = WHelper.GetObjectStruct();
                var link = ContactLink.Get(pair.ObjectId, pair.RecordId);
                if (link != null)
                    cboMode.SelectedValue = link.Mode.ToString();

                hObjectId.Value = pair.ObjectId.ToString();
                hRecordId.Value = pair.RecordId.ToString();
                GridView1.DataBind();

                // INQUIRIES
                //cboSites.Items.AddRange(WebSiteViewModel.GenerateListItem(-1).ToArray());

                //int siteId = DataHelper.GetId(Request, WebColumns.SiteId);
                //if (siteId > 0)
                //{
                //    try
                //    {
                //        cboSites.SelectedValue = siteId.ToString();
                //    }
                //    catch { }
                //}

                TabControl1.AddTab("tabGeneral", "General");
                TabControl1.AddTab("tabInquiries", ContactConstants.CONST_Inquiries);

                string tab = DataHelper.Get(Request, "Tab");
                if (!string.IsNullOrWhiteSpace(tab) && tab.Equals(ContactConstants.CONST_Inquiries, StringComparison.InvariantCultureIgnoreCase))
                    TabControl1.SelectedTab = "tabInquiries";
            }
        }

        protected void TabControl1_SelectedTabChanged(object sender, TabEventArgs e)
        {
            switch (e.TabName)
            {
                case "tabGeneral":
                    MultiView1.SetActiveView(viewBasic);
                    break;

                case "tabInquiries":
                    MultiView1.SetActiveView(viewAdvanced);
                    break;
            }
        }

        public DataSet Select(int objectId, int recordId)
        {
            return DataHelper.ToDataSet(ContactInquiry.GetList(objectId, recordId));
        }

        public IEnumerable<Contact> GetContacts()
        {
            return Contact.GetList();
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
            var query = new WQuery(this);
            string sID = e.CommandArgument.ToString();

            switch (e.CommandName)
            {
                case "edit_item":
                    query.Set("__id", sID);
                    query.Set("Tab", ContactConstants.CONST_Inquiries);
                    query.LoadAndRedirect("AdminInquiriesDetails.ascx");
                    break;
            }
        }

        protected void cboSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            string sFile = "ContactUs_Inquiries.xls";
            string sFilePath = MapPath("~/Admin/Data/" + sFile);

            var pair = WHelper.GetObjectStruct();
            DataSet ds = DataHelper.ToDataSet(ContactInquiry.GetList(pair.ObjectId, pair.RecordId));

            //int siteId = DataHelper.GetId(cboSites.SelectedValue);
            //if (siteId > 0)
            //{

            //}
            //else
            //{
            //    ds = DataHelper.ToDataSet(ContactInquiry.GetList());
            //}

            ds.DataSetName = "ContactUs";
            ds.Tables[0].TableName = "Inquiries";
            ds.WriteXml(sFilePath);

            Response.AppendHeader("content-disposition", "attachment; filename=" + sFile);
            Response.WriteFile(sFilePath);
            Response.End();
        }

        #region IUpdatable Members

        public bool Update()
        {
            var pair = WHelper.GetObjectStruct();
            var link = ContactLink.Get(pair.ObjectId, pair.RecordId);
            if (link == null) link = new ContactLink();
            {
                link.RecordId = pair.RecordId;
                link.ObjectId = pair.ObjectId;
            }

            link.ContactId = DataHelper.GetId(DropDownList1.SelectedValue);
            link.Mode = DataHelper.GetInt32(cboMode.SelectedValue);
            link.Update();

            return true;
        }

        public string UpdateText
        {
            get { return ""; }
        }

        public string CancelText
        {
            get { return ""; }
        }

        #endregion
    }
}
