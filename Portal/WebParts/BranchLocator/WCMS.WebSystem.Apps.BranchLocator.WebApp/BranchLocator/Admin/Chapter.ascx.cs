using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Shared;

namespace WCMS.WebSystem.Apps.BranchLocator
{
    public partial class WebOfficeController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                var divisionType = (int)ChapterTypes.Division;

                cboCountries.DataSource = Country.GetList();
                cboCountries.DataBind();

                var divisions = from i in MChapter.Provider.GetList()
                                where i.ChapterType == divisionType
                                select i;
                cboDivision.DataSource = divisions;
                cboDivision.DataBind();

                var query = new WQuery(this);
                var q = query.Clone();
                q.Remove(WebColumns.Id);
                q.RemoveLoad();
                linkChapters.HRef = q.BuildQuery();
                linkCancel.HRef = q.BuildQuery();

                int id = query.GetId(WebColumns.Id);
                int parentId = query.GetId(WebColumns.ParentId);
                if (id > 0)
                {
                    var item = MChapter.Provider.Get(id);
                    if (item != null)
                    {
                        txtName.Text = item.Name;
                        txtAddressLine1.Text = item.Address;
                        txtPhoneNumber.Text = item.Telephone;
                        txtMobileNumber.Text = item.Mobile;
                        txtEmailAddress.Text = item.Email;
                        txtServiceSchedule.Text = item.ServiceSchedule;
                        txtMoreInfo.Text = item.MoreInfo;

                        WebHelper.SetCboValue(cboCountries, item.CountryCode);
                        WebHelper.SetCboValue(cboChapterType, item.ChapterType);
                        WebHelper.SetCboValue(cboAccessType, item.AccessType);

                        //txtLatitude.Text = item.Latitude.ToString();
                        //txtLongitude.Text = item.Longitude.ToString();
                        txtDistrictNo.Text = item.DistrictNo.ToString();
                        txtLocaleId.Text = item.LocaleId > 0 ? item.LocaleId.ToString() : "";

                        var parent = item.Parent;
                        if (parent != null)
                            txtParent.Text = parent.Id.ToString();

                        if (item.DivisionId > 0)
                        {
                            WebHelper.SetCboValue(cboDivision, item.DivisionId);
                        }
                        else if (item.ChapterType == divisionType)
                        {
                            WebHelper.SetCboValue(cboDivision, item.Id);
                        }
                        else if(item.ParentId > 0)
                        {
                            var i = item;
                            while (true)
                            {
                                i = i.Parent;
                                if (i == null || i.Id == item.Id)
                                    break;

                                if (i.ChapterType == divisionType)
                                {
                                    WebHelper.SetCboValue(cboDivision, i.Id);
                                    break;
                                }
                            }
                        }

                        lblHeader.InnerHtml = item.Name;
                        lblLastUpdate.InnerHtml = item.LastUpdate.ToString();
                        cmdDelete.Visible = true;

                        // Location Tab URL
                        query.BasePath = CentralPages.LoaderRazor;

                        query.SetLoad("FindALocale/Admin/SetLocation");
                        linkLocation.HRef = query.BuildQuery();

                        query.SetLoad("FindALocale/Admin/Announcements");
                        linkAnnouncements.HRef = query.BuildQuery();
                        return;
                    }
                }
                else if (parentId > 0)
                {
                    txtParent.Text = parentId.ToString();
                }

                linkLocation.HRef = query.BuildQuery() + "#";
            }
        }


        private void Redirect()
        {
            var query = new WQuery(this);
            query.Remove(WebColumns.Id);
            //int id = query.GetId(WebColumns.Id);
            //if (id > 0)
            //    query.Set(WConstants.Load, "Admin/ChapterHome");
            //else
            query.Set(WConstants.Load, "FindALocale/Admin/Chapters");
            query.Redirect();
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            Update();
            //this.Redirect();
            lblStatus.Visible = true;
        }

        private int Update()
        {
            int id = DataUtil.GetId(Request, WebColumns.Id);
            var isNew = id <= 0;
            int parentId = DataUtil.GetId(txtParent.Text.Trim());
            MChapter parent = parentId > 0 ? MChapter.Provider.Get(parentId) : null; //txtParent.Text.Trim());

            var item = (isNew) ? new MChapter() : MChapter.Provider.Get(id);
            item.Name = txtName.Text.Trim();
            item.Address = txtAddressLine1.Text.Trim();
            item.Telephone = txtPhoneNumber.Text.Trim();
            item.Mobile = txtMobileNumber.Text.Trim();
            item.Email = txtEmailAddress.Text.Trim();
            item.ParentId = parent == null ? -1 : parent.Id;
            item.ServiceSchedule = txtServiceSchedule.Text.Trim();
            item.MoreInfo = txtMoreInfo.Text.Trim();
            //item.Latitude = DataHelper.GetDouble(txtLatitude.Text.Trim(), 0);
            //item.Longitude = DataHelper.GetDouble(txtLongitude.Text.Trim(), 0);
            item.CountryCode = DataUtil.GetId(cboCountries.SelectedValue);
            item.ChapterType = DataUtil.GetInt32(cboChapterType.SelectedValue);
            item.AccessType = DataUtil.GetInt32(cboAccessType.SelectedValue);
            item.DivisionId = DataUtil.GetId(cboDivision.SelectedValue);
            item.DistrictNo = DataUtil.GetInt32(txtDistrictNo.Text);
            item.LocaleId = DataUtil.GetId(txtLocaleId.Text);
            item.LastUpdate = DateTime.Now;
            id = item.Update();

            if (isNew)
            {
                var query = new WQuery(this);
                query.Set(WebColumns.Id, id);
                query.BasePath = CentralPages.LoaderRazor;
                query.SetLoad("FindALocale/Admin/SetLocation");
                query.Redirect();
                //query.Set(WebColumns.Id, id);
                //query.BasePath = CentralPages.LoaderRazor;
                //query.SetLoad("FindALocale/Admin/SetLocation");
                //linkLocation.HRef = query.BuildQuery();

                //lblLastUpdate.InnerHtml = item.LastUpdate.ToString();
                //cmdDelete.Visible = true;
            }
            
            return id;
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            int id = DataUtil.GetId(Request, WebColumns.Id);
            MChapter.Provider.Delete(id);

            Redirect();
        }

        //protected void cmdUpdateContinue_Click(object sender, EventArgs e)
        //{
        //    var id = Update();
        //    var query = new WQuery(this);
        //    query.Set(WebColumns.Id, id);
        //    query.BasePath = CentralPages.LoaderRazor;
        //    query.SetLoad("FindALocale/Admin/SetLocation");
        //    query.Redirect();
        //}
    }
}