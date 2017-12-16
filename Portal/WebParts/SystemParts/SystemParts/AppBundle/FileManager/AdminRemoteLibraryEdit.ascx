<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminRemoteLibraryEdit.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.FileManager.AdminRemoteLibraryEdit" %>
<table border="0" cellpadding="1" cellspacing="0">
    <tr>
        <td style="width: 140px">
            <strong>Name<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                ErrorMessage="Name">*</asp:RequiredFieldValidator></strong>
        </td>
        <td>
            <asp:TextBox ID="txtName" ClientIDMode="Static" runat="server" CssClass="input" Columns="60" />
        </td>
    </tr>
    <tr>
        <td>Source Type
        </td>
        <td>
            <asp:DropDownList ID="cboSourceType" runat="server" CssClass="input">
                <asp:ListItem Value="-1" Text=""></asp:ListItem>
            </asp:DropDownList>&nbsp;<asp:CheckBox ID="chkActive" CssClass="aspnet-checkbox" runat="server" Text="Active" Checked="True"></asp:CheckBox></td>
    </tr>
    <tr>
        <td>Base Address<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
            ControlToValidate="txtBaseAddress" ErrorMessage="Base address">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtBaseAddress" ClientIDMode="Static" runat="server" Columns="70" CssClass="span6 input" />
        </td>
    </tr>
    <tr>
        <td>UserName
        </td>
        <td>
            <asp:TextBox ID="txtUserName" ClientIDMode="Static" CssClass="input" runat="server" Columns="40" />
        </td>
    </tr>
    <tr>
        <td>Password
        </td>
        <td>
            <asp:TextBox ID="txtPassword" ClientIDMode="Static" CssClass="input" TextMode="Password" runat="server" Columns="40" />
        </td>
    </tr>
    <tr>
        <td>Display Base Address
        </td>
        <td>
            <asp:TextBox ID="txtDisplayBaseAddress" ClientIDMode="Static" CssClass="span5 input" runat="server" Columns="60" />
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" CssClass="btn btn-primary" runat="server" Text="Update"
            OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" CssClass="btn btn-default" runat="server" Text="Cancel"
            OnClick="cmdCancel_Click" CausesValidation="False" />
    </div>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
