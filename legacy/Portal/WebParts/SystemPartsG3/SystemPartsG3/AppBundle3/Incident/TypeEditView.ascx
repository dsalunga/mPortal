<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TypeEditView.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Incident.TypeEditView" %>
<table border="0">
    <tr>
        <td>Name:<asp:RequiredFieldValidator ID="rfvCaption" runat="server" ControlToValidate="txtName"
            ErrorMessage="Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" Columns="75" CssClass="input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Rank:
        </td>
        <td>
            <asp:TextBox ID="txtRank" Text="0" runat="server" Columns="5" CssClass="input"></asp:TextBox>&nbsp;
            <asp:CheckBox ID="chkSLA" CssClass="checkbox-inline" Text="Use Standard SLA" runat="server" />
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" CssClass="btn btn-primary" runat="server" Text="Update"
            OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" CssClass="btn" runat="server" Text="Cancel"
            OnClick="cmdCancel_Click" CausesValidation="False" />
    </div>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
