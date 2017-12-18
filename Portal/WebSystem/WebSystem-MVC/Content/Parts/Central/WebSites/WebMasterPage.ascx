<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebMasterPage.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebMasterPageController" %>
<%@ Register Src="../Controls/ManagementSecurityOption.ascx" TagName="ManagementSecurityOption"
    TagPrefix="uc1" %>
<h1 class="central page-header">
    Master Page
</h1>
<table width="100%">
    <tr>
        <td style="width: 90px;">Name:<asp:RequiredFieldValidator ID="rfvPageName" runat="server" ControlToValidate="txtName"
            ErrorMessage="Page Name" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" CssClass="input" Columns="65"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Parent:</td>
        <td>
            <asp:DropDownList ID="cboParent" runat="server" CssClass="input"
                DataTextField="Value" DataValueField="Key" AppendDataBoundItems="true" DataSourceID="ObjectDataSourceMasterPages">
                <asp:ListItem Value="-1" Text=""></asp:ListItem>
            </asp:DropDownList>
            <asp:ObjectDataSource ID="ObjectDataSourceMasterPages" runat="server"
                SelectMethod="GetMasterPages" TypeName="WCMS.WebSystem.WebParts.Central.WebSites.WebMasterPageController">
                <SelectParameters>
                    <asp:Parameter Name="siteId" Type="Int32" DefaultValue="-1" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr style="margin: 5px 0" />
        </td>
    </tr>
    <tr>
        <td>Theme:
        </td>
        <td>
            <asp:DropDownList ID="cboThemes" runat="server" CssClass="input"
                DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
                <asp:ListItem Value="-1" Text=""></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td valign="top">Template:
        </td>
        <td>
            <asp:DropDownList ID="cboPageTemplates" runat="server" DataSourceID="ObjectDataSource1" CssClass="input"
                DataTextField="Value" DataValueField="Key" AutoPostBack="True"
                OnSelectedIndexChanged="cboPageTemplates_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
                SelectMethod="GetTemplates" TypeName="WCMS.WebSystem.WebParts.Central.WebSites.WebMasterPageController"></asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr style="margin: 5px 0" />
        </td>
    </tr>
    <tr runat="server" id="panelSkin">
        <td>Skin:
        </td>
        <td>
            <asp:DropDownList ID="cboSkins" DataTextField="Name" DataValueField="Id" runat="server" CssClass="input"
                AppendDataBoundItems="True">
                <asp:ListItem Selected="True" Value="-1"># Default Skin #</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr runat="server" id="panelOwnerPage">
        <td>Owner Page:
        </td>
        <td>
            <asp:DropDownList ID="cboOwnerPage" runat="server" CssClass="input">
            </asp:DropDownList>&nbsp;<asp:CheckBox ID="chkIsDefault" CssClass="aspnet-checkbox" runat="server" Text="Is Default" Checked="false" /></td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td></td>
    </tr>
    <tr>
        <td valign="top">Public Access
        </td>
        <td valign="top">
            <asp:DropDownList ID="cboPublicAccess" runat="server" CssClass="input">
                <asp:ListItem Selected="True" Text="Inherit" Value="128" />
                <asp:ListItem Text="Anonymous Access" Value="1" />
                <asp:ListItem Text="Account Authentication" Value="2" />
                <asp:ListItem Text="IP Address Authentication" Value="4" />
                <asp:ListItem Text="Account Or IP Address Authentication" Value="8" />
                <asp:ListItem Text="Account And IP Address Authentication" Value="16" />
            </asp:DropDownList>
            <br />
            <asp:CheckBox ID="chkIPAddress" CssClass="aspnet-checkbox" runat="server" Text="Accept all IP addresses except from list" />
            <br />
            <asp:CheckBox ID="chkAccount" CssClass="aspnet-checkbox" runat="server" Text="Accept all authenticated users except from list" />
        </td>
    </tr>
    <tr>
        <td valign="top">Mgmt Access
        </td>
        <td valign="top">
            <uc1:ManagementSecurityOption ID="ManagementSecurityOption1" runat="server" />
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" Text="Update" OnClick="cmdUpdate_Click"
            Font-Bold="True" CssClass="btn btn-primary" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" CssClass="btn btn-default" />
    </div>
</div>
