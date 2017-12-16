using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Contact
{
    /// <summary>
    ///		Summary description for CMS_Contacts_01.
    /// </summary>
    public partial class AdminContactList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here

            if (!Page.IsPostBack)
            {
            }
        }

        protected void cmdAdd_Click(object sender, System.EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            query["Load"] = "AdminContactDetails.ascx";
            query.Redirect();
        }

        protected void cmdDelete_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                var items = DataHelper.ParseCommaSeparatedIdList(sChecked);
                if (items.Count > 0)
                    foreach (int id in items)
                        Contact.Delete(id);

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
                    query["Load"] = "AdminContactDetails.ascx";
                    query["__id"] = sID;
                    query.Redirect();
                    break;
            }
        }

        public DataSet GetContacts()
        {
            return DataHelper.ToDataSet(Contact.GetList());
        }
    }
}
