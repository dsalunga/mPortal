using System;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.WebParts.Photo;

namespace WCMS.WebSystem.WebParts.Photo.Controls
{
    /// <summary>
    ///		Summary description for AlbumView.
    /// </summary>
    public partial class AlbumView : System.Web.UI.UserControl
    {
        private int _Columns = 2;
        private int _CellPadding = 15;
        private WContext context;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            context = new WContext(this.Parent);
            
            var element = context.Element;
            var sortBy = element.GetParameterValue("SortBy", "Title");

            var items = Album.Provider.GetList(context.ObjectId, context.RecordId);

            rGallery.RepeatColumns = _Columns;
            rGallery.DataSource = from i in items
                                  orderby i.DateModified descending
                                  select i;
            rGallery.DataBind();

            /*
            using (SqlDataReader r = SqlHelper.ExecuteReader("GalleryCategoryLink_GetTypeId",
                       new SqlParameter("@RecordId", context.RecordId),
                       new SqlParameter("@ObjectId", context.ObjectId),
                       new SqlParameter("@SortBy", sortBy)
                ))
            {
                rGallery.RepeatColumns = _Columns;
                rGallery.DataSource = r;
                rGallery.DataBind();
            }
            */
        }

        protected string CreateLink(string id)
        {
            context["CID"] = id;
            return context.BuildQuery();
        }

        protected string BuildAlbumPath(string albumFile)
        {
            return PhotoConstants.GalleryPath + "Resized." + albumFile;
        }

        public int Columns
        {
            set { _Columns = value; }
            get { return _Columns; }
        }

        public int CellPadding
        {
            set { _CellPadding = value; }
            get { return _CellPadding; }
        }
    }
}