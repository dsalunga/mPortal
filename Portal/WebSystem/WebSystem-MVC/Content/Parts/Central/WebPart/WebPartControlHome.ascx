<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPartControlHome.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebPartControlHome" %>
<%@ Register Src="../Controls/WebPartControlTab.ascx" TagName="WebPartControlTab"
    TagPrefix="uc1" %>
<uc1:WebPartControlTab ID="WebPartControlTab1" runat="server" />
<table width="100%">
    <tr>
        <td>
            <table border="0" cellpadding="0">
                <tr>
                    <td rowspan="2">
                        <a runat="server" id="linkConfigure" href="">
                            <img src="/Content/Assets/Images/file_edit.png" class="TaskListIcon" border="0" />
                        </a>
                    </td>
                    <td class="Header">
                        Properties
                    </td>
                </tr>
                <tr>
                    <td valign="top">
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
                    <td class="Header">
                        Delete this
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Place description here
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
                        <a id="linkSecurity" runat="server" href="">
                            <img src="/Content/Assets/Images/lock.png" class="TaskListIcon" border="0" />
                        </a>
                    </td>
                    <td class="Header">
                        Security
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Security options
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
                            <img src="/Content/Assets/Images/Image.png" class="TaskListIcon" border="0" />
                        </a>
                    </td>
                    <td class="Header">
                        Resources
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Place description here
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
                        <a id="linkParameters" runat="server" href="">
                            <img src="/Content/Assets/Images/piece.png" class="TaskListIcon" border="0" />
                        </a>
                    </td>
                    <td class="Header">
                        Parameters
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Place description here
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
