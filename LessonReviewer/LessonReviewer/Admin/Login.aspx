<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs"
    Inherits="WCMS.LessonReviewer.Admin.Login" Title="Administrator Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Administrator Login</h2>
    <br />
    <div>
        <table border="0" cellpadding="2">
            <tr>
                <td align="left">
                    User Name:&nbsp;
                </td>
                <td align="left">
                    <asp:TextBox ID="txtUserName" ClientIDMode="Static" runat="server" Width="200px"
                        Columns="50" CssClass="login_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName"
                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    Password:&nbsp;
                </td>
                <td align="left">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="200px" Columns="50"
                        CssClass="login_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chkBypass" Text="For MCGI Portal Bypass" Checked="true" runat="server" />
                    <br />
                    <asp:CheckBox ID="chkBypassSeek" Text="Override Video Seek and Forward Restrictions" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" style="padding-top: 10px;">
                    <asp:Button ID="cmdLogin" CssClass="Command" Width="85px" runat="server" Text="Login"
                        OnClick="cmdLogin_Click" />&nbsp;<asp:Button ID="cmdCancel" CssClass="Command" Width="75px"
                            runat="server" Text="Cancel" OnClick="cmdCancel_Click" />
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
