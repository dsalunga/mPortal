<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="WCMS.WebSystem.WebParts.Test.TemplateFormEditor" CodeBehind="TemplateFormEditor.ascx.cs" %>
<table border="0">
    <tr>
        <td>Menu Name:<asp:RequiredFieldValidator ID="rfvCaption" runat="server" ControlToValidate="txtCaption"
            ErrorMessage="Menu Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtCaption" runat="server" Columns="75"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Site:</td>
        <td>
            <asp:DropDownList ID="ddlSites" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Checked="True"></asp:CheckBox></td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" CssClass="btn btn-primary" runat="server" Text="Update" OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" />
    </div>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
