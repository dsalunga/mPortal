<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupEdit.ascx.cs" Inherits="WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList.GroupEdit" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Register Src="Controls/WebGroupTab.ascx" TagName="WebGroupTab" TagPrefix="uc1" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        cmdParentBrowse.Attributes["onclick"] = string.Format("ShowAccountBrowser('{0}', {1}, 0);", txtParent.ClientID, WebObjects.WebGroup);
        //cmdMentorBrowse.Attributes["onclick"] = string.Format("ShowAccountBrowser('{0}', {1}, 0);", txtMentors.ClientID, WebObjects.WebGroup);
        //cmdConductorBrowse.Attributes["onclick"] = string.Format("ShowAccountBrowser('{0}', {1}, 0);", txtConductors.ClientID, WebObjects.WebGroup);
        cmdOwnerBrowse.Attributes["onclick"] = string.Format("ShowAccountBrowser('{0}', {1}, 0);", txtOwner.ClientID, WebObjects.WebUser);

        base.Page_Load(sender, e);
    }
    
</script>

<uc1:webgrouptab id="WebGroupTab1" runat="server" selectedtab="GroupOverview" />
<asp:HiddenField ID="hGroupId" runat="server" Value="-1" />
<asp:HiddenField ID="hManagerGroupId" runat="server" Value="-1" />
<div class="Header" runat="server" id="lblHeader"></div>
<div class="form-horizontal">
    <%--<div class="control-group">
        <div class="controls">
            <h3>Edit Group</h3>
        </div>
    </div>--%>
    <div class="control-group">
        <label class="control-label" for="txtName">
            NAME OF GROUP<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                ErrorMessage="NAME OF GROUP" ForeColor="Red">*</asp:RequiredFieldValidator></label>
        <div class="controls">
            <asp:TextBox ID="txtName" runat="server" CssClass="col-md-4" placeholder="NAME OF GROUP"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtParent">ZONE/DISTRICT/DIVISION<asp:RequiredFieldValidator ID="rfvLocale" runat="server" ControlToValidate="txtParent"
                ErrorMessage="ZONE/DISTRICT/DIVISION" ForeColor="Red">*</asp:RequiredFieldValidator></label>
        <div class="controls">
            <asp:TextBox ID="txtParent" ClientIDMode="Static" runat="server" Columns="40" Text="INTERNATIONAL"></asp:TextBox>
            <input runat="server" id="cmdParentBrowse" class="btn btn-default" type="button" value="Browse..." />
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtOwner">ASSIGNED COUNCILLOR</label>
        <div class="controls">
            <asp:TextBox ID="txtOwner" ClientIDMode="Static" runat="server" Columns="40"></asp:TextBox>
            <input runat="server" id="cmdOwnerBrowse" class="btn btn-default" type="button" value="Browse..." />
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtMentors">MENTORS</label>
        <div class="controls">
            <asp:TextBox ID="txtMentors" ClientIDMode="Static" runat="server" CssClass="col-md-4" Columns="40"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtConductors">CONDUCTORS</label>
        <div class="controls">
            <asp:TextBox ID="txtConductors" ClientIDMode="Static" runat="server" CssClass="col-md-4" Columns="40"></asp:TextBox><%--&nbsp;<em>(to be improved)</em>--%>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtDescription">BRIEF HISTORY</label>
        <div class="controls">
            <asp:TextBox ID="txtDescription" runat="server" MaxLength="999" CssClass="col-md-5" Rows="8" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <%--<div class="control-group">
        <div class="controls">
            <h3>Misc. Details</h3>
        </div>
    </div>--%>
    <br />
    <div class="control-group">
        <label class="control-label" for="txtManagers">GROUP MANAGERS</label>
        <div class="controls">
            <asp:TextBox ID="txtManagers" ClientIDMode="Static" runat="server" CssClass="col-md-4" Columns="40"></asp:TextBox>
        </div>
    </div>
    <%--<div class="control-group">
        <label class="control-label" for="txtOfficers">OFFICERS</label>
        <div class="controls">
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtActivities">ACTIVITIES</label>
        <div class="controls">
        </div>
    </div>--%>
    <%--<div class="control-group" runat="server" id="panelUpdatedOn" visible="false">
        <label class="control-label">UPDATED ON</label>
        <div class="controls">
            <strong runat="server" id="lblUpdatedOn"></strong>
        </div>
    </div>--%>
    <div class="control-group">
        <div class="controls">
            <asp:Button ID="cmdUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="cmdUpdate_Click" />
            <a href="#" id="linkCancel" runat="server" class="btn btn-default">Cancel</a>
            <%--<asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="cmdCancel_Click"
                CausesValidation="False" />--%>
        </div>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:" ShowMessageBox="True" ShowSummary="False" />
</div>
