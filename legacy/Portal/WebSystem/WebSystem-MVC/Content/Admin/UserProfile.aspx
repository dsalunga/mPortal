<%@ Page Language="c#" Inherits="WCMS.Web.cmsadmin.UserProfile" ValidateRequest="False"
    MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" %>

<%@ Register Src="Controls/UserProfileForm.ascx" TagName="UserProfileForm" TagPrefix="uc1" %>
<%@ Register Src="Controls/ChangePasswordForm.ascx" TagName="ChangePasswordForm"
    TagPrefix="uc2" %>
<%@ Register Src="Controls/UserRolesForm.ascx" TagName="UserRolesForm" TagPrefix="uc4" %>
<%@ Register Src="../Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Header">
        User Profile</div>
    <br />
    <uc5:TabControl ID="TabControl1" runat="server" />
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="viewProfile" runat="server">
            <uc1:UserProfileForm ID="FormProfile" runat="server" />
        </asp:View>
        <asp:View ID="viewSecurity" runat="server">
            <uc2:ChangePasswordForm ID="FormSecurity" runat="server" />
        </asp:View>
        <asp:View ID="viewRoles" runat="server">
            <uc4:UserRolesForm ID="FormRoles" runat="server" />
        </asp:View>
    </asp:MultiView>
    <table width="100%">
        <tr>
            <td colspan="2" align="left" class="ControlBox">
                <asp:Button ID="cmdUpdate" Width="85px" runat="server" Text="Update" OnClick="cmdUpdate_Click"
                    Height="30px" />
                <asp:Button ID="cmdCancel" Width="85px" runat="server" Text="Close" OnClick="cmdCancel_Click"
                    CausesValidation="False" Height="30px" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
        ShowMessageBox="True" ShowSummary="False" />
</asp:Content>
