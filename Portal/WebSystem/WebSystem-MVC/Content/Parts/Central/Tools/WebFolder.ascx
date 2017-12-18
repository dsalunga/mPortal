<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebFolder.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.Tools.WebFolderController" %>
<h1 class="central page-header">
    Web Folder
</h1>
<table width="100%">
    <tr>
        <td style="width: 110px">Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtID"
            ErrorMessage="Setting ID">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtID" runat="server" Columns="100"></asp:TextBox>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" Text="Update" OnClick="cmdUpdate_Click" CssClass="btn btn-primary" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" CssClass="btn btn-default" />
    </div>
</div>
