<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Menu._Sections_MENU_CCMS_Menu_09"
    CodeBehind="CCMS_Menu_09.ascx.cs" %>
<table border="0">
    <tr>
        <td>
            Menu Name:<asp:RequiredFieldValidator ID="rfvCaption" runat="server" ControlToValidate="txtCaption"
                ErrorMessage="Menu Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtCaption" runat="server" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Site:
        </td>
        <td>
            <asp:DropDownList ID="ddlSites" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Checked="True"></asp:CheckBox>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="right" class="ControlBox">
            <asp:Button ID="cmdUpdate" CssClass="Command" Width="75px" runat="server" Text="Update"
                OnClick="cmdUpdate_Click" />
            <asp:Button ID="cmdCancel" CssClass="Command" Width="75px" runat="server" Text="Cancel"
                OnClick="cmdCancel_Click" CausesValidation="False" />
        </td>
    </tr>
</table>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
