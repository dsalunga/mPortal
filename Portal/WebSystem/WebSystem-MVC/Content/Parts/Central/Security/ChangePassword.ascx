<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Security.ChangePassword" %>
<%@ Register Src="../Controls/ChangePasswordForm.ascx" TagName="ChangePasswordForm"
    TagPrefix="uc2" %>
<h1 class="central page-header">
    Change Password
</h1>
<uc2:ChangePasswordForm ID="FormSecurity" runat="server" />
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" CssClass="btn btn-primary" runat="server" Text="Update" OnClick="cmdUpdate_Click"
            OnClientClick="return prepareAndSubmit();" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" CssClass="btn btn-default" />
    </div>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
<br />
<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
