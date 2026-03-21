<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.ChangePassword" %>
<%@ Register Src="../Central/Controls/ChangePasswordForm.ascx" TagName="ChangePasswordForm"
    TagPrefix="uc1" %>

<asp:HiddenField ID="hUserId" runat="server" Value="-1" />
<asp:HiddenField ID="hEmailDomainFilter" runat="server" Value="" />
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewUpdate" runat="server">
        <div class="grid-padding">
            <uc1:ChangePasswordForm ID="ChangePasswordForm1" runat="server" />
        </div>
        <div runat="server" id="panelError" visible="false">
            <div style="font-size: larger; color: White; background-color: Red; padding: 2px; display: inline;">
                <span runat="server" id="lblError" enableviewstate="false"></span>
            </div>
            <br />
            <br />
        </div>
        <div id="panelButtons">
            <asp:Button CssClass="btn btn-primary" ID="cmdSubmit" runat="server" Text="Update"
                OnClick="cmdSubmit_Click" ClientIDMode="Static" OnClientClick="prepareAndSubmit();" />
            &nbsp;
            <asp:Button CssClass="btn btn-default" ID="cmdCancel" runat="server" Text="Cancel"
                OnClick="cmdCancel_Click" Visible="False" />
        </div>
        <br />
        <div runat="server" clientidmode="static" id="panelUpdateNote">
        </div>
        <span style="padding: 3px; background-color: Yellow" id="panelAlert">Updating your password may take a little while.</span>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
            ShowMessageBox="True" ShowSummary="False" />
    </asp:View>
    <asp:View ID="viewDisplayNote" runat="server">
        <div runat="server" clientidmode="static" id="panelPageNote">
            Password update not allowed.
        </div>
    </asp:View>
</asp:MultiView>