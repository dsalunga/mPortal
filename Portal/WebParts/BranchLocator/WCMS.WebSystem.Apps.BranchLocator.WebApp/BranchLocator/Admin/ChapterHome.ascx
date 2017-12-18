<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChapterHome.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.BranchLocator.WebOfficeHome" %>
<div style="padding: 5px 0 5px 0">
    <span runat="server" id="lblBreadcrumb"></span>
</div>
<h2 runat="server" id="lblName">Chapter Name</h2>
<table width="100%">
    <tr>
        <td>
            <table border="0" cellpadding="0">
                <tr>
                    <td rowspan="2">
                        <a id="linkProperties" runat="server" href="#">
                            <img src="/Content/Assets/Images/file_edit.png" class="TaskListIcon" border="0" />
                        </a>
                    </td>
                    <td class="Header">
                        Properties
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
                        <a id="linkSetLocation" runat="server" href="#">
                            <img src="/Content/Assets/Images/file_edit.png" class="TaskListIcon" border="0" />
                        </a>
                    </td>
                    <td class="Header">
                        Set Location
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
            <!-- Delete -->
            <table border="0" cellpadding="0">
                <tr>
                    <td rowspan="2">
                        <asp:LinkButton OnClientClick="return confirm('Are you sure you want to delete this item?');"
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
    <%--<tr>
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
    </tr>--%>
    <tr>
        <td>
            <br />
        </td>
    </tr>
</table>
