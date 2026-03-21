<%@ Control Language="c#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.GenericForm.CMS_CreateSurveyPage"
    CodeBehind="WM_CreateSurveyPage_04.ascx.cs" %>
<table width="100%" border="0">
    <tr>
        <td width="120" valign="top">
            Page Title:
        </td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" Columns="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Ranking:
        </td>
        <td>
            <asp:TextBox ID="txtRank" runat="server" Width="64px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="ControlBox">
            <asp:Button ID="cmdQUpdate" runat="server" CssClass="Command" Width="72px" Text="Save" OnClick="cmdQUpdate_Click" />
            <asp:Button ID="cmdCancel" runat="server" CssClass="Command" Width="72px" Text="Cancel" OnClick="cmdCancel_Click" />
        </td>
    </tr>
</table>
