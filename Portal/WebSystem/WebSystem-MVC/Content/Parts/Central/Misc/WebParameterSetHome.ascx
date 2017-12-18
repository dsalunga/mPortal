<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebParameterSetHome.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Misc.WebParameterSetHome" %>
<%@ Register Src="../Controls/WebGenericTab.ascx" TagName="WebGenericTab" TagPrefix="uc1" %>
<uc1:WebGenericTab ID="WebGenericTab1" runat="server" Title="Parameters" />
<table width="100%">
    <%--<tr>
        <td class="Header" id="tdHeader" runat="server">
            &nbsp;
        </td>
    </tr>--%>
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
