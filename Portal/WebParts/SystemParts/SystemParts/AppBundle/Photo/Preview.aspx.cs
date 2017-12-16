using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;

using WCMS.WebSystem.WebParts.Photo;

namespace WCMS.WebSystem.WebParts.Photo
{
    public partial class _Sections_PG_Preview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var id = DataHelper.GetId(Request, "PID");
                string folder = DataHelper.Get(Request, "Album");

                if (id > 0)
                {
                    var photo = AlbumPhoto.Provider.Get(id);
                    if (photo != null)
                    {
                        tdCaption.InnerHtml = photo.Caption;
                        if (string.IsNullOrEmpty(folder))
                            imagePreview.Src = PhotoConstants.GalleryPath + photo.PhotoName;
                        else
                            imagePreview.Src = PhotoConstants.GalleryPath + folder + "/" + photo.PhotoName;
                        imagePreview.Alt = photo.Caption;
                        imagePreview.Attributes["onload"] = "javascript:ResizeImage(this);";
                    }

                    /*
                    using (SqlDataReader r = SqlHelper.ExecuteReader("Gallery_Get",
                        new SqlParameter("@GalleryID", int.Parse(id))
                    ))
                    {
                        if (r.Read())
                        {
                            string sCaption = r["Caption"].ToString();

                            tdCaption.InnerHtml = sCaption;
                            if (string.IsNullOrEmpty(folder))
                                imagePreview.Src = PhotoConstants.GalleryPath + r["ImageURL"];
                            else
                                imagePreview.Src = PhotoConstants.GalleryPath + folder + "/" + r["ImageURL"];
                            imagePreview.Alt = sCaption;
                            imagePreview.Attributes["onload"] = "javascript:ResizeImage(this);";
                        }
                    }
                    */
                }
            }
        }
    }
}