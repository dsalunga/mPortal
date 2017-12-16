using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Office
{
    public partial class OfficeBrowser : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindLevel1(-1);

                int id = DataHelper.GetId(Request[WebColumns.OfficeId]);
                if (id > 0)
                {
                    cboLevel1.SelectedValue = id.ToString();
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "View_Children":
                    var id = DataHelper.GetId(e.CommandArgument);
                    SelectItem(id);
                    break;
            }
        }

        private void SelectItem(int id)
        {
            var level1 = DataHelper.GetId(cboLevel1.SelectedValue);
            var level2 = DataHelper.GetId(cboLevel2.SelectedValue);
            var level3 = DataHelper.GetId(cboLevel3.SelectedValue);

            if (level1 < 0)
                BindLevel1(id);
            else if (level2 < 0)
                BindLevel2(id);
            else if (level3 < 0)
                BindLevel3(id);

            GridView1.DataBind();
        }

        private void BindLevel1(int id)
        {
            cboLevel1.Items.Clear();
            cboLevel1.Items.AddRange(WebOfficeViewModel.GenerateListItem(-1, "Chapters").ToArray());
            cboLevel1.Items.Insert(0, new ListItem("* Show All *", "-2"));
            cboLevel1.SelectedValue = id >0 ? id.ToString() : "-1";

            BindLevel2(-1);
        }

        private void BindLevel2(int id)
        {
            int level1 = DataHelper.GetInt32(cboLevel1.SelectedValue);

            cboLevel2.Items.Clear();

            if (level1 > -2)
            {
                cboLevel2.DataSource = WebOffice.GetList(level1);
                cboLevel2.DataBind();

                cboLevel2.Items.Insert(0, new ListItem("* Show All *", "-1"));
            }
            else
            {
                cboLevel2.Items.Add(new ListItem("* None *", "-1"));
            }

            if (id > 0)
                cboLevel2.SelectedValue = id.ToString();

            BindLevel3(-1);
        }

        private void BindLevel3(int id)
        {
            int level2 = DataHelper.GetInt32(cboLevel2.SelectedValue);

            cboLevel3.Items.Clear();

            if (level2 > 0)
            {
                cboLevel3.DataSource = WebOffice.GetList(level2);
                cboLevel3.DataBind();

                cboLevel3.Items.Insert(0, new ListItem("* Show All *", "-1"));
            }
            else
            {
                cboLevel3.Items.Add(new ListItem("* None *", "-1"));
            }

            if (id > 0)
                cboLevel3.SelectedValue = id.ToString();
        }

        public DataSet Select(int level1, int level2, int level3, string keyword)
        {
            var detailsPageFormat = WebRegistry.SelectNodeValue("/Apps/Office/DetailsPageFormat");
            var items = WebOffice.GetList();

            int parentId;

            if (level3 > 0)
                parentId = level3;
            else if (level2 > 0)
                parentId = level2;
            else
                parentId = level1;

            string kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();

            return DataHelper.ToDataSet(from i in items
                                        where (level1 == -2 || i.ParentId == parentId) &&
                                            (string.IsNullOrEmpty(kwl) ||
                                                (i.Name.ToLower().Contains(kwl) ||
                                                    i.AddressLine1.ToLower().Contains(kwl) ||
                                                    i.ContactPerson.ToLower().Contains(kwl) ||
                                                    i.EmailAddress.ToLower().Contains(kwl)))
                                        select new
                                        {
                                            i.Id,
                                            i.Name,
                                            i.MobileNumber,
                                            i.EmailAddress,
                                            i.PhoneNumber,
                                            i.ContactPerson,
                                            OfficeUrl = string.Format(detailsPageFormat, i.Id)
                                        });
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {

        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
        }

        protected void cboLevel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLevel2(-1);
        }

        protected void cboLevel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLevel3(-1);
        }
    }
}