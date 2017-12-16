using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.WebSystem.WebParts.Photo.Controls;

using WCMS.Framework;
using WCMS.Framework.Diagnostics;

namespace WCMS.WebSystem.WebParts.Photo
{
    public partial class PictureGallery : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sw = PerformanceLog.StartLog();

            WContext context = new WContext(this);
            int siteId = context.Page.SiteId;
            int albumId = context.GetId("CID");
            int photoId = context.GetId("PID");
            string controlFile = "Album.ascx";
            string diplayMethod = "Thumbnails.ascx";

            int albumColumns = 2;
            int thumbColumns = 4;
            int thumbRows = 5;
            int albumCellPadding = 15;
            int maxPhotoWidth = 700;

            using (SqlDataReader r = SqlHelper.ExecuteReader("GalleryObject_Get",
                    new SqlParameter("@ObjectId", context.ObjectId),
                    new SqlParameter("@RecordId", context.RecordId)))
            {
                if (r.Read())
                {
                    diplayMethod = r["InitialControl"].ToString();

                    //if (string.IsNullOrEmpty(controlFile)) 
                    //    controlFile = "Album.ascx";

                    maxPhotoWidth = DataHelper.GetInt32(r, "MaxPhotoWidth");
                    albumColumns = DataHelper.GetInt32(r["AlbumColumns"], albumColumns);
                    albumCellPadding = DataHelper.GetInt32(r["AlbumCellPadding"], albumCellPadding);
                    thumbColumns = DataHelper.GetInt32(r["ThumbColumns"], thumbColumns);
                    thumbRows = DataHelper.GetInt32(r["ThumbRows"], thumbRows);
                }
            }

            if (photoId > 0 || albumId > 0)
                controlFile = diplayMethod; //"FullView.ascx"; // SHOW FULL PICTURE

            //else if (albumId > 0)
            //{
            //    // SHOW THUMBNAILS
            //    controlFile = "Thumbnails.ascx";
            //}

            //Response.Write(string.Format("<h1>ThumbColumns = {0}</h1>", iThumbColumns));

            string controlPath = "Controls/" + controlFile;
            Control control = LoadControl(controlPath);
            switch (controlFile)
            {
                case "Album.ascx":
                    AlbumView albumView = (AlbumView)control;
                    albumView.Columns = albumColumns;
                    albumView.CellPadding = albumCellPadding;
                    break;

                case "FullView.ascx":
                    FullView fullView = (FullView)control;
                    fullView.MaxPhotoWidth = maxPhotoWidth.ToString();
                    fullView.ThumbnailCount = thumbColumns;
                    break;

                case "Thumbnails.ascx":
                    ThumbnailView thumbView = (ThumbnailView)control;
                    thumbView.ThumbnailColumns = thumbColumns;
                    thumbView.ThumbnailRows = thumbRows;
                    break;

                case "FancyBoxThumbnails.ascx":
                    FancyBoxThumbnails fancyBoxThumbs = (FancyBoxThumbnails)control;
                    fancyBoxThumbs.ThumbnailColumns = thumbColumns;
                    fancyBoxThumbs.ThumbnailRows = thumbRows;
                    break;

                //case "Slideshow.ascx":
                //    Slideshow ss = (Slideshow)c;
                //    ss.Width = iThumbColumns;
                //    ss.Height = iThumbRows;
                //    break;
            }

            this.Controls.Add(control);

            PerformanceLog.EndLog(string.Format("Photo: {0}/{1}", context.ObjectId, context.RecordId), sw, context.PageId);
        }
    }
}