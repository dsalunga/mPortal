<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebIdentity.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Tools.WebBindingController" %>

<h1 class="central page-header">
    Site Binding
</h1>
<table width="100%">
    <tr>
        <td width="125">Host Name:
        </td>
        <td>
            <asp:TextBox ID="txtHostHeader" CssClass="input" runat="server" Columns="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Protocol:
        </td>
        <td>
            <asp:DropDownList ID="cboProtocols" CssClass="input" runat="server">
                <asp:ListItem Selected="True" Text="http" Value="0"></asp:ListItem>
                <asp:ListItem Text="https" Value="1"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Port:
        </td>
        <td>
            <asp:TextBox ID="txtPort" runat="server" CssClass="input" Columns="8">80</asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>IP Address:
        </td>
        <td>
            <asp:TextBox ID="txtIP" runat="server" CssClass="input" Columns="20">*</asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>URL Path:
        </td>
        <td>
            <asp:TextBox ID="txtUrlPath" runat="server" CssClass="input" Columns="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Redirect URL:
        </td>
        <td>
            <asp:TextBox ID="txtRedirectUrl" runat="server" CssClass="input" Columns="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Site:
        </td>
        <td>
            <asp:DropDownList ID="cboSites" CssClass="input" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" Text="Update" OnClick="cmdUpdate_Click"
            CssClass="btn btn-primary" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
            CssClass="btn btn-default" />
    </div>
</div>
<asp:ValidationSummary ID="vsMessages" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
