using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.WebSystem.Controls;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Security
{
    public partial class WebSecurity : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var key = WHelper.GetObjectKey();
                var selectedTab = DataHelper.Get(Request, "SelectedTab");

                Action permitted = () =>
                {
                    bool publicFirst = false;

                    if (key.ObjectId == WebObjects.WebSite ||
                        key.ObjectId == WebObjects.WebPage ||
                        key.ObjectId == WebObjects.WebMasterPage)
                    {
                        TabControl1.AddTab("tabPublicAccount", "Public: Accounts");
                        TabControl1.AddTab("tabIP", "Public: IP Addresses");
                        publicFirst = true;
                    }
                    TabControl1.AddTab("tabAccount", "Mgmt: Accounts");

                    cblPermisssions.DataSource = WebPermissionSet.GetPermissions(key.ObjectId, key.RecordId, 0);
                    cblPermisssions.DataBind();

                    cblPublicPermissions.DataSource = WebPermissionSet.GetPermissions(key.ObjectId, key.RecordId, 1);
                    cblPublicPermissions.DataBind();

                    hObjectId.Value = key.ObjectId.ToString();
                    hRecordId.Value = key.RecordId.ToString();

                    // Accounts Grid
                    if (publicFirst)
                    {
                        gridPublicAccounts.DataBind();
                    }
                    else
                    {
                        MultiView1.SetActiveView(viewAccounts);
                        gridAccounts.DataBind();
                    }

                    txtIPAddress.Text = Request.UserHostAddress;

                    if (!string.IsNullOrEmpty(selectedTab))
                        TabControl1.SelectedTab = selectedTab;
                };

                Action accessDenied = () =>
                {
                    MultiView1.Visible = false;
                    TabControl1.Visible = false;
                    lblStatus.Text = "Access denied.";
                };

                var record = key.Record as PublicSecurableObject;

                if (record != null)
                    record.ExecuteUserMgmtActions(accessDenied, accessDenied, permitted);
                else
                    permitted();
            }
        }

        protected void TabControl1_SelectedTabChanged(object oSender, TabEventArgs args)
        {
            switch (args.TabName)
            {
                case "tabAccount":
                    MultiView1.SetActiveView(viewAccounts);
                    gridAccounts.DataBind();
                    break;

                case "tabPublicAccount":
                    MultiView1.SetActiveView(viewPublicAccounts);
                    gridPublicAccounts.DataBind();
                    break;

                case "tabIP":
                    MultiView1.SetActiveView(viewIPAddresses);
                    gridIPAddresses.DataBind();
                    break;
            }
        }

        public DataSet SelectMgmtAccess(int recordId, int objectId)
        {
            return WebObjectSecurity.GetDataSet(objectId, recordId, -1, -1, 0);
        }

        public DataSet SelectPublicAccess(int recordId, int objectId)
        {
            return WebObjectSecurity.GetDataSet(objectId, recordId, -1, -1, 1);
        }

        public DataSet SelectIPAccess(int objectId, int recordId)
        {
            var accounts = WebObjectIPAddress.GetList(objectId, recordId);
            return DataHelper.ToDataSet(accounts);
        }

        protected void gridAccounts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    SetPermissions(id);
                    break;

                case "Custom_Delete":
                    WebObjectSecurity.Delete(id);
                    gridAccounts.DataBind();

                    break;
            }
        }

        private void SetPermissions(List<WebObjectSecurity> items)
        {
            lblObjectName.InnerText = "Multiple Accounts";

            // Get all object permissions
            foreach (ListItem item in cblPermisssions.Items)
                item.Selected = false;

            hidSetId.Value = GetDelimitedIds(items);

            MultiView1.SetActiveView(viewAccountSecurity);
        }

        private void SetPermissions(int id)
        {
            lblObjectName.InnerText = GetObjectName(id);

            // Get all object permissions
            var permissions = WebObjectSecurityPermission.GetList(id);
            foreach (ListItem item in cblPermisssions.Items)
                item.Selected = (permissions.FirstOrDefault(p => p.PermissionId.ToString() == item.Value) != null);

            hidSetId.Value = id.ToString();

            MultiView1.SetActiveView(viewAccountSecurity);
        }

        private string GetDelimitedIds(List<WebObjectSecurity> items)
        {
            var sb = new StringBuilder();
            sb.Append(items.First().Id);

            if (items.Count > 1)
                for (int i = 1; i < items.Count; i++)
                    sb.AppendFormat(",{0}", items[i].Id);

            return sb.ToString();
        }

        private string GetObjectName(int objectSecurityId)
        {
            var item = WebObjectSecurity.Get(objectSecurityId);
            if (item != null)
                return item.SecurityEntity.Name;

            return "#NULL#";
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(viewAccounts);
        }

        protected void cmdOK_Click(object sender, EventArgs e)
        {
            if (hidSetId.Value.Contains(","))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(hidSetId.Value);

                // Get all object permissions
                foreach (var id in ids)
                {
                    var permissions = WebObjectSecurityPermission.GetList(id);
                    foreach (ListItem item in cblPermisssions.Items)
                    {
                        var objSecPerm = permissions.FirstOrDefault(p => p.PermissionId.ToString() == item.Value);
                        if (item.Selected)
                        {
                            if (objSecPerm == null)
                            {
                                objSecPerm = new WebObjectSecurityPermission();
                                objSecPerm.PermissionId = DataHelper.GetId(item.Value);
                                objSecPerm.ObjectSecurityId = id;
                                objSecPerm.Update();
                            }
                        }
                    }
                }
            }
            else
            {
                int id = DataHelper.GetId(hidSetId.Value);

                // Get all object permissions
                var permissions = WebObjectSecurityPermission.GetList(id);
                foreach (ListItem item in cblPermisssions.Items)
                {
                    var objSecPerm = permissions.FirstOrDefault(p => p.PermissionId.ToString() == item.Value);
                    if (item.Selected)
                    {
                        if (objSecPerm == null)
                        {
                            objSecPerm = new WebObjectSecurityPermission();
                            objSecPerm.PermissionId = DataHelper.GetId(item.Value);
                            objSecPerm.ObjectSecurityId = id;
                            objSecPerm.Update();
                        }
                    }
                    else
                    {
                        if (objSecPerm != null)
                            objSecPerm.Delete();
                    }
                }
            }

            MultiView1.SetActiveView(viewAccounts);
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            ObjectKey key = WHelper.GetObjectKey();
            var existing = WebObjectSecurity.Provider.GetList(key.ObjectId, key.RecordId, -1, -1, 0);
            bool accountAdded = false;
            int onlyAddedId = -1;
            List<WebObjectSecurity> addedItems = new List<WebObjectSecurity>();

            string newAccounts = txtAdd.Text.Trim();
            if (!string.IsNullOrEmpty(newAccounts))
            {
                var accountList = DataHelper.ParseDelimitedStringToList(newAccounts, AccountConstants.AccountDelimiter); // newAccounts.Split(AccountConstants.AccountDelimiter).AsEnumerable();
                if (accountList.Count > 0)
                {
                    foreach (string account in accountList)
                    {
                        int objectId = -1;
                        int securityRecordId = -1;

                        if (WebUser.IsValidUniqueName(account))
                        {
                            objectId = WebObjects.WebUser;
                            WebUser user = WebUser.GetByUniqueName(account);

                            if (user == null)
                                continue;

                            securityRecordId = user.Id;
                        }
                        else if (WebGroup.IsValidUniqueName(account)) // Should support GROUP\
                        {
                            objectId = WebObjects.WebGroup;
                            WebGroup group = WebGroup.GetByUniqueName(account);

                            if (group == null)
                                continue;

                            securityRecordId = group.Id;
                        }
                        else
                        {
                            // Should we support this?
                            objectId = WebObjects.WebGroup;
                            NamedWebObject groupOrUser = WebGroup.Get(account);

                            if (groupOrUser == null)
                            {
                                groupOrUser = WebUser.Get(account);
                                objectId = WebObjects.WebUser;
                            }

                            if (groupOrUser == null)
                                continue;

                            securityRecordId = groupOrUser.Id;
                        }


                        if (existing.FirstOrDefault(ex =>
                            ex.ObjectId == key.ObjectId
                            && ex.RecordId == key.RecordId
                            && ex.SecurityObjectId == objectId
                            && ex.SecurityRecordId == securityRecordId) == null)
                        {
                            WebObjectSecurity item = new WebObjectSecurity();
                            item.ObjectId = key.ObjectId;
                            item.RecordId = key.RecordId;
                            item.SecurityObjectId = objectId;
                            item.SecurityRecordId = securityRecordId;
                            item.Update();

                            addedItems.Add(item);

                            if (onlyAddedId == -1)
                                onlyAddedId = item.Id;
                            else if (onlyAddedId > 0)
                                onlyAddedId = 0;
                        }

                        if (!accountAdded)
                            accountAdded = true;
                    }
                }
            }

            if (accountAdded)
            {
                txtAdd.Text = string.Empty;
                gridAccounts.DataBind();

                if (addedItems.Count > 1)
                    SetPermissions(addedItems);
                else if (onlyAddedId > 0)
                    SetPermissions(onlyAddedId);
            }
            else
            {
                lblStatus.Text = "Account does not exist.";
            }
        }

        protected void cmdAddIPAddress_Click(object sender, EventArgs e)
        {
            ObjectKey key = WHelper.GetObjectKey();
            var existing = WebObjectIPAddress.GetList(key.ObjectId, key.RecordId);

            string newIPAddresses = txtIPAddress.Text.Replace(" ", string.Empty);
            if (!string.IsNullOrEmpty(newIPAddresses))
            {
                var ipList = newIPAddresses.Split(WConstants.IPDelimiter).AsEnumerable();
                if (ipList.Count() > 0)
                {
                    foreach (string ipAddress in ipList)
                    {
                        if (!string.IsNullOrEmpty(ipAddress))
                        {
                            if (existing.FirstOrDefault(f =>
                                f.IPAddress.Equals(ipAddress) &&
                                f.ObjectId == key.ObjectId &&
                                f.RecordId == key.RecordId) == null)
                            {
                                WebObjectIPAddress item = new WebObjectIPAddress();
                                item.RecordId = key.RecordId;
                                item.ObjectId = key.ObjectId;
                                item.IPAddress = ipAddress;
                                item.Update();
                            }
                        }
                    }
                }
            }

            txtIPAddress.Text = string.Empty;
            gridIPAddresses.DataBind();
        }

        protected void gridIPAddresses_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Custom_Delete":
                    WebObjectIPAddress.Delete(id);
                    gridIPAddresses.DataBind();
                    break;
            }
        }

        protected void gridPublicAccounts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    PublicSetPermissions(id);
                    break;

                case "Custom_Delete":
                    WebObjectSecurity.Delete(id);
                    gridPublicAccounts.DataBind();

                    break;
            }
        }

        private void PublicSetPermissions(int id)
        {
            lblPublicObjectName.InnerText = GetObjectName(id);

            // Get all object permissions
            var permissions = WebObjectSecurityPermission.GetList(id);
            foreach (ListItem item in cblPublicPermissions.Items)
                item.Selected = (permissions.FirstOrDefault(p => p.PermissionId.ToString() == item.Value) != null);

            hidSetId.Value = id.ToString();
            MultiView1.SetActiveView(viewPublicAccountSecurity);
        }

        private void PublicSetPermissions(List<WebObjectSecurity> items)
        {
            lblPublicObjectName.InnerText = "Multiple Accounts";

            // Get all object permissions
            foreach (ListItem item in cblPublicPermissions.Items)
                item.Selected = false;

            hidSetId.Value = GetDelimitedIds(items);

            MultiView1.SetActiveView(viewPublicAccountSecurity);
        }

        protected void cmdPublicAdd_Click(object sender, EventArgs e)
        {
            ObjectKey key = WHelper.GetObjectKey();
            var existing = WebObjectSecurity.Provider.GetList(key.ObjectId, key.RecordId, -1, -1, 1);
            bool accountAdded = false;
            int onlyAddedId = -1;
            List<WebObjectSecurity> addedItems = new List<WebObjectSecurity>();

            string newAccounts = txtPublicAdd.Text.Trim();
            if (!string.IsNullOrEmpty(newAccounts))
            {
                var accountList = DataHelper.ParseDelimitedStringToList(newAccounts, AccountConstants.AccountDelimiter); // newAccounts.Split(AccountConstants.AccountDelimiter).AsEnumerable();
                if (accountList.Count > 0)
                {
                    foreach (string account in accountList)
                    {
                        int objectId = -1;
                        int securityRecordId = -1;

                        if (WebUser.IsValidUniqueName(account))
                        {
                            objectId = WebObjects.WebUser;
                            WebUser user = WebUser.GetByUniqueName(account);

                            if (user == null)
                                continue;

                            securityRecordId = user.Id;
                        }
                        else if (WebGroup.IsValidUniqueName(account)) // Should support GROUP\
                        {
                            objectId = WebObjects.WebGroup;
                            WebGroup group = WebGroup.GetByUniqueName(account);

                            if (group == null)
                                continue;

                            securityRecordId = group.Id;
                        }
                        else
                        {
                            // Should we support this?
                            objectId = WebObjects.WebGroup;
                            NamedWebObject groupOrUser = WebGroup.Get(account);

                            if (groupOrUser == null)
                            {
                                groupOrUser = WebUser.Get(account);
                                objectId = WebObjects.WebUser;
                            }

                            if (groupOrUser == null)
                                continue;

                            securityRecordId = groupOrUser.Id;
                        }


                        if (existing.FirstOrDefault(ex =>
                            ex.ObjectId == key.ObjectId
                            && ex.RecordId == key.RecordId
                            && ex.SecurityObjectId == objectId
                            && ex.SecurityRecordId == securityRecordId
                            && ex.Public == 1) == null)
                        {
                            WebObjectSecurity item = new WebObjectSecurity();
                            item.ObjectId = key.ObjectId;
                            item.RecordId = key.RecordId;
                            item.SecurityObjectId = objectId;
                            item.SecurityRecordId = securityRecordId;
                            item.Public = 1;
                            item.Update();

                            addedItems.Add(item);

                            if (onlyAddedId == -1)
                                onlyAddedId = item.Id;
                            else if (onlyAddedId > 0)
                                onlyAddedId = 0;
                        }

                        if (!accountAdded)
                            accountAdded = true;
                    }
                }
            }

            if (accountAdded)
            {
                txtPublicAdd.Text = string.Empty;
                gridPublicAccounts.DataBind();

                if (addedItems.Count > 1)
                    PublicSetPermissions(addedItems);
                else if (onlyAddedId > 0)
                    PublicSetPermissions(onlyAddedId);
            }
            else
            {
                lblStatus.Text = "Account does not exist.";
            }
        }

        protected void cmdPublicCancel_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(viewPublicAccounts);
        }

        protected void cmdPublicOK_Click(object sender, EventArgs e)
        {
            if (hidSetId.Value.Contains(","))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(hidSetId.Value);

                foreach (var id in ids)
                {
                    // Get all object permissions
                    var permissions = WebObjectSecurityPermission.GetList(id);
                    foreach (ListItem item in cblPublicPermissions.Items)
                    {
                        var objSecPerm = permissions.FirstOrDefault(p => p.PermissionId.ToString() == item.Value);
                        if (item.Selected)
                        {
                            if (objSecPerm == null)
                            {
                                objSecPerm = new WebObjectSecurityPermission();
                                objSecPerm.PermissionId = DataHelper.GetId(item.Value);
                                objSecPerm.ObjectSecurityId = id;
                                objSecPerm.Update();
                            }
                        }
                    }
                }
            }
            else
            {
                int id = DataHelper.GetId(hidSetId.Value);

                // Get all object permissions
                var permissions = WebObjectSecurityPermission.GetList(id);
                foreach (ListItem item in cblPublicPermissions.Items)
                {
                    var objSecPerm = permissions.FirstOrDefault(p => p.PermissionId.ToString() == item.Value);
                    if (item.Selected)
                    {
                        if (objSecPerm == null)
                        {
                            objSecPerm = new WebObjectSecurityPermission();
                            objSecPerm.PermissionId = DataHelper.GetId(item.Value);
                            objSecPerm.ObjectSecurityId = id;
                            objSecPerm.Update();
                        }
                    }
                    else
                    {
                        if (objSecPerm != null)
                            objSecPerm.Delete();
                    }
                }
            }

            MultiView1.SetActiveView(viewPublicAccounts);
        }
    }
}