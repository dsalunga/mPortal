<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.Controls.FullNamePicker"
    CodeBehind="FullNamePicker.ascx.cs" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:TextBox ID="txtFirstName" placeholder="First Name" CssClass="input" ClientIDMode="Static" runat="server" Columns="15"></asp:TextBox><asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                ErrorMessage="First Name" ForeColor="Red">*</asp:RequiredFieldValidator>&nbsp;
        </td>
        <td>
            <asp:TextBox ID="txtMiddleName" placeholder="Middle Name" CssClass="input" ClientIDMode="Static" runat="server" Columns="15"></asp:TextBox>&nbsp;&nbsp;
        </td>
        <td>
            <asp:TextBox ID="txtLastName" placeholder="Last Name" CssClass="input" ClientIDMode="Static" runat="server" Columns="15"></asp:TextBox><asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                ErrorMessage="Last Name" ForeColor="Red">*</asp:RequiredFieldValidator>&nbsp;
        </td>
    </tr>
</table>
