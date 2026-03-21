<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Menu.MenuItemEdit"
    CodeBehind="ConfigMenuItemEdit.ascx.cs" %>
<table border="0">
    <tr>
        <td style="width: 110px">Text:<asp:RequiredFieldValidator ID="rfvCaption" runat="server" ControlToValidate="txtCaption"
            ErrorMessage="Menu Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtCaption" ClientIDMode="Static" CssClass="input" runat="server" Columns="75" />
        </td>
    </tr>
    <tr>
        <td style="width: 110px">Navigate URL:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
            ControlToValidate="txtNavigateURL" ErrorMessage="Navigate URL">*</asp:RequiredFieldValidator>
        </td>
        <td class="min-bottom-margin">
            <asp:TextBox ID="txtNavigateURL" ClientIDMode="Static" CssClass="input" runat="server" Columns="70">/</asp:TextBox>
            <asp:Button ID="cmdBrowse" ClientIDMode="Static" CssClass="btn btn-default btn-sm" runat="server" Text="Browse..."
                CausesValidation="False" />
        </td>
    </tr>
    <tr>
        <td style="width: 110px">Target Window:
        </td>
        <td>
            <asp:DropDownList ID="cboTarget" runat="server" CssClass="input" DataSourceID="ObjectDataSource1"
                DataTextField="Text" DataValueField="Value" AppendDataBoundItems="True">
                <asp:ListItem Selected="True" Text="# Default #" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetLinkTargets"
                TypeName="WCMS.WebSystem.WebParts.Menu.MenuItemEdit"></asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td style="width: 110px">Rank:<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRank"
            ErrorMessage="Rank">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtRank" CssClass="input" Text="0" runat="server" Columns="10"></asp:TextBox>&nbsp;<asp:CheckBox
                ID="chkIsActive" runat="server" CssClass="aspnet-checkbox" Text="Active" Checked="True"></asp:CheckBox>&nbsp;<asp:CheckBox
                    ID="chkCheckPermission" CssClass="aspnet-checkbox" runat="server" Text="Check Permission" Checked="False"></asp:CheckBox>
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
    $("#txtNavigateURL").change(function () {
        GetPageName($("#txtNavigateURL").val(), $("#txtCaption"));
    });

    $("#cmdBrowse").click(function () {
        BrowseLink("txtNavigateURL", -1, "<%: SiteId %>"); return false;
    });
</script>
