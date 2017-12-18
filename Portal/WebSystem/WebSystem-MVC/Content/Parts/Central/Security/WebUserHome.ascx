<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserHome.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Security.WebUserHome" %>
<%@ Register Src="../Controls/WebUserTab.ascx" TagName="WebUserTab" TagPrefix="uc1" %>

<uc1:WebUserTab ID="WebUserTab1" runat="server" />
<div class="row">
    <div class="col-md-4">
        <table>
            <tr id="rowProperties" runat="server">
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
            <tr runat="server" id="panelFixOrientation">
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <asp:LinkButton OnClientClick="return WCMS.Dom.Confirm('Are you sure you want to try to fix the orientation of the photo?');"
                                    ID="cmdFixOrientation" runat="server" OnClick="cmdFixOrientation_Click"><img src="/Content/Assets/Images/config.png" class="TaskListIcon" border="0" /></asp:LinkButton>
                            </td>
                            <td class="Header">Fix Photo Orientation
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Place description here
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr runat="server" id="rowGroups">
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <a id="linkGroups" runat="server" href="">
                                    <img src="/Content/Assets/Images/user_group.png" class="TaskListIcon" border="0" />
                                </a>
                            </td>
                            <td class="Header">Groups
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
                                <a id="linkSecurity" runat="server" href="">
                                    <img src="/Content/Assets/Images/lock.png" class="TaskListIcon" border="0" />
                                </a>
                            </td>
                            <td class="Header">Change Password
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
        <h3 class="page-header1">Quick Info</h3>
        <img src="/Content/Assets/Images/nophoto.png" class="img-responsive" width="300" runat="server" id="accountPhoto" style="border: solid 2px #ccc; margin: 2px 0 2px 0" />
        <table>
            <tr>
                <td valign="top" style="width: 120px">Full Name
                </td>
                <td id="lblFullName" runat="server" style="font-weight: bold"></td>
            </tr>
            <tr>
                <td>Email Address
                </td>
                <td id="lblEmail" runat="server" style="font-weight: bold"></td>
            </tr>
            <tr>
                <td>Mobile Number
                </td>
                <td id="lblMobileNumber" runat="server" style="font-weight: bold"></td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;
                </td>
            </tr>
            <tr>
                <td>Date Created
                </td>
                <td id="lblDateCreated" runat="server" style="font-weight: bold"></td>
            </tr>
            <tr>
                <td>Last Updated
                </td>
                <td id="lblLastUpdated" runat="server" style="font-weight: bold"></td>
            </tr>
            <tr>
                <td>Last Login
                </td>
                <td id="lblLastLogin" runat="server" style="font-weight: bold"></td>
            </tr>
            <%--<tr>
            <td>
            </td>
            <td align="center" style="background-color: #f9f7f7; padding: 2px">
                <img src="/Assets/Uploads/Image/Photo/silho.gif" title="User's Photo" />
            </td>
        </tr>--%>
            <tr>
                <td>Account Status
                </td>
                <td id="lblActive" runat="server" style="font-weight: bold"></td>
            </tr>
            <tr>
                <td>Password Expiry
                </td>
                <td id="lblPasswordExpiry" runat="server" style="font-weight: bold"></td>
            </tr>
            <tr>
                <td>Provider
                </td>
                <td id="lblProvider" runat="server" style="font-weight: bold"></td>
            </tr>
        </table>
    </div>
</div>
<div class="row">
    <div class="col-md-6">
        <div class="alert alert-warning" runat="server" id="lblMessage" enableviewstate="false" visible="false"></div>
    </div>
</div>
