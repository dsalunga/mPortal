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

using WCMS.Framework;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Menu
{
    public partial class ConfigDynamicMenuV2 : UserControl, IUpdatable
    {
        #region Generated Code
        /// <summary>
        /// ParameterSetSelector1 control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::WCMS.WebSystem.WebParts.Central.Controls.ParameterSetSelector ParameterSetSelector1;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WContext query = new WContext(this);
                var element = query.Element;

                cboMenu.DataSource = MenuEntity.Provider.GetList();
                cboMenu.DataBind();

                var set = element.GetParameterSet();

                var menuId = DataHelper.GetId(element.GetParameterValue(ParameterKeys.MenuId));
                var parameterSetId = set != null ? set.Id : -1;

                if (menuId > 0)
                    if (cboMenu.Items.FindByValue(menuId.ToString()) != null)
                        cboMenu.SelectedValue = menuId.ToString();

                // ParameterSet
                if (parameterSetId > 0)
                    ParameterSetSelector1.ParameterSetId = parameterSetId;

                query.Set(WConstants.Load, "AdminMenu.ascx");
                linkManageMenu.HRef = query.BuildQuery();
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
            WContext query = new WContext(this);
            var element = query.Element;

            var set = ParameterSetSelector1.GetParameterSet();

            var param = element.GetOrCreateParameter(ParameterKeys.MenuId);
            param.Value = DataHelper.GetId(cboMenu.SelectedValue).ToString();
            param.Update();

            param = element.GetOrCreateParameter(WConstants.ParameterSetKey);
            param.Value = set != null ? set.Name : string.Empty;
            param.Update();

            return true;
        }

        public string UpdateText { get; set; }
        public string CancelText { get; set; }

        #endregion
    }
}