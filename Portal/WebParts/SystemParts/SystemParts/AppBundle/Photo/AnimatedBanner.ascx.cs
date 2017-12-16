using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Photo
{
    public partial class AnimatedBanner : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WContext context = new WContext(this);

            var element = context.Element;

            var albumId = DataHelper.GetId(element.GetParameterValue("AlbumId"));
            var renderMode = DataHelper.GetInt32(element.GetParameterValue("RenderMode"), 0);

            var set = context.GetParameterSet();
            var album = Album.Provider.Get(albumId);

            if (set != null && album != null)
            {
                var containerTemplate = set.GetParameterValue(TemplateConstants.ContainerTemplate);
                var itemTemplate = set.GetParameterValue(TemplateConstants.ItemTemplate);

                var photos = album.Photos;
                StringBuilder builder = new StringBuilder();
                NamedValueProvider provider = null;

                foreach (var photo in photos)
                {
                    provider = new NamedValueProvider();
                    provider.Add("PhotoUrl", photo.RelativePhotoPath);

                    builder.Append(Substituter.Substitute(itemTemplate, provider));
                }

                provider = new NamedValueProvider();
                provider.Add(Substituter.DefaultKey, builder.ToString());

                lContent.Text = Substituter.Substitute(string.IsNullOrEmpty(containerTemplate) ? Substituter.DefaultContentToken : containerTemplate, provider);
            }
        }
    }
}