<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportExportPage.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Tools.ImportExportPage" %>
<table>
    <tr>
        <td style="width: 130px; vertical-align: top">Reference Page:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
            ControlToValidate="txtPageUrl" ForeColor="Red" ErrorMessage="Reference Page">*</asp:RequiredFieldValidator>
        </td>
        <td class="min-bottom-margin">
            <asp:TextBox ID="txtPageUrl" ClientIDMode="Static" runat="server" CssClass="input" Columns="70">/</asp:TextBox>
            <asp:Button ID="cmdBrowse" ClientIDMode="Static" runat="server" Text="Browse..."
                CausesValidation="False" CssClass="btn btn-default btn-sm" />
            <br />
            <em>(Restore: Enter only "/" if restoring a Site entity)</em>
            <br />
            <br />
            <%--
            <asp:TextBox ID="txtPageName" ReadOnly="true" ClientIDMode="Static" runat="server" Columns="70" />--%>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top">Back-Up/Restore Options:
        </td>
        <td>
            <asp:CheckBox ID="chkWebSite" runat="server" CssClass="aspnet-checkbox" Checked="true" Text="Site" />
            <asp:CheckBox ID="chkSiteIdentities" CssClass="aspnet-checkbox" runat="server" Checked="true" Text="Identities" />
            <asp:CheckBox ID="chkCurrentPage" CssClass="aspnet-checkbox" runat="server" Checked="true" Text="Current Page" />
            <br />
            <asp:CheckBox ID="chkMasterPages" CssClass="aspnet-checkbox" runat="server" Checked="true" Text="Master Pages" />
            <asp:CheckBox ID="chkWebPages" CssClass="aspnet-checkbox" runat="server" Checked="true" Text="Pages" />
            <asp:CheckBox ID="chkElements" CssClass="aspnet-checkbox" runat="server" Checked="true" Text="Elements" />
            <asp:CheckBox ID="chkPanels" CssClass="aspnet-checkbox" runat="server" Checked="true" Text="Panels" />
            <br />
            <asp:CheckBox ID="chkChildren" CssClass="aspnet-checkbox" runat="server" Checked="true" Text="Children" />
            <asp:CheckBox ID="chkResources" CssClass="aspnet-checkbox" runat="server" Checked="true" Text="Resources" />
            <asp:CheckBox ID="chkSecurity" CssClass="aspnet-checkbox" runat="server" Checked="true" Text="Security" />
            <asp:CheckBox ID="chkParameters" CssClass="aspnet-checkbox" runat="server" Checked="true" Text="Parameters" />
            <br />
            <asp:CheckBox ID="chkElementData" CssClass="aspnet-checkbox" runat="server" Checked="true" Text="Element Data" />
            <asp:CheckBox ID="chkPartData" CssClass="aspnet-checkbox" runat="server" Checked="true" Text="Part Data" />
            <br />
            <br />
        </td>
    </tr>
    <tr>
        <td>Source XML:
        </td>
        <td style="padding-bottom: 5px">
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Restore Options:
        </td>
        <td>
            <asp:DropDownList ID="cboRestore" runat="server" CssClass="input">
                <asp:ListItem Value="0">Restore in root</asp:ListItem>
                <asp:ListItem Selected="True" Value="1">Restore as children</asp:ListItem>
                <asp:ListItem Value="2">Restore in same level</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdExport" CssClass="btn btn-default" runat="server" Text="Export as XML"
            OnClick="cmdExport_Click" />
        <asp:Button ID="cmdImport" CssClass="btn btn-default" runat="server" Text="Import from XML"
            OnClick="cmdImport_Click" />
    </div>
</div>
<br />
<br />
<asp:Label ID="lblStatus" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
<script type="text/javascript">
    $(document).ready(function () {
        $("#cmdBrowse").click(function () {
            BrowseLink("txtPageUrl", -1); return false;
        });
    });
</script>
