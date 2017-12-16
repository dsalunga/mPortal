using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;

using WCMS.WebSystem.Controls;

using WCMS.Framework;
using WCMS.WebSystem.ViewModel;

using WCMS.WebSystem.WebParts.Central.Controls;

namespace WCMS.WebSystem.WebParts.Menu
{
    public partial class MENU_CMS_Menu_01 : UserControl, IUpdatable
    {
        protected ParameterSetSelector ParameterSetSelector1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new QueryParser(this);
                var key = ObjectKey.TryCreate(query);

                cboMenu.DataSource = MenuEntity.Provider.GetList();
                cboMenu.DataBind();

                var item = MenuObject.Provider.Get(key.ObjectId, key.RecordId);
                if (item != null)
                {
                    WebHelper.SetCboValue(cboMenu, item.MenuId);
                    ParameterSetSelector1.ParameterSetId = item.ParameterSetId;
                    cboRenderMode.SelectedValue = item.RenderMode.ToString();

                    //cboOrientation.SelectedValue = item.Horizontal.ToString();

                    //txtWidth.Text = item.Width.ToString();
                    //txtHeight.Text = item.Height.ToString();
                }

                //query.Set(QueryParser.LoadKey, "CCMS_Menu_08.ascx");
                //linkManageMenu.HRef = query.BuildQuery();
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            Update();
            lblStatus.Text = "Update Successful";
        }

        #region IUpdatable Members

        public bool Update()
        {
            var query = new QueryParser(this);
            var key = ObjectKey.TryCreate(query);

            //int width = DataHelper.GetInt32(txtWidth.Text.Trim());
            //int height = DataHelper.GetInt32(txtHeight.Text.Trim());
            //int isHorizontal = DataHelper.GetInt32(cboOrientation.SelectedValue);

            int renderMode = DataHelper.GetInt32(cboRenderMode.SelectedValue);
            int menuId = DataHelper.GetId(cboMenu.SelectedValue);

            var item = MenuObject.Provider.Get(key.ObjectId, key.RecordId);
            if (item == null)
            {
                item = new MenuObject();
                item.ObjectId = key.ObjectId;
                item.RecordId = key.RecordId;
            }

            //item.Height = height;
            //item.Width = width;
            //item.Horizontal = isHorizontal;

            item.MenuId = menuId;
            item.ParameterSetId = ParameterSetSelector1.ParameterSetId;
            item.RenderMode = renderMode;
            item.Update();

            return true;
        }

        public string UpdateText { get; set; }
        public string CancelText { get; set; }

        #endregion
    }
}