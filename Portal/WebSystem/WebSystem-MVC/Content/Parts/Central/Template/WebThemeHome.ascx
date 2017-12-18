<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebThemeHome.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Template.WebThemeHome" %>
<%@ Register Src="../Controls/WebThemeHome.ascx" TagName="WebThemeHome" TagPrefix="uc1" %>
<uc1:WebThemeHome ID="WebThemeHome1" runat="server" />
<br />
<div style="float: left">
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
                            <a id="linkResources" runat="server" href="">
                                <img runat="server" src="~/Content/Assets/Images/Image.png" class="TaskListIcon" border="0" />
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
                <br />
            </td>
        </tr>
    </table>
</div>
<div style="float: left; margin-top: 10px; margin-left: 150px">
    <h3 class="page-header1" style="margin-top: 5px">
        Quick Info</h3>
    <table>
        <tr>
            <td style="width: 100px">
                Name
            </td>
            <td>
                <strong runat="server" id="lblName"></strong>
            </td>
        </tr>
        <tr>
            <td>
                Identity
            </td>
            <td>
                <strong runat="server" id="lblIdentity"></strong>
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
                Default Template
            </td>
            <td>
                <strong runat="server" id="lblDefaultTemplate"></strong>
            </td>
        </tr>
        <tr>
            <td>
                Default Skin
            </td>
            <td>
                <strong runat="server" id="lblDefaultSkin"></strong>
            </td>
        </tr>
    </table>
</div>
