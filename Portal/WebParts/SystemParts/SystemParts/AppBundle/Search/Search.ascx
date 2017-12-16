<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Search.SearchView" Codebehind="Search.ascx.cs" %>
<table cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td>
            <asp:TextBox ID="txtSearch" runat="server" Columns="25" /></td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="cmdFind" Text=" Search " runat="server" OnClick="cmdFind_Click" /></td>
    </tr>
    <tr>
        <td align="left" colspan="2">
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Selected="True">This site only</asp:ListItem>
                <asp:ListItem>All sites</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
</table>
