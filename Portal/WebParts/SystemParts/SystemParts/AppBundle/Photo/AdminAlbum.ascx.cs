using System;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

using WCMS.Common.Utilities;
using WCMS.WebSystem.WebParts.Photo;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Photo
{
    /// <summary>
    ///		Summary description for CONTENTCMS_Category.
    /// </summary>
    public partial class AdminAlbum : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        public DataSet Select()
        {
            return DataHelper.ToDataSet(
                from i in Album.Provider.GetList()
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

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            ShowEditView();
        }

        private void ShowEditView(int id = -1)
        {
            var query = new WQuery(this);
            if (id > 0)
                query.Set("CategoryId", id);
            else
                query.Remove("CategoryId");
            query.SetLoad("AdminAlbumEdit");
            query.Redirect();
        }

        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
            string formChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(formChecked))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(formChecked);
                if (ids.Count > 0)
                {
                    foreach (var id in ids)
                    {
                        var album = Album.Provider.Get(id);
                        if (album != null)
                            album.Delete();
                    }

                    GridView1.DataBind();

                    //SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE GalleryCategory WHERE CategoryID IN(" + formChecked + ");");
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var id = DataHelper.GetId(e.CommandArgument);
            switch (e.CommandName)
            {
                case "edit_item":
                    var item = Album.Provider.Get(id);
                    if (item != null) ShowEditView(item.Id);
                    else lblNotify.Text = "Album does not exist.";
                    break;

                case "view_pictures":
                    var query = new WQuery(this);
                    query.Set("CategoryId", id);
                    query.LoadAndRedirect("AdminPhotos");
                    break;
            }
        }
    }
}