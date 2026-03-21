<%@ Register TagPrefix="fckeditorv2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<%@ Control Language="c#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.GenericForm.CreateSurvey"
    CodeBehind="WM_CreateSurvey_02.ascx.cs" %>
<table width="100%" border="0">
    <tr>
        <td width="120">
            <font size="2">Survey Title:</font>
        </td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" Width="100%" Font-Name="Verdana"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td valign="top">
            <font size="2">Welcome Text:</font>
        </td>
        <td>
            <fckeditorv2:FCKeditor ID="txtdesc" runat="server" Height="200px" ToolbarSet="Basic">
            </fckeditorv2:FCKeditor>
        </td>
    </tr>
    <tr>
        <td valign="top">
            <font size="2">Ending Text:</font>
        </td>
        <td>
            <fckeditorv2:FCKeditor ID="txtEnding" runat="server" Height="200px" ToolbarSet="Basic">
            </fckeditorv2:FCKeditor>
        </td>
    </tr>
    <tr>
        <td valign="top">
        </td>
        <td>
            <asp:CheckBox ID="chkActive" runat="server" Text="Active"></asp:CheckBox>&nbsp;
            <asp:CheckBox ID="chkPageTitle" runat="server" Text="Show Page Title"></asp:CheckBox>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" class="ControlBox">
            <asp:Button ID="cmdCreate" CssClass="Command" runat="server" Width="72px" Text="Update" OnClick="cmdCreate_Click">
            </asp:Button>&nbsp;
            <asp:Button ID="cmdCancel" runat="server" CssClass="Command" Width="64px" Text="Cancel" OnClick="cmdCancel_Click">
            </asp:Button>
        </td>
    </tr>
</table>
