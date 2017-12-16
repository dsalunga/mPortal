<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigLinkedMenuEdit.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Menu.ConfigLinkedMenuEdit" %>
<table border="0" cellpadding="1" cellspacing="0">
    <tr>
        <td style="width: 125px">
            <strong>Text<asp:RequiredFieldValidator ID="rfvCaption" runat="server" ControlToValidate="txtCaption"
                ErrorMessage="Menu Name">*</asp:RequiredFieldValidator></strong>
        </td>
        <td>
            <asp:TextBox ID="txtCaption" ClientIDMode="Static" runat="server" Columns="75" />
        </td>
    </tr>
    <tr>
        <td>Navigate URL<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
            ControlToValidate="txtNavigateURL" ErrorMessage="Navigate URL">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtNavigateURL" ClientIDMode="Static" runat="server" Columns="70"
                ReadOnly="true" />
        </td>
    </tr>
    <tr>
        <td>Target Window
        </td>
        <td>
            <asp:DropDownList ID="cboTarget" runat="server" DataSourceID="ObjectDataSource1"
                DataTextField="Text" DataValueField="Value" AppendDataBoundItems="True">
                <asp:ListItem Selected="True" Text="* Default *" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetLinkTargets"
                TypeName="WCMS.WebSystem.WebParts.Menu.ConfigLinkedMenuEdit"></asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td colspan="2">&nbsp;
        </td>
    </tr>
    <tr>
        <td valign="top">
            <strong>Menu Placement</strong>
        </td>
        <td>
            <div style="padding-bottom: 10px">
                <label for="cboMenus">
                    Owner Menu</label>
                <asp:DropDownList ID="cboMenus" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                    DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="cboMenus_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="-1" Text=""></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="padding-bottom: 10px">
                <label for="cboMenuItems">
                    Relative Item</label>
                <asp:DropDownList ID="cboMenuItems" runat="server">
                    <asp:ListItem Selected="True" Value="-1" Text=""></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div>
                <label>
                    Relative Position</label>
                <asp:DropDownList ID="cboItemPostion" runat="server">
                    <asp:ListItem Value="0">Before</asp:ListItem>
                    <asp:ListItem Selected="True" Value="1">After</asp:ListItem>
                    <asp:ListItem Value="2">Child</asp:ListItem>
                </asp:DropDownList>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2">&nbsp;
        </td>
    </tr>
    <tr>
        <td>Properties
        </td>
        <td>
            <asp:CheckBox ID="chkIsActive" CssClass="aspnet-checkbox" runat="server" Text="Active" Checked="True"></asp:CheckBox>&nbsp;<asp:CheckBox
                ID="chkCheckPermission" runat="server" CssClass="aspnet-checkbox" Text="Check Permission" Checked="False"></asp:CheckBox>
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
