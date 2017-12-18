<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebRegistryEntry.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Tools.WebRegistryEntryController" %>
<%@ Register Src="~/Content/Controls/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc2" %>
<h1 class="central page-header">Registry Entry
</h1>
<table width="100%">
    <tr>
        <td style="width: 110px">Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtID"
            ErrorMessage="Setting ID">*</asp:RequiredFieldValidator>
        </td>
        <td class="min-bottom-margin"   >
            <asp:TextBox ID="txtID" runat="server" Columns="100" CssClass="form-control"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Parent:
        </td>
        <td class="min-bottom-margin">
            <asp:TextBox ID="txtParent" ClientIDMode="Static" CssClass="form-control" runat="server" Columns="40"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td valign="top" style="width: 110px">Value:
        </td>
        <td>
            <uc2:TextEditor ID="TextEditor1" runat="server" />
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
