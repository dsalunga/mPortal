<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebRoleHome.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.Security.WebRoleHome" %>
<%@ Register Src="../Controls/WebRoleTab.ascx" TagName="WebRoleTab" TagPrefix="uc1" %>
<uc1:WebRoleTab ID="WebRoleTab1" runat="server" />
<table width="100%">
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <table border="0" cellpadding="0">
                <tr>
                    <td rowspan="2">
                        <a id="linkProperties" runat="server" href="">
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
            <br />
        </td>
    </tr>
</table>
