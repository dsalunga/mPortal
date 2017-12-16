<%@ Control Language="c#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.GenericForm.SurveyWelcome"
    CodeBehind="SurveyWelcome.ascx.cs" %>
<table width="100%" border="0" cellspacing="10" cellpadding="0">
    <tr>
        <td align="center">
            <strong>
                <asp:Label ID="lblTitle" runat="server"></asp:Label></strong>
        </td>
    </tr>
    <tr>
        <td align="center">
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="cmdStart" runat="server" Text="Start Now!" Width="100px" Height="25px"
                OnClick="cmdStart_Click" />
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
