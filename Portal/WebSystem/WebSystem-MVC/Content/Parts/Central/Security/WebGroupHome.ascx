<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebGroupHome.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Security.WebGroupHome" %>
<%@ Register Src="../Controls/WebGroupTab.ascx" TagName="WebGroupTab" TagPrefix="uc1" %>
<uc1:WebGroupTab ID="WebGroupTab1" runat="server" />
<div style="float: left">
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
</div>
<div style="float: left; margin-top: 10px; margin-left: 150px">
    <h3 style="margin-bottom: 10px; margin-top: 5px">
        Quick Info</h3>
    <table>
        <tr>
            <td style="width: 120px">
                Name
            </td>
            <td>
                <strong runat="server" id="lblName"></strong>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                Description
            </td>
            <td style="vertical-align: top">
                <strong runat="server" id="lblDescription"></strong>
            </td>
        </tr>
        <tr>
            <td>
                Owner
            </td>
            <td>
                <strong runat="server" id="lblOwner"></strong>
            </td>
        </tr>
        <tr>
            <td>
                Parent
            </td>
            <td>
                <strong runat="server" id="lblParent"></strong>
            </td>
        </tr>
        <tr>
            <td>
                Page URL
            </td>
            <td>
                <strong runat="server" id="lblPageUrl"></strong>
            </td>
        </tr>
        <tr>
            <td>
                Require Approval
            </td>
            <td>
                <strong runat="server" id="lblRequireApproval"></strong>
            </td>
        </tr>
    </table>
</div>
