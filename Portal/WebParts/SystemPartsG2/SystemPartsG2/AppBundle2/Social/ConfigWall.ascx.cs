﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Social;
using WCMS.Framework.Core;


using WCMS.WebSystem.Controls;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Social
{
    public partial class ConfigWall : System.Web.UI.UserControl
    {
        protected TabControl TabControl1;
        protected string PART_NAME = "Social";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cmdBrowse.OnClientClick = string.Format("BrowseLink('{0}',{1}); return false;", txtNavigateURL.ClientID, WPart.Get(PART_NAME).Id);

                var query = new WQuery(this);
                var key = ObjectKey.TryCreate(query);

                // Subscription Mode
                var mode = WebParameter.Get(key.ObjectId, key.RecordId, SocialConstants.ModeKey);
                if (mode != null)
                    cboSubscriptionMode.SelectedValue = mode.Value;

                // Group-specific
                var group = WebParameter.Get(key.ObjectId, key.RecordId, SocialConstants.GroupKey);
                if (group != null)
                    txtGroupId.Text = group.Value;

                var param = WebParameter.Get(key.ObjectId, key.RecordId, SocialConstants.UsePageParameterKey);
                if (param != null)
                    chkUsePageParameter.Checked = param.Value == "1";

                txtIgnoreGroups.Text = WebParameter.GetStringValue(key.ObjectId, key.RecordId, SocialConstants.IgnoreGroupsKey);

                TabControl1.AddTab("tabConfig", "General");
                TabControl1.AddTab("tabSubscribe", "Subscriptions");
            }
        }

        protected void TabControl1_SelectedTabChanged(object sender, TabEventArgs e)
        {
            switch (e.TabName)
            {
                case "tabConfig":
                    MultiView1.SetActiveView(viewConfig);
                    break;

                case "tabSubscribe":
                    MultiView1.SetActiveView(viewSubscriptions);
                    break;
            }
        }


        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            Update();

            lblStatus.Text = "Update Successful";
        }

        protected void cmdAddFull_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            string pageUrl = txtNavigateURL.Text.Trim();

            if (!string.IsNullOrEmpty(pageUrl))
            {
                var page = WebRewriter.ResolvePage(pageUrl);
                if (page != null)
                {
                    var item = new WebSubscription();
                    item.ObjectId = context.ObjectId;
                    item.RecordId = context.RecordId;
                    item.PageId = page.Id;
                    item.PartId = WPart.Get(PART_NAME).Id;
                    item.Allow = 1;
                    item.Update();

                    txtNavigateURL.Text = string.Empty;
                    GridView1.DataBind();
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            string selected = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(selected))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(selected);
                foreach (var id in ids)
                    WebSubscription.Provider.Delete(id);

                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case WConstants.CustomDeleteCmd:
                    if (id > 0)
                    {
                        WebSubscription.Provider.Delete(id);
                        GridView1.DataBind();
                    }
                    break;

                case "Custom_Manage":
                    if (id > 0)
                    {
                        var sub = WebSubscription.Provider.Get(id);
                        if (sub != null)
                        {
                            WContext context = new WContext(this);
                            context.Set(WebColumns.PageId, sub.PageId);
                            context.Remove(WebColumns.PageElementId);
                            context.Remove(WebColumns.TemplatePanelId);

                            context.Redirect();
                        }
                    }
                    break;
            }
        }

        public DataSet Select()
        {
            WPage page = null;

            var pair = WHelper.GetObjectStruct();
            var items = WebSubscription.Provider.GetList(pair.ObjectId, pair.RecordId, WPart.Get(PART_NAME).Id, -1, -1);
            var subItems = from i in items
                           select new
                           {
                               i.Id,
                               PageUrl = (page = WPage.Get(i.PageId)).BuildRelativeUrl(),
                               PageName = page.Name
                           };

            return DataHelper.ToDataSet(subItems);
        }

        #region IUpdatable Members

        public bool Update()
        {
            QueryParser query = new QueryParser(this);
            ObjectKey key = ObjectKey.TryCreate(query);

            // Subscription Mode
            WebParameter mode = WebParameter.Get(key.ObjectId, key.RecordId, SocialConstants.ModeKey);
            if (mode == null)
            {
                mode = new WebParameter();
                mode.Name = SocialConstants.ModeKey;
                mode.ObjectId = key.ObjectId;
                mode.RecordId = key.RecordId;
            }
            mode.Value = cboSubscriptionMode.SelectedValue;
            mode.Update();

            // Group-specific
            WebParameter group = WebParameter.Get(key.ObjectId, key.RecordId, SocialConstants.GroupKey);
            if (group == null)
            {
                group = new WebParameter();
                group.Name = SocialConstants.GroupKey;
                group.ObjectId = key.ObjectId;
                group.RecordId = key.RecordId;
            }
            group.Value = txtGroupId.Text.Trim();
            group.Update();

            // Group-specific
            WebParameter item = WebParameter.Get(key.ObjectId, key.RecordId, SocialConstants.UsePageParameterKey);
            if (item == null)
            {
                item = new WebParameter();
                item.Name = SocialConstants.UsePageParameterKey;
                item.ObjectId = key.ObjectId;
                item.RecordId = key.RecordId;
            }
            item.Value = chkUsePageParameter.Checked ? "1" : "0";
            item.Update();

            // Ignore Groups
            item = WebParameter.Get(key.ObjectId, key.RecordId, SocialConstants.IgnoreGroupsKey);
            if (item == null)
            {
                item = new WebParameter();
                item.Name = SocialConstants.IgnoreGroupsKey;
                item.ObjectId = key.ObjectId;
                item.RecordId = key.RecordId;
            }
            item.Value = txtIgnoreGroups.Text.Trim();
            item.Update();

            return true;
        }

        public string UpdateText { get; set; }
        public string CancelText { get; set; }

        #endregion
    }
}