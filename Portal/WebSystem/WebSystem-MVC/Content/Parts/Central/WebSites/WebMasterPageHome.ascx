<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebMasterPageHome.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebMasterPageHome" %>
<%@ Register Src="../Controls/WebGenericTab.ascx" TagName="WebGenericTab" TagPrefix="uc1" %>
<uc1:WebGenericTab ID="WebGenericTab1" runat="server" />
<div class="row">
    <div class="col-md-4">
        <table>
            <tr runat="server" id="rowProperties">
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <a id="linkProperties" runat="server" href="">
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
            <tr id="rowPanels" runat="server">
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
            <tr runat="server" id="rowElements">
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <a href="#" id="linkElements" runat="server">
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
            <tr id="rowResources" runat="server">
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <a id="linkResource" runat="server" href="">
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
                <td style="width: 100px">Name
                </td>
                <td>
                    <strong runat="server" id="lblName"></strong>
                </td>
            </tr>
            <tr>
                <td>Template
                </td>
                <td>
                    <strong runat="server" id="lblTemplateName"></strong>
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
        </table>
    </div>
</div>
