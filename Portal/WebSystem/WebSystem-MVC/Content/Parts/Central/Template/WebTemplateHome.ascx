<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebTemplateHome.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Template.WebTemplateHome" %>
<%@ Register Src="../Controls/WebTemplateHome.ascx" TagName="WebTemplateHome" TagPrefix="uc1" %>
<uc1:WebTemplateHome ID="WebTemplateHome1" runat="server" />
<br />
<div class="row">
    <div class="col-md-4">
        <table width="100%">
            <tr>
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <a href="#" id="linkEdit" runat="server">
                                    <img src="/Content/Assets/Images/edit.png" class="TaskListIcon" border="0" />
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
            <tr>
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <a href=".#" id="linkEditContent" runat="server">
                                    <img src="/Content/Assets/Images/misc.png" class="TaskListIcon" border="0" />
                                </a>
                            </td>
                            <td class="Header">Edit Content
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
                                <a id="linkResources" runat="server" href="">
                                    <img runat="server" src="~/Content/Assets/Images/Image.png" class="TaskListIcon" border="0" />
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
    <div class="col-md-6">
        <h3 class="page-header1" style="margin-top: 5px;">Quick Info</h3>
        <table>
            <tr>
                <td style="width: 100px">Name
                </td>
                <td>
                    <strong runat="server" id="lblName"></strong>
                </td>
            </tr>
            <tr>
                <td>Path
                </td>
                <td>
                    <strong runat="server" id="lblPath"></strong>
                </td>
            </tr>
            <tr runat="server" visible="false" id="rowParent">
                <td>Parent</td>
                <td>
                    <strong runat="server" id="lblParent"></strong>
                </td>
            </tr>
            <tr>
                <td>Theme
                </td>
                <td>
                    <strong runat="server" id="lblTheme"></strong>
                </td>
            </tr>
            <tr>
                <td>Default Panel
                </td>
                <td>
                    <strong runat="server" id="lblDefaultPanel"></strong>
                </td>
            </tr>
            <tr>
                <td>Default Skin
                </td>
                <td>
                    <strong runat="server" id="lblDefaultSkin"></strong>
                </td>
            </tr>
            <tr>
                <td>Is Standalone
                </td>
                <td>
                    <strong runat="server" id="lblStandalone"></strong>
                </td>
            </tr>
        </table>
    </div>
</div>
