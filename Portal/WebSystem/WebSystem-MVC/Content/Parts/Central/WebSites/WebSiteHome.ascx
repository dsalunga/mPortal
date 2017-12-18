<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebSiteHome.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebSiteHome" %>
<%@ Register Src="../Controls/WebSiteTab.ascx" TagName="WebSiteTab" TagPrefix="uc2" %>
<uc2:WebSiteTab ID="WebSiteTab1" runat="server" />
<div class="row">
    <div class="col-md-4">
        <table runat="server" id="tableTasks">
            <tr runat="server" id="rowProperties">
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <a id="linkProperties" runat="server" href="/Central/WebSite/">
                                    <img src="/Content/Assets/Images/file_edit.png" class="TaskListIcon" border="0" />
                                </a>
                            </td>
                            <td class="Header">Properties
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Place description here
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr runat="server" id="rowDelete">
                <td>
                    <!-- Delete -->
                    <table border="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <asp:LinkButton OnClientClick="return WCMS.Dom.Confirm('Are you sure you want to delete this item?');"
                                    ID="cmdDelete" runat="server" OnClick="cmdDelete_Click"><img src="/Content/Assets/Images/delete-folder.png" class="TaskListIcon" border="0" /></asp:LinkButton>
                            </td>
                            <td class="Header">Delete this
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Place description here
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr runat="server" id="rowBindings">
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <a runat="server" id="linkBindings" href="">
                                    <img src="/Content/Assets/Images/web.png" class="TaskListIcon" border="0" />
                                </a>
                            </td>
                            <td class="Header">Bindings
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Site Bindings
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr runat="server" id="rowResources">
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <a id="linkResources" runat="server" href="">
                                    <img src="/Content/Assets/Images/Image.png" class="TaskListIcon" border="0" />
                                </a>
                            </td>
                            <td class="Header">Resources
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Place description here
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="rowSecurity" runat="server">
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <a runat="server" id="linkSecurity" href="">
                                    <img src="/Content/Assets/Images/lock.png" class="TaskListIcon" border="0" />
                                </a>
                            </td>
                            <td class="Header">Security
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Security options
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr runat="server" id="rowParameters">
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <a id="linkParameters" runat="server" href="">
                                    <img src="/Content/Assets/Images/piece.png" class="TaskListIcon" border="0" />
                                </a>
                            </td>
                            <td class="Header">Parameters
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Place description here
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <div class="col-md-8">
        <div class="Header" style="margin-bottom: 10px">
            Quick Info
        </div>
        <table>
            <tr>
                <td style="width: 150px">Name
                </td>
                <td>
                    <strong runat="server" id="lblName"></strong>
                </td>
            </tr>
            <tr>
                <td>Identity
                </td>
                <td>
                    <strong runat="server" id="lblIdentity"></strong>
                </td>
            </tr>
            <tr runat="server" id="fieldPrimaryBinding" visible="false">
                <td>Primary Binding
                </td>
                <td>
                    <a href="#" runat="server" id="lblPrimaryBinding"></a>
                </td>
            </tr>
            <tr>
                <td>Home Page
                </td>
                <td>
                    <strong runat="server" id="lblHomePage"></strong>
                </td>
            </tr>
            <tr>
                <td>Default Master Page
                </td>
                <td>
                    <strong runat="server" id="lblMasterPage"></strong>
                </td>
            </tr>
            <tr>
                <td>Public Access
                </td>
                <td>
                    <strong runat="server" id="lblPublicAccess"></strong>
                </td>
            </tr>
            <tr>
                <td>Mgmt Access
                </td>
                <td>
                    <strong runat="server" id="lblMgmtAccess"></strong>
                </td>
            </tr>
            <tr>
                <td>Active
                </td>
                <td>
                    <strong runat="server" id="lblActive"></strong>
                </td>
            </tr>
            <tr>
                <td>Rank
                </td>
                <td>
                    <strong runat="server" id="lblRank"></strong>
                </td>
            </tr>
        </table>
    </div>
</div>
