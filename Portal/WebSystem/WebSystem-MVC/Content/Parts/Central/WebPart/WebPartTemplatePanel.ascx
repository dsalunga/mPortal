<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPartTemplatePanel.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebPartTemplatePanelController" %>
<h1 class="central page-header">
    <asp:Literal ID="lMessage" runat="server"></asp:Literal>
</h1>
<table width="100%">
    <tr>
        <td width="125">Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
            ErrorMessage="PlaceHolder Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Panel Name:<asp:RequiredFieldValidator ID="rfvPlaceHolderName" runat="server" ControlToValidate="txtPanelName"
            ErrorMessage="Panel Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtPanelName" runat="server" Columns="50">ph</asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Rank:
        </td>
        <td>
            <asp:TextBox ID="txtRank" runat="server" Columns="10"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chkSetDefault" CssClass="aspnet-checkbox" Text="Set as Default Panel" runat="server" /></td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" Text="Update" OnClick="cmdUpdate_Click" CssClass="btn btn-primary" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmdCancel_Click"
            CausesValidation="False" />
    </div>
</div>
