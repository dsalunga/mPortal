<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebGroup.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.Security.WebGroupController" %>
<h1 class="central page-header" runat="server" id="lblHeader"></h1>
<table width="100%">
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td width="75">Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                        ErrorMessage="Name" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" CssClass="input" Columns="75"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">Description:
                    </td>
                    <td style="vertical-align: top">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="input" MaxLength="999" Columns="75" Rows="4" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Owner:
                    </td>
                    <td class="min-bottom-margin">
                        <asp:TextBox ID="txtOwner" ClientIDMode="Static" CssClass="input" runat="server" Columns="40"></asp:TextBox>
                        <input id="cmdBrowse" class="btn btn-default btn-sm" onclick="ShowAccountBrowser('txtOwner', <% =WCMS.Framework.WebObjects.WebUser %>, 0);" type="button" value="Browse..." />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">Managers:
                    </td>
                    <td style="vertical-align: top">
                        <asp:TextBox ID="txtManagers" runat="server" CssClass="input" MaxLength="999" Columns="75" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Parent:
                    </td>
                    <td class="min-bottom-margin">
                        <asp:TextBox ID="txtParent" ClientIDMode="Static" CssClass="input" runat="server" Columns="40"></asp:TextBox>
                        <input id="cmdParentBrowse" class="btn btn-default btn-sm" onclick="ShowAccountBrowser('txtParent', <% =WCMS.Framework.WebObjects.WebGroup %>, 0);" type="button" value="Browse..." />
                    </td>
                </tr>
                <tr>
                    <td style="width: 110px">Page URL:
                    </td>
                    <td class="min-bottom-margin">
                        <asp:TextBox ID="txtPageUrl" ClientIDMode="Static" CssClass="input" runat="server" Columns="60" />
                        <input id="cmdBrowsePage" onclick="BrowseLink('txtPageUrl',-1); return false;" class="btn btn-default btn-sm" value="Browse..." type="button" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:CheckBox ID="chkJoinApproval" CssClass="aspnet-checkbox" runat="server" Text="Require Approval" ToolTip="Require manager's approval when users join" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" />
    </div>
</div>
