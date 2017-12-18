<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Security.UserProfile" %>
<%@ Register Src="../Controls/UserProfileForm.ascx" TagName="UserProfileForm" TagPrefix="uc1" %>
<%@ Register Src="../Controls/ChangePasswordForm.ascx" TagName="ChangePasswordForm"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/UserRolesForm.ascx" TagName="UserRolesForm" TagPrefix="uc4" %>
<asp:HiddenField ID="hNewUserId" runat="server" Value="" />
<asp:HiddenField ID="hGroupFilter" runat="server" Value="" />
<asp:HiddenField ID="hDataEntry" runat="server" Value="0" />
<div id="divGeneral" runat="server">
    <div runat="server" id="panelGeneralHeading">
        <h1 class="central page-header" id="linkHeader" runat="server">New User</h1>
    </div>
    <uc1:UserProfileForm ID="FormProfile" runat="server" />
</div>
<div id="panelSecurity" runat="server">
    <br />
    <h3 class="central">Security</h3>
    <uc2:ChangePasswordForm ID="ChangePasswordForm1" runat="server" />
</div>
<br />
<div class="control-box">
    <div>
        <div class="btn-group">
            <button type="submit" id="cmdUpdate" class="btn btn-default" runat="server"
                onserverclick="cmdUpdate_Click">
                Update</button>
            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                <span class="caret"></span>
                <span class="sr-only">Toggle Dropdown</span>
            </button>
            <ul class="dropdown-menu" role="menu">
                <li>
                    <asp:LinkButton ID="cmdUpdateAddNew"
                        runat="server" Text="Update & Add New" OnClick="cmdUpdateAddNew_Click" ToolTip="Update and Add new user"></asp:LinkButton></li>
            </ul>
        </div>
        <asp:Button ID="cmdCancel" runat="server" Text="Close" OnClick="cmdCancel_Click"
            CausesValidation="False" CssClass="btn btn-default" />
    </div>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
<br />
<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
