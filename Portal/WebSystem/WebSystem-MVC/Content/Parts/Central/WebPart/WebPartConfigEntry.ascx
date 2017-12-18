<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPartConfigEntry.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebPartConfigEntry" %>
<h1 class="central page-header">
    Web Part Configuration Control
</h1>
<table width="100%">
    <tr>
        <td style="width: 135px">Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
            ErrorMessage="Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>File Name:
        </td>
        <td>
            <asp:TextBox ID="txtFileName" runat="server" Columns="55"></asp:TextBox>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" />
    </div>
</div>
