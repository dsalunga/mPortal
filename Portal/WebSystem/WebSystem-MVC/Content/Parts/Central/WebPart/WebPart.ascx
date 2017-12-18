<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPart.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.WebPartController" %>
<h1 class="central page-header">Web Part</h1>
<table width="100%">
    <tr>
        <td width="125">Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
            ErrorMessage="Name">*</asp:RequiredFieldValidator></td>
        <td>
            <asp:TextBox ID="txtName" runat="server" Columns="75" CssClass="input"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Folder Name:<asp:RequiredFieldValidator ID="rfvIdentity" runat="server" ControlToValidate="txtIdentity"
            ErrorMessage="Folder Name">*</asp:RequiredFieldValidator></td>
        <td>
            <asp:TextBox ID="txtIdentity" runat="server" Columns="75" CssClass="input"></asp:TextBox></td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chkActive" CssClass="aspnet-checkbox" runat="server" Text="Active" Checked="true" /></td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" Text="Update" OnClick="cmdUpdate_Click" CssClass="btn btn-primary" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" CssClass="btn btn-default" />
    </div>
</div>
