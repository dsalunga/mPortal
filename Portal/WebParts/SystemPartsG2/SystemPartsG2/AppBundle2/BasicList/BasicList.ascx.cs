using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.BasicList
{
    public partial class _Sections_BasicList_BasicList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WContext ctx = new WContext(this);
            int objectId = ctx.ObjectId;
            int recordId = ctx.RecordId;

            string sAlternatingColor = string.Empty;
            string sTextColor = string.Empty;
            string sTemplateFile = "Single.ascx";
            int iPageSize = 25;
            int iRepeatColumns = 1;
            int iCellPadding = 5;
            int iGridLines = 0;

            using (SqlDataReader r = SqlHelper.ExecuteReader("BasicList_Get",
                    new SqlParameter("@PageType", objectId),
                    new SqlParameter("@SitePageItemID", recordId)
                ))
            {
                if (r.Read())
                {
                    string _sTemplateFile = r["ItemTemplate"].ToString();
                    if (!string.IsNullOrEmpty(_sTemplateFile)) sTemplateFile = _sTemplateFile;

                    sAlternatingColor = r["AlternatingColor"].ToString();
                    sTextColor = r["TextColor"].ToString();
                    iRepeatColumns = (int)r["RepeatColumns"];
                    iCellPadding = (int)r["CellPadding"];
                    iPageSize = (int)r["PageSize"];
                    iGridLines = (int)r["GridLines"];
                }
            }

            SqlDataSource1.SelectParameters["PageSize"].DefaultValue = iPageSize.ToString();
            SqlDataSource1.SelectParameters["PageType"].DefaultValue = objectId.ToString();
            SqlDataSource1.SelectParameters["SitePageItemID"].DefaultValue = recordId.ToString();

            // LOAD LIST
            Control c = LoadControl("ItemTemplates/" + sTemplateFile);
            DataList DataList1 = (DataList)c.FindControl("DataList1");

            if (!string.IsNullOrEmpty(sAlternatingColor)) DataList1.AlternatingItemStyle.BackColor = System.Drawing.Color.FromName(sAlternatingColor);
            if (!string.IsNullOrEmpty(sTextColor)) DataList1.ItemStyle.ForeColor = System.Drawing.Color.FromName(sTextColor);
            DataList1.RepeatColumns = iRepeatColumns;
            DataList1.CellPadding = iCellPadding;
            DataList1.GridLines = (GridLines)iGridLines;
            DataList1.DataSourceID = "SqlDataSource1";

            phList.Controls.Add(c);
            DataList1.DataBind();

            // PAGING
            string sPage = ctx["Page"];

            int iPage = 1;
            if (!string.IsNullOrEmpty(sPage))
            {
                iPage = int.Parse(sPage);
                if (iPage < 1) iPage = 1;
            }

            int iCount = (int)SqlHelper.ExecuteScalar(CommandType.Text,
                "SELECT COUNT(*) FROM BasicListItem WHERE (SitePageItemID=@SitePageItemID) AND (PageType=@PageType)",
                new SqlParameter("@SitePageItemID", recordId),
                new SqlParameter("@PageType", objectId)
            );

            if (iCount == 0)
            {
                return;
            }

            int iPageCount = (int)decimal.Ceiling((decimal)iCount / iPageSize);
            if (iPageCount > 1)
            {
                string sPaging = "Page:";
                for (int i = 1; i <= iPageCount; i++)
                {
                    if (i == iPage)
                    {
                        sPaging += "&nbsp;" + i;
                    }
                    else
                    {
                        ctx["Page"] = i.ToString();
                        sPaging += "&nbsp;<a style=\"color: red;font-size:11px\" href=\"/?" + ctx.BuildQuery() + "\">" + i + "</a>";
                    }
                }

                divPaging.InnerHtml = sPaging;
            }
        }
    }
}