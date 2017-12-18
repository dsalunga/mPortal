<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShortUrlEdit.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Tools.ShortUrlEdit" %>
<h1 class="central page-header">
    Short URL Editor
</h1>
<table border="0">
    <tr>
        <td>Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
            ErrorMessage="Name" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <strong style="font-size: 16px">/</strong>&nbsp;<asp:TextBox ID="txtName" runat="server" Columns="75" CssClass="input"></asp:TextBox>&nbsp;<%--<strong>.aspx</strong>--%>
        </td>
    </tr>
    <tr>
        <td style="width: 110px">Page:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNavigateURL"
            ErrorMessage="Page">*</asp:RequiredFieldValidator>
        </td>
        <td class="min-bottom-margin">
            <asp:TextBox ID="txtNavigateURL" ClientIDMode="Static" runat="server" Columns="70" CssClass="input" />
            <asp:Button ID="cmdBrowse" runat="server" ClientIDMode="Static" Text="Browse..."
                CausesValidation="False" CssClass="btn btn-default btn-sm" />
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
<script type="text/javascript">
    $("#cmdBrowse").click(function () {
        BrowseLink("txtNavigateURL", -1); return false;
    });
</script>
