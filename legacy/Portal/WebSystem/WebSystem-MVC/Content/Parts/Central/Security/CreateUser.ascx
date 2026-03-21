<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateUser.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Security.CreateUser" %>
<%@ Register Src="../Controls/UserProfileForm.ascx" TagName="UserProfileForm" TagPrefix="uc1" %>
<%@ Register Src="../Controls/ChangePasswordForm.ascx" TagName="ChangePasswordForm"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/UserRolesForm.ascx" TagName="UserRolesForm" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc5" %>
<div class="Header">
    Create New User</div>
<br />
<uc5:TabControl ID="TabControl1" runat="server" />
&nbsp;<asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="viewProfile" runat="server">
        <uc1:UserProfileForm ID="FormProfile" runat="server" />
        <table width="100%">
            <tr>
                <td align="left" class="ControlBox">
                    <asp:Button ID="cmdCancel" Width="85px" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
                        CausesValidation="False" Height="30px" />
                    <asp:Button ID="cmdUpdate" Width="85px" runat="server" Text="Next" OnClick="cmdUpdate_Click"
                        Height="30px" />
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="viewRoles" runat="server">
        <uc4:UserRolesForm ID="FormRoles" runat="server" />
        <table width="100%">
            <tr>
                <td align="left" class="ControlBox">
                    <asp:Button ID="cmdRolesBack" Width="85px" runat="server" Text="Back" OnClick="cmdCancel_Click"
                        CausesValidation="False" Height="30px" />
                    <asp:Button ID="cmdRolesNext" Width="85px" runat="server" Text="Next" OnClick="cmdUpdate_Click"
                        Height="30px" />
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="viewSecurity" runat="server">
        <uc2:ChangePasswordForm ID="FormSecurity" runat="server" />
        <table width="100%">
            <tr>
                <td align="left" class="ControlBox">
                    <asp:Button ID="cmdSecurityBack" Width="85px" runat="server" Text="Back" OnClick="cmdCancel_Click"
                        CausesValidation="False" Height="30px" />
                    <asp:Button ID="cmdFinish" Width="85px" runat="server" Text="Finish" OnClick="cmdUpdate_Click"
                        Height="30px" />
                </td>
            </tr>
        </table>
    </asp:View>
</asp:MultiView>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
    <br />
<asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>