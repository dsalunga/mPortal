<%@ Page Title="Configuration Manager" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="WCMS.LessonReviewer.Admin.Manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Configuration Manager</h2>
    <br />
    <div>
        <table border="0" cellpadding="2">
            <tr>
                <td>
                    Admin UserName
                </td>
                <td>
                    <asp:TextBox ID="txtAdminUserName" runat="server" Columns="40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Admin Password
                </td>
                <td>
                    <asp:TextBox ID="txtAdminPassword" runat="server" Columns="40" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chkIntranetMode" Text="Intranet Mode (Disable redirection to MCGI Portal even if available)"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chkAutoBypassUrl" Text="Enable Auto-Bypass URL (Enables Intranet Mode via a URL)"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    PortalAjaxHandlerUrl
                </td>
                <td>
                    <asp:TextBox ID="txtPortalAjaxHandlerUrl" runat="server" Columns="70"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    PortalMakeUpHomeUrl
                </td>
                <td>
                    <asp:TextBox ID="txtPortalMakeUpHomeUrl" runat="server" Columns="70"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    MCGI.MakeUp.BaseFolder
                </td>
                <td>
                    <asp:TextBox ID="txtBaseFolder" runat="server" Columns="70"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    MCGI.MakeUp.BaseHttp
                </td>
                <td>
                    <asp:TextBox ID="txtBaseHttp" runat="server" Columns="70"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" style="padding-top: 10px;">
                    <asp:Button ID="cmdUpdate" CssClass="Command" Width="85px" runat="server" Text="Update"
                        OnClick="cmdUpdate_Click" />&nbsp;<asp:Button ID="cmdCancel" CssClass="Command" Width="75px"
                            runat="server" Text="Log Out" OnClick="cmdCancel_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="color: red">
                    <asp:Literal ID="lblMsg" runat="server" EnableViewState="False"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
