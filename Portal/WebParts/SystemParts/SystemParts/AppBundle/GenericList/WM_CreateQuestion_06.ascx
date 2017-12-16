<%@ Control Language="c#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.GenericForm.CreateQuestion"
    CodeBehind="WM_CreateQuestion_06.ascx.cs" %>
<table width="100%" border="0">
    <tr>
        <td width="120" valign="top">
            Question Text:
        </td>
        <td>
            <asp:TextBox ID="txtQLabel" runat="server" Height="104px" TextMode="MultiLine" Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Ranking:
        </td>
        <td>
            <asp:TextBox ID="txtQRanking" runat="server" Width="64px"></asp:TextBox>
            <asp:CheckBox ID="chkRequired" runat="server" Text="Required"></asp:CheckBox>
            <asp:CheckBox ID="chkHorizontal" runat="server" Text="Horizontal Orientation"></asp:CheckBox>
        </td>
    </tr>
    <tr>
        <td class="ControlBox" colspan="2">
            <asp:Button ID="cmdQUpdate" runat="server" CssClass="Command" Width="72px" Text="Save"
                OnClick="cmdQUpdate_Click" />
            <asp:Button ID="cmdCancel" runat="server" CssClass="Command" Width="72px" Text="Cancel"
                OnClick="cmdCancel_Click" />
    </tr>
</table>
