using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Social;
using WCMS.Framework.Utilities;

using WCMS.WebSystem.WebParts.Social.ViewModel;

namespace WCMS.WebSystem.WebParts.Social
{
    public partial class WallController : System.Web.UI.UserControl
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int IsContentManager { get; set; }

        private WContext context;

        public WallController()
        {
            ObjectId = -1;
            RecordId = -1;
            IsContentManager = 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Account filter
            if (!Page.IsPostBack)
            {
                context = new WContext(this);
                
                ObjectId = context.ObjectId;
                RecordId = context.RecordId;

                if (WSession.Current.IsLoggedIn)
                {
                    if (!context.IsUserMgmtPermitted(Permissions.ManageContent))
                        panelNewWallPost.Visible = false;
                    else
                        IsContentManager = 1;
                }
                else
                {
                    panelNewWallPost.Visible = false;
                }

                UpdateWall();
            }
        }

        private void UpdateWall()
        {
            var element = context.Element;
            var wallOwner = element.GetParameterValue("WallOwner");

            if (!string.IsNullOrEmpty(wallOwner))
            {
                var key = new ObjectKey(wallOwner);
                if (key.HasValue)
                {
                    var updates = WallUpdate.Provider.GetList(key.ObjectId, key.RecordId);
                    foreach (var update in updates)
                    {
                        var plugin = WallPlugin.Provider.GetByEventType(update.EventTypeId);
                        if (plugin != null)
                        {
                            var item = LoadControl(plugin.FileName) as WallEventViewModel; // (WallEventViewModel)Activator.CreateInstance(plugin.TypeObject);
                            if (item != null)
                            {
                                item.WallUpdate = update;
                                item.UserIsContentManager = IsContentManager == 1;
                                item.PartContext = context;
                                item.RenderUI();

                                panelWallUpdates.Controls.Add(item);
                            }
                        }
                    }
                }
            }
        }

        protected void cmdPost_Click(object sender, EventArgs e)
        {
            if (WSession.Current.IsLoggedIn)
            {
                var post = txtNewPost.Value.Trim();
                if (!string.IsNullOrEmpty(post))
                {
                    txtNewPost.Value = string.Empty;

                    var item = new GenericWallEvent(post);
                    item.Update();

                    UpdateWall();
                }
            }
        }
    }
}