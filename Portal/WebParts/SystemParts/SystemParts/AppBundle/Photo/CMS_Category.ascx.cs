using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.WebSystem.Controls;
using WCMS.Framework;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Photo
{
    /// <summary>
    ///		Summary description for CMS_Category.
    /// </summary>
    public partial class CMS_Category : System.Web.UI.UserControl, IUpdatable
    {
        int siteId = -1;
        int objectId = -1;
        int recordId = -1;

        protected TabControl TabControl1;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            int pageId = DataHelper.GetId(Request, WebColumns.PageId);
            int elementId = DataHelper.GetId(Request, WebColumns.PageElementId);

            recordId = elementId > 0 ? elementId : pageId;
            objectId = elementId > 0 ? WebObjects.WebPageElement : WebObjects.WebPage;
            siteId = WPage.Get(pageId).SiteId;

            if (!Page.IsPostBack)
            {
                //LoadData(-1);
                //LoadAllData(-1);

                hObjectId.Value = objectId.ToString();
                hRecordId.Value = recordId.ToString();

                GridViewPool.DataBind();
                GridViewInserted.DataBind();

                TabControl1.AddTab("tabAlbum", "Selected Albums");
                TabControl1.AddTab("tabConfig", "Configuration");

                using (SqlDataReader r = SqlHelper.ExecuteReader("GalleryObject_Get",
                        new SqlParameter("@ObjectId", objectId),
                        new SqlParameter("@RecordId", recordId)
                    ))
                {
                    if (r.Read())
                    {
                        WebHelper.SetCboValue(cboControls, r["InitialControl"].ToString());

                        txtAlbumColumns.Text = r["AlbumColumns"].ToString();
                        txtThumbColumns.Text = r["ThumbColumns"].ToString();
                        txtThumbRows.Text = r["ThumbRows"].ToString();
                        txtAlbumPadding.Text = r["AlbumCellPadding"].ToString();
                        txtMaxPhotoWidth.Text = DataHelper.Get(r, "MaxPhotoWidth");
                    }
                }

                var permissionId = WHelper.GetUserMgmtPermission();
                if (permissionId == Permissions.ManageInstance)
                {
                    //panelManageAll.Visible = true;

                    var query = new WQuery(this);
                    query.Set(WConstants.Load, "AdminAlbum.ascx");
                    //linkManage.HRef = qs.BuildQuery();

                    TabControl1.AddTab("tabAlbumManager", "Album Manager", query.BuildQuery());
                }
            }
        }

        protected void TabControl1_SelectedTabChanged(object sender, TabEventArgs e)
        {
            switch (e.TabName)
            {
                case "tabAlbum":
                    MultiView1.SetActiveView(viewBasic);
                    break;

                case "tabConfig":
                    MultiView1.SetActiveView(viewAdvanced);
                    break;
            }
        }

        public DataSet SelectInserted(int objectId, int recordId)
        {
            var inserted = AlbumLink.Provider.GetList(objectId, recordId);
            AlbumLink item = null;

            return DataHelper.ToDataSet(
                from i in Album.Provider.GetList(objectId, recordId)
                select new
                {
                    Id = (item = inserted.FirstOrDefault(a => i.Id == a.AlbumId)) != null ? item.Id : -1,
                    AlbumId = i.Id,
                    i.Title,
                    i.PhotoWidth,
                    i.PhotoHeight,
                    i.ImageFile,
                    ThumbUrl = i.ThumbPath,
                    i.DateModified
                }
            );
        }

        public DataSet SelectPool(int objectId, int recordId)
        {
            var inserted = Album.Provider.GetList(objectId, recordId);
            var all = Album.Provider.GetList();

            return DataHelper.ToDataSet(
                from i in all.Except(inserted, new AlbumEqualityComparer())
                select new
                {
                    i.Id,
                    i.Title,
                    i.PhotoWidth,
                    i.PhotoHeight,
                    i.ImageFile,
                    ThumbUrl = i.ThumbPath,
                    i.DateModified
                }
            );
        }

        //private void LoadData(int intPage)
        //{
        //    if (intPage != -1)
        //        dtgGallery.CurrentPageIndex = intPage;

        //    DataSet ds = SqlHelper.ExecuteDataSet("GalleryCategoryLink_GetTypeId",
        //        new SqlParameter("@ObjectId", objectId),
        //        new SqlParameter("@RecordId", recordId));

        //    int rowCount = ds.Tables[0].Rows.Count;

        //    dtgGallery.AllowPaging = rowCount > 10;
        //    dtgGallery.DataSource = ds;
        //    dtgGallery.DataBind();

        //    rowRemove.Visible = rowCount > 0;
        //}

        //private void LoadAllData(int intPage)
        //{
        //    if (intPage != -1)
        //        dtgContent.CurrentPageIndex = intPage;

        //    DataSet ds = SqlHelper.ExecuteDataSet("GalleryCategoryLink_GetTypeIdOut",
        //        new SqlParameter("@ObjectId", objectId),
        //        new SqlParameter("@RecordId", recordId));

        //    dtgContent.AllowPaging = ds.Tables[0].Rows.Count > 10;
        //    dtgContent.DataSource = ds;
        //    dtgContent.DataBind();

        //    rowInsert.Visible = ds.Tables[0].Rows.Count > 0;
        //}

        protected void btnInsert_Click(object sender, System.EventArgs e)
        {
            string checkedIds = Request.Form["chkCheckedPool"];
            if (!string.IsNullOrEmpty(checkedIds))
            {
                string query = string.Empty;
                string[] strGroup = checkedIds.Split(',');
                foreach (string str in strGroup)
                    query += "INSERT INTO GalleryCategoryLink (SiteID, ObjectId, RecordId, CategoryID) VALUES(" + siteId + ", '" + objectId + "', " + recordId + ", " + str + ");";

                SqlHelper.ExecuteNonQuery(CommandType.Text, query);

                GridViewPool.DataBind();
                GridViewInserted.DataBind();

                //LoadData(0);
                //LoadAllData(0);
            }
        }

        protected void btnRemove_Click(object sender, System.EventArgs e)
        {
            string checkedIds = Request.Form["chkCheckedInserted"];
            if (!string.IsNullOrEmpty(checkedIds))
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE GalleryCategoryLink WHERE (Id) IN(" + checkedIds + ");");

                GridViewPool.DataBind();
                GridViewInserted.DataBind();

                //LoadData(0);
                //LoadAllData(0);
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            Update();

            lblStatus.Text = "Update Successful!";
        }

        #region IUpdatable Members

        public bool Update()
        {
            string initialViewControl = cboControls.SelectedValue;
            int iAlbumColumns = 0;
            int iThumbColumns = 0;
            int iThumbRows = 0;
            int iAlbumCellPadding = 15;
            int maxPhotoWidth = 700;

            if (!int.TryParse(txtAlbumColumns.Text.Trim(), out iAlbumColumns))
            {
                lblStatus.Text = "Invalid Album Columns value.";
                return false;
            }

            if (!int.TryParse(txtMaxPhotoWidth.Text.Trim(), out maxPhotoWidth))
            {
                lblStatus.Text = "Invalid Max. Photo Width value. Value must be a number";
                return false;
            }

            if (!int.TryParse(txtAlbumPadding.Text.Trim(), out iAlbumCellPadding))
            {
                lblStatus.Text = "Invalid Album Cell Padding value.";
                return false;
            }

            if (!int.TryParse(txtThumbColumns.Text.Trim(), out iThumbColumns))
            {
                lblStatus.Text = "Invalid Thumbnail Columns value.";
                return false;
            }

            if (!int.TryParse(txtThumbRows.Text.Trim(), out iThumbRows))
            {
                lblStatus.Text = "Invalid Thumbnail Rows value.";
                return false;
            }

            // PERSIST DATA
            SqlHelper.ExecuteNonQuery("GalleryObject_Set",
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@InitialControl", initialViewControl),
                new SqlParameter("@AlbumColumns", iAlbumColumns),
                new SqlParameter("@ThumbColumns", iThumbColumns),
                new SqlParameter("@ThumbRows", iThumbRows),
                new SqlParameter("@AlbumCellPadding", iAlbumCellPadding),
                new SqlParameter("@MaxPhotoWidth", maxPhotoWidth)
            );

            return true;
        }

        public string UpdateText { get; set; }

        public string CancelText { get; set; }

        #endregion
    }
}