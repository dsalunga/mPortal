<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPage.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebPageController" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/ManagementSecurityOption.ascx" TagName="ManagementSecurityOption"
    TagPrefix="uc2" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
<asp:HiddenField ID="hiddenCSITID" runat="server" />
<asp:HiddenField ID="hiddenTempCSITID" runat="server" />
<h1 class="central page-header" id="lblTitle" runat="server">
    Web Page
</h1>
<div>
    <uc1:TabControl ID="tabNavigation" OnSelectedTabChanged="tabNavigation_SelectedTabChanged"
        runat="server" />
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="viewBasic" runat="server">
            <table>
                <tr>
                    <td style="width: 100px">Name<asp:RequiredFieldValidator ID="rfvSectionName" runat="server" ControlToValidate="txtName"
                        ErrorMessage="Section Name">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" CssClass="input" runat="server" Columns="75"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Page Url<asp:RequiredFieldValidator ID="rfvFriendlyURL" runat="server" ControlToValidate="txtIdentityName"
                        ErrorMessage="Friendly URL Name" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="min-bottom-margin">
                        <span runat="server" id="lblUrlPre" style="border: 1px solid #eee; padding: 6px; background-color: #eee; font-size: 14px"></span>
                        <asp:TextBox ID="txtIdentityName" CssClass="input" runat="server" Columns="30"></asp:TextBox>&nbsp;<span id="lblExtension" style="border: 1px solid #eee; padding: 6px; background-color: #eee; font-size: 14px" runat="server">.aspx</span>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Rank
                    </td>
                    <td>
                        <cc1:ComboBox ID="cboRank" runat="server" AutoCompleteMode="Suggest" Width="50px">
                        </cc1:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Page Type
                    </td>
                    <td align="center">
                        <table border="0" style="background-color: ghostwhite;" width="100%" cellpadding="0"
                            cellspacing="0">
                            <tr>
                                <td style="height: 3px"></td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:ImageButton ImageUrl="~/Content/Assets/Images/PartThumb.jpg" ToolTip="Click here to change module"
                                        ID="imgPreview" runat="server" OnClick="imgPreview_Click" CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 3px; font-style: italic; text-align: center">Click icon to change
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:Literal ID="lTemplateName" runat="server" Text="* Web Part *"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 3px"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:CheckBox ID="chkIsActive" CssClass="aspnet-checkbox" runat="server" Checked="True" Text="Active"></asp:CheckBox>&nbsp;
                                <asp:CheckBox ID="chkUsePartTemplatePath" CssClass="aspnet-checkbox" runat="server" Text="Use Built-in Template"
                                    Checked="True"></asp:CheckBox>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="viewAdvanced" runat="server">
            <table style="margin-top: 10px;">
                <tr>
                    <td style="width: 100px;">Page Title
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitle" CssClass="col-md-4 input" runat="server" Columns="75"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Parent Page<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtParentUrl" ErrorMessage="Parent Page">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="min-bottom-margin">
                        <asp:TextBox ID="txtParentUrl" ClientIDMode="Static" CssClass="input" runat="server" Columns="60">/</asp:TextBox>
                        <asp:Button ID="cmdBrowse" ClientIDMode="Static" runat="server" Text="Browse..."
                            CausesValidation="False" CssClass="btn btn-default btn-sm" />
                    </td>
                </tr>
                <%--
                            <tr>
                                <td>
                                    Page Url:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSiteSectionURL" runat="server" Columns="75"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Page Identity:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSectionIdentity" runat="server" Columns="30"></asp:TextBox>
                                    <asp:CheckBox ID="chkDefault" runat="server" Text="Default Page" />
                                </td>
                            </tr>--%>
                <tr>
                    <td>Master Page
                    </td>
                    <td>
                        <asp:DropDownList ID="cboMasterPage" CssClass="input" runat="server" DataSourceID="ObjectDataSource1"
                            DataTextField="Name" DataValueField="Id" AppendDataBoundItems="True" AutoPostBack="True"
                            OnSelectedIndexChanged="cboMasterPage_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="-1"># Web Site #</asp:ListItem>
                            <asp:ListItem Value="-2"># Parent #</asp:ListItem>
                            <asp:ListItem Value="-3"># None #</asp:ListItem>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetMasterPages"
                            TypeName="WCMS.WebSystem.WebParts.Central.WebSites.WebPageController">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="siteId" QueryStringField="SiteId" Type="Int32" DefaultValue="-1" />
                                <asp:QueryStringParameter Name="pageId" QueryStringField="PageId" Type="Int32" DefaultValue="-1" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td>Theme:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboThemes" DataTextField="Name" CssClass="input" DataValueField="Id" runat="server"
                            AppendDataBoundItems="True">
                            <asp:ListItem Selected="True" Value="-1"># Default Theme #</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;
                    </td>
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
                    <td colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Page Type
                    </td>
                    <td>
                        <asp:DropDownList ID="cboPageType" runat="server" CssClass="input">
                            <asp:ListItem Selected="True" Text="Dynamic" Value="0" />
                            <asp:ListItem Text="Static" Value="1" />
                        </asp:DropDownList>
                        &nbsp;<em>(ignored if part template is standalone)</em>
                    </td>
                </tr>
            </table>
            <script type="text/javascript">
                $("#cmdBrowse").click(function () {
                    BrowseLink("txtParentUrl", -1, "<%= DataHelper.GetId(Request, WebColumns.SiteId) %>"); return false;
                        });
            </script>
        </asp:View>
        <asp:View ID="viewModuleChooser" runat="server" OnActivate="viewModuleChooser_Activate">
            <table border="0" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="top">
                        <div style="padding: 5px; width: 300px; height: 600px; overflow: auto; background-color: #f0f0f0">
                            <asp:CheckBox ID="chkShowAll" CssClass="aspnet-checkbox" runat="server" AutoPostBack="true" Checked="false" Text="Show All" OnCheckedChanged="chkShowAll_CheckedChanged" />
                            <asp:TreeView ID="tv1" runat="server" NodeIndent="15" ShowLines="True" AutoGenerateDataBindings="False"
                                EnableViewState="False">
                                <ParentNodeStyle Font-Bold="False" />
                                <HoverNodeStyle Font-Underline="True" />
                                <SelectedNodeStyle BackColor="White" BorderColor="#888888" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Underline="False" HorizontalPadding="2px" VerticalPadding="1px" />
                                <NodeStyle ForeColor="Black" HorizontalPadding="3px" NodeSpacing="1px" VerticalPadding="1px" />
                                <LeafNodeStyle ImageUrl="~/Content/Assets/Images/TreeView/txt.gif" />
                            </asp:TreeView>
                        </div>
                    </td>
                    <td valign="top" style="width: 100%">
                        <iframe frameborder="0" name="iframePreview" src="<% =QueryParser.BuildQuery(CentralPages.WebPartPreview, WebColumns.PartControlTemplateId, hiddenCSITID.Value) %>"
                            width="450" height="610"></iframe>
                    </td>
                </tr>
            </table>
            <div class="control-box">
                <div>
                    <asp:Button ID="cmdOK" runat="server" Text="OK" CssClass="btn btn-primary" OnClick="cmdOK_Click" />
                    <asp:Button ID="cmdTemplateCancel" runat="server" Text="Cancel" OnClick="cmdTemplateCancel_Click" CssClass="btn btn-default" />
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</div>
<div runat="server" id="trControlBox" class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" Text="Finish" CssClass="btn btn-primary" OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdNext" runat="server" Text="Next" OnClick="cmdNext_Click" CssClass="btn btn-default" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" CssClass="btn btn-default" />
    </div>
</div>
<div style="color: Red; padding: 5px" id="divMessage" runat="server"></div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The folowing are required:"
    ShowMessageBox="True" ShowSummary="False" />
