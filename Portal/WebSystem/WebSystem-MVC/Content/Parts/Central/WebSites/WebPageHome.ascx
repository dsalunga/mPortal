<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPageHome.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebPageHome" %>
<%@ Register Src="../Controls/WebPageTab.ascx" TagName="WebPageTab" TagPrefix="uc1" %>
<uc1:WebPageTab ID="WebPageTab1" runat="server" />
<asp:HiddenField runat="server" ID="hidPageURL" Value="" />
<div class="row">
    <div class="col-md-4">
        <table border="0">
            <tr id="taskList" runat="server" visible="false">
                <td>
                    <table width="100%">
                        <tr runat="server" id="rowProperties">
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td rowspan="2">
                                            <a href="#" id="linkConfigPage" runat="server">
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
                        <tr>
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td rowspan="2">
                                            <a href="#" id="linkManageContent" runat="server">
                                                <img src="/Content/Assets/Images/misc.png" class="TaskListIcon" border="0" />
                                            </a>
                                        </td>
                                        <td class="Header">Settings
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Place description here
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server" id="panelMenuManager">
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td rowspan="2">
                                            <a href="#" id="linkMenuManager" runat="server">
                                                <img src="/Content/Assets/Images/plugin.png" class="TaskListIcon" border="0" />
                                            </a>
                                        </td>
                                        <td class="Header">Menu Manager
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Place description here
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server" id="panelCommentManager" visible="false">
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td rowspan="2">
                                            <a href="#" id="linkCommentManager" runat="server">
                                                <img src="/Content/Assets/Images/plugin.png" class="TaskListIcon" border="0" />
                                            </a>
                                        </td>
                                        <td class="Header">Comment Manager
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Place description here
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server" id="rowLinkedParts">
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td rowspan="2">
                                            <a href="#" id="linkLinkedParts" runat="server">
                                                <img src="/Content/Assets/Images/plugin.png" class="TaskListIcon" border="0" />
                                            </a>
                                        </td>
                                        <td class="Header">Linked Parts
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Place description here
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server" id="rowPanels">
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td rowspan="2">
                                            <a href="#" id="linkPanels" runat="server">
                                                <img src="/Content/Assets/Images/folder.png" class="TaskListIcon" border="0" />
                                            </a>
                                        </td>
                                        <td class="Header">Panels
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
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td rowspan="2">
                                            <a href="#" runat="server" id="linkPageElements">
                                                <img src="/Content/Assets/Images/grafle.png" class="TaskListIcon" border="0" />
                                            </a>
                                        </td>
                                        <td class="Header">Elements
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Place description here
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
                        <tr runat="server" id="rowSecurity">
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td rowspan="2">
                                            <a id="linkSecurity" runat="server" href="">
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
                        <tr id="rowParameters" runat="server">
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
                    <br />
                </td>
            </tr>
            <!--
        <tr>
            <td>
                <br />
                <br />
                <br />
                <br />
                <span class="Header">Web Page Preview</span>
                <br />
                <br />
                <iframe id="iframePage" width="100%" height="400" src="about:blank"></iframe>
            </td>
        </tr>
        -->
        </table>
    </div>
    <div class="col-md-8">
        <div class="Header" style="margin-bottom: 10px">
            Quick Info
        </div>
        <table>
            <tr>
                <td style="width: 100px">Name
                </td>
                <td>
                    <strong runat="server" id="lblName"></strong>
                </td>
            </tr>
            <tr>
                <td>Type
                </td>
                <td>
                    <strong runat="server" id="lblPart"></strong>
                </td>
            </tr>
            <tr>
                <td>Master Page
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
            <tr>
                <td>Use Built-in Template
                </td>
                <td>
                    <strong runat="server" id="lblUseBuiltinTemplate"></strong>
                </td>
            </tr>
            <tr>
                <td>Page Type
                </td>
                <td>
                    <strong runat="server" id="lblPageType"></strong>
                </td>
            </tr>
        </table>
    </div>
</div>
