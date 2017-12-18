using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Shared;

namespace WCMS.WebSystem.Apps.BranchLocator
{
    public partial class WebOfficesController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblBreadcrumb.InnerHtml = FALHelper.BuildAdminBreadcrumb(new WQuery(this));

                var id = DataUtil.GetId(Request, WebColumns.Id);
                if (id > 0)
                {
                    var item = MChapter.Provider.Get(id);
                    if (item != null)
                    {
                        var query = new WQuery(this);
                        if (item.ParentId > 0)
                            query.Set(WebColumns.ParentId, item.ParentId);
                        query.SetLoad("FindALocale/Admin/Chapter");
                        query.Redirect();
                    }
                }

                var parentId = DataUtil.GetId(Request, WebColumns.ParentId);
                if (parentId > 0)
                {
                    var query = new WQuery(this);
                    query.Set(WebColumns.Id, parentId);
                    query.SetLoad("FindALocale/Admin/Chapter");

                    linkEdit.HRef = query.BuildQuery();
                    linkEdit.Visible = true;
                    cmdUp.Visible = true;
                }
            }
        }

        protected void cmdAddFull_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            query.Remove(WebColumns.Id);
            query.Set(WConstants.Load, "FindALocale/Admin/Chapter");
            query.Redirect();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //var query = new WQuery(this);
            //int id = DataHelper.GetId(e.CommandArgument);

            //switch (e.CommandName)
            //{
            //    //case "Custom_Edit":
            //    //    query.Set(WebColumns.Id, id);
            //    //    query.Set(WConstants.Load, "Admin/ChapterHome");
            //    //    query.Redirect();
            //    //    break;

            //    case "Custom_Delete":
            //        if (id > 0)
            //        {
            //            MChapter.Provider.Delete(id);
            //            GridView1.DataBind();
            //        }
            //        break;

            //    case "View_ChildNodes":
            //        query.Set(WebColumns.ParentId, id);
            //        query.Redirect();
            //        break;
            //}
        }

        protected void cmdUp_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            int parentId = query.GetId(WebColumns.ParentId);
            if (parentId > 0)
            {
                query.Set(WebColumns.ParentId, MChapter.Provider.Get(parentId).ParentId);
                query.Redirect();
            }
        }

        public DataSet Get(int parentId)
        {
            var qLoc = new WQuery(true);
            var qEdit = qLoc.Clone();
            var qChildren = qLoc.Clone();
            var countries = Country.Provider.GetList();
            Country country = null;

            qLoc.BasePath = CentralPages.LoaderRazor;
            qLoc.SetLoad("FindALocale/Admin/SetLocation");

            qEdit.SetLoad("FindALocale/Admin/Chapter");

            return DataHelper.ToDataSet(
                from i in MChapter.Provider.GetList(parentId)
                select new
                {
                    i.Id,
                    i.Name,
                    Country = (country = countries.FirstOrDefault(c => c.Id == i.CountryCode)) == null ? "" : country.CountryName,
                    SetLocationUrl = qLoc.Set(WebColumns.Id, i.Id).BuildQuery(),
                    Address = string.IsNullOrEmpty(i.Address) ? "Set Address & Coordinates" : DataHelper.GetStringPreview(i.Address, 30),
                    EditUrl = qEdit.Set(WebColumns.Id, i.Id).BuildQuery(),
                    ChildrenUrl = qChildren.Set(WebColumns.ParentId, i.Id).BuildQuery(),
                    LatLng = i.Latitude == 0 && i.Longitude == 0 ? "" : i.GetLatLng(),
                    AccessType = ChapterAccess.Get(i.AccessType)
                });
        }

        protected void cmdFix_Click(object sender, EventArgs e)
        {
            var divisionType = (int)ChapterTypes.Division;
            var districtType = (int)ChapterTypes.District;
            var items = MChapter.Provider.GetList();
            MChapter division = null, district = null;

            Action<MChapter> ProcessItems = null;
            ProcessItems = (parent) =>
            {
                IEnumerable<MChapter> children = parent == null ? items.Where(i => i.ParentId == -1) : parent.Children;
                if (children.Count() > 0)
                {
                    foreach (var child in children)
                    {
                        var hasUpdate = false;
                        if (child.ChapterType == divisionType)
                            division = child;
                        else if (child.ChapterType == districtType)
                            district = child;

                        if (division != null)
                        {
                            if (child.DivisionId != division.Id)
                            {
                                hasUpdate = true;
                                child.DivisionId = division.Id;
                            }
                        }
                        else if(child.DivisionId != -1)
                        {
                            hasUpdate = true;
                            child.DivisionId = -1;
                        }

                        if (district != null)
                        {
                            if (child.DistrictNo != district.DistrictNo)
                            {
                                hasUpdate = true;
                                child.DistrictNo = district.DistrictNo;
                            }
                        }
                        else if(child.DistrictNo != -1)
                        {
                            hasUpdate = true;
                            child.DistrictNo = -1;
                        }

                        if (hasUpdate)
                            child.Update();

                        ProcessItems(child);

                        if (child.ChapterType == divisionType)
                            division = null;
                        else if (child.ChapterType == districtType)
                            district = null;
                    }
                }
            };

            ProcessItems(null);
        }
    }
}