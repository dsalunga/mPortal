using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.ViewModel;
using WCMS.WebSystem.WebParts.Central.Controls;

namespace WCMS.WebSystem.WebParts.Photo
{
    public partial class ConfigAnimatedBanner : UserControl, IUpdatable
    {
        protected ParameterSetSelector ParameterSetSelector1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;
                var albumId = DataHelper.GetId(element.GetParameterValue("AlbumId"));
                var renderMode = DataHelper.GetInt32(element.GetParameterValue("RenderMode"), 0);

                cboAlbum.DataSource = Album.Provider.GetList();
                cboAlbum.DataBind();

                if (albumId > 0)
                    WebHelper.SetCboValue(cboAlbum, albumId);

                if (renderMode >= 0)
                    WebHelper.SetCboValue(cboAlbum, renderMode);

                var set = context.GetParameterSet();
                if (set != null)
                    ParameterSetSelector1.ParameterSetId = set.Id;
            }
        }

        #region IUpdatable Members

        public bool Update()
        {
            var context = new WContext(this);
            var element = context.Element;
            int renderMode = DataHelper.GetInt32(cboRenderMode.SelectedValue);
            int albumId = DataHelper.GetId(cboAlbum.SelectedValue);
            var setId = ParameterSetSelector1.ParameterSetId;

            // AlbumId
            var param = element.GetOrCreateParameter("AlbumId");
            param.Value = albumId.ToString();
            param.Update();

            // RenderMode
            param = element.GetOrCreateParameter("RenderMode");
            param.Value = renderMode.ToString();
            param.Update();

            // Template
            param = element.GetOrCreateParameter(WConstants.ParameterSetKey);
            param.Value = setId.ToString();
            param.Update();

            return true;
        }

        public string UpdateText { get; set; }
        public string CancelText { get; set; }

        #endregion
    }
}