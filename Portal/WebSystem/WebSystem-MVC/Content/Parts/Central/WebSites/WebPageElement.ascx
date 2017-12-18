<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPageElement.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebPageElementController" %>
<%@ Register Src="../Controls/ManagementSecurityOption.ascx" TagName="ManagementSecurityOption"
    TagPrefix="uc2" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<asp:HiddenField ID="hiddenCSITID" runat="server" />
<asp:HiddenField ID="hiddenTempCSITID" runat="server" />
<asp:HiddenField ID="hMasterPageId" runat="server" Value="-1" />
<asp:HiddenField ID="hElementId" runat="server" Value="-1" />

<h1 class="central page-header">
    Web Page Element
</h1>
<div>
    <uc1:TabControl ID="tabNavigation" OnSelectedTabChanged="tabNavigation_SelectedTabChanged"
        runat="server" />
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="viewBasic" runat="server">
            <table style="margin-top: 5px">
                <tr>
                    <td style="width: 100px;">Name<asp:RequiredFieldValidator ID="rvfSectionTitle" runat="server" ControlToValidate="txtName"
                        ErrorMessage="Section Name">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" CssClass="input" runat="server" Columns="75" meta:resourcekey="txtNameResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Panel
                    </td>
                    <td>
                        <asp:DropDownList ID="cboPanel" CssClass="input" DataValueField="Id" DataTextField="Name" runat="server"
                            AutoPostBack="True" OnSelectedIndexChanged="cboPanel_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px;">Create In (Owner)
                    </td>
                    <td style="height: 12px">
                        <asp:DropDownList ID="cboOwner" CssClass="input" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Rank
                    </td>
                    <td style="vertical-align: top">
                        <asp:DropDownList ID="cboItemPostion" CssClass="input" runat="server" Enabled="True" ClientIDMode="Static">
                            <asp:ListItem Selected="True" Value="0">After</asp:ListItem>
                            <asp:ListItem Value="1">Before</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="cboSiblings" CssClass="input" runat="server" DataTextField="Name" DataValueField="Id"
                            Enabled="True" ClientIDMode="Static">
                            <asp:ListItem Value="-1">* None *</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                                <asp:CheckBox ID="chkCustomRank" CssClass="aspnet-checkbox" runat="server" Text="Custom:" ClientIDMode="Static" /><asp:TextBox
                                    ID="txtRank" Columns="7" runat="server" CssClass="input" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Web Part
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
                                        ID="imgPreview" runat="server" OnClick="imgPreview_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 3px"></td>
                            </tr>
                            <tr>
                                <td style="text-align: center; font-style: italic">Click icon to change
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <br />
                                    <asp:Literal ID="lTemplateName" runat="server" Text="* Web Part *"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px"></td>
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
                        <asp:CheckBox ID="chkIsActive" CssClass="aspnet-checkbox" runat="server" Text="Active" Checked="True" meta:resourcekey="chkIsActiveResource1"></asp:CheckBox>&nbsp;
                                <asp:CheckBox ID="chkUsePartTemplatePath" CssClass="aspnet-checkbox" runat="server" Text="Use Built-in Template"
                                    Checked="True"></asp:CheckBox>
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The folowing are required:"
                ShowMessageBox="True" ShowSummary="False" />
        </asp:View>
        <asp:View ID="viewAdvanced" runat="server">
            <table style="margin-top: 5px">
                <%--<tr>
                            <td>&nbsp;
                            </td>
                            <td></td>
                        </tr>--%>
                <tr>
                    <td style="width: 100px;" valign="top">Public Access
                    </td>
                    <td>
                        <asp:DropDownList ID="cboPublicAccess" CssClass="input" runat="server">
                            <asp:ListItem Text="Inherit" Value="128" />
                            <asp:ListItem Selected="True" Text="Anonymous Access" Value="1" />
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
            </table>
        </asp:View>
        <!-- Module Chooser -->
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
                    <td style="width: 100%">
                        <iframe frameborder="0" name="iframePreview" src="<% =QueryParser.BuildQuery(CentralPages.WebPartPreview, WebColumns.PartControlTemplateId, hiddenCSITID.Value) %>"
                            width="450" height="610"></iframe>
                    </td>
                </tr>
            </table>
            <div class="control-box">
                <div>
                    <asp:Button ID="cmdOK" runat="server" Text="OK" CssClass="btn btn-primary" OnClick="cmdOK_Click" />
                    <asp:Button ID="cmdTemplateCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="cmdTemplateCancel_Click" />
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</div>
<div class="control-box" runat="server" id="trControlBox">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" CssClass="btn btn-primary" Text="Finish" OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdNext" runat="server" Text="Next" OnClick="cmdNext_Click" CssClass="btn btn-default" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" CssClass="btn btn-default" />
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("#chkCustomRank").change(function () {
            ToggleCustomRank();
        });

        ToggleCustomRank();
    });

    function ToggleCustomRank() {
        var enableCustomRank = $("#chkCustomRank").is(":checked");

        $("#txtRank").attr("disabled", !enableCustomRank);
        $("#cboItemPostion").attr("disabled", enableCustomRank);
        $("#cboSiblings").attr("disabled", enableCustomRank);
    }
</script>
