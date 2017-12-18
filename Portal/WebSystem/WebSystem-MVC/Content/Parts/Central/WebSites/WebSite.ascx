<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebSite.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebSiteController" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/ManagementSecurityOption.ascx" TagName="ManagementSecurityOption"
    TagPrefix="uc2" %>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
<h1 class="central page-header" id="lblTitle" runat="server">
    Web Site
</h1>
<div>
    <uc1:TabControl ID="editTab" runat="server" OnSelectedTabChanged="editTab_OnSelectedTabChanged" />
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="viewBasic" runat="server">
            <table style="margin-top: 10px;">
                <tr>
                    <td style="width: 70px">Name:<asp:RequiredFieldValidator ID="rfvSiteName" runat="server" ControlToValidate="txtName"
                        ErrorMessage="Site Name">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" CssClass="input" Columns="75"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Identity:<asp:RequiredFieldValidator ID="rfvFriendlyURL" runat="server" ControlToValidate="txtUrlName"
                        ErrorMessage="Url Name" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUrlName" runat="server" CssClass="input" Columns="60"></asp:TextBox><%--&nbsp;<strong>.aspx</strong>--%>
                    </td>
                </tr>
                <tr>
                    <td>Rank:
                    </td>
                    <td>
                        <cc1:ComboBox ID="cboRank" runat="server" AutoCompleteMode="Suggest" Width="50px">
                        </cc1:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:CheckBox ID="chkIsActive" CssClass="aspnet-checkbox" Checked="true" runat="server" Text="Active" /></td>
                </tr>
            </table>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The folowing are required:"
                ShowMessageBox="True" ShowSummary="False" />
        </asp:View>
        <asp:View ID="viewAdvanced" runat="server">
            <table style="margin-top: 10px;">
                <tr>
                    <td style="width: 140px">Title:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitle" CssClass="col-md-4 input" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Title Format:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitleFormat" runat="server" CssClass="input" Columns="45"></asp:TextBox>&nbsp;<em>e.g. $(Page:Title) - $(Site:Title)</em>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td valign="top">Public Security
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="cboPublicAccess" CssClass="input" runat="server">
                            <asp:ListItem Selected="True" Text="Inherit" Value="128" />
                            <asp:ListItem Text="Anonymous Access" Value="1" />
                            <asp:ListItem Text="Account Authentication" Value="2" />
                            <asp:ListItem Text="IP Address Authentication" Value="4" />
                            <asp:ListItem Text="Account Or IP Address Authentication" Value="8" />
                            <asp:ListItem Text="Account And IP Address Authentication" Value="16" />
                        </asp:DropDownList>
                        <br />
                        <asp:CheckBox ID="chkAccount" CssClass="aspnet-checkbox" runat="server" Text="Accept all authenticated users except from list" />
                        <br />
                        <asp:CheckBox ID="chkIPAddress" CssClass="aspnet-checkbox" runat="server" Text="Accept all IP addresses except from list" />
                    </td>
                </tr>
                <tr>
                    <td valign="top">Mgmt Access
                    </td>
                    <td valign="top">
                        <uc2:ManagementSecurityOption ID="ManagementSecurityOption1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Base Address:
                    </td>
                    <td>
                        <asp:TextBox ID="txtBaseAddress" CssClass="col-md-8 input" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" id="panelPrimaryIdentity" visible="false">
                    <td>Primary Identity:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboPrimaryIdentity" CssClass="input" runat="server" DataTextField="HostName" DataValueField="Id" AppendDataBoundItems="true">
                            <asp:ListItem Value="-1"># None #</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Login Page:
                    </td>
                    <td>
                        <asp:TextBox ID="txtLoginPage" CssClass="input" runat="server" Columns="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr runat="server" id="panelHomePage">
                    <td>Home Page:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboHomePage" CssClass="input" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Value="-1"># None #</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr runat="server" id="panelMasterPage">
                    <td valign="top">Default Master Page:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboDefaultMasterPage" CssClass="input" runat="server" DataSourceID="ObjectDSMasterPages"
                            DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
                            <asp:ListItem Value="-1"># None #</asp:ListItem>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDSMasterPages" runat="server" SelectMethod="GetMasterPages"
                            TypeName="WCMS.WebSystem.WebParts.Central.WebSites.WebSiteController">
                            <SelectParameters>
                                <asp:QueryStringParameter DefaultValue="-1" Name="siteId" QueryStringField="_SiteId"
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr runat="server" id="panelTheme">
                    <td>Theme:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboTheme" runat="server" CssClass="input"
                            DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
                            <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</div>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" Text="Update" OnClick="cmdUpdate_Click" CssClass="btn btn-primary" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" CssClass="btn btn-default" />
    </div>
</div>
