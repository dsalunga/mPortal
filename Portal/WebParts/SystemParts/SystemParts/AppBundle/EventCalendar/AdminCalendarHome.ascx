<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminCalendarHome.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.EventCalendar.AdminCalendarHome" %>
<div id="lblHeader" runat="server" class="Header-Main">
</div>
<br />
<table border="0" width="100%">
    <tr id="taskList" runat="server">
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
                <tr runat="server" id="rowDelete">
                    <td>
                        <!-- Delete -->
                        <table border="0" cellpadding="0">
                            <tr>
                                <td rowspan="2">
                                    <asp:LinkButton OnClientClick="return WCMS.Dom.Confirm('Are you sure you want to delete this item?');"
                                        ID="cmdDelete" runat="server" OnClick="cmdDelete_Click">
                                        <img src="/Content/Assets/Images/delete-folder.png" class="TaskListIcon" border="0" /></asp:LinkButton>
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
                <tr id="rowParameters" runat="server">
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
            <br />
        </td>
    </tr>
</table>
