<%@ Control Language="C#" AutoEventWireup="true" Inherits="_Sections_P_FeedBack" Codebehind="FeedBack.ascx.cs" %>
<asp:Panel ID="pnlContent" runat="server" Height="50px" Width="100%">
    <table border="0" width="100%">
        <tr>
            <td style="font-weight: bold;" align="center">
                Feedback
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:TextBox ID="txtFeed" runat="server" Columns="50" Height="200px" TextMode="MultiLine"
                    Width="400px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="Send Feedback" /></td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlThank" runat="server" Height="50px" Width="100%" Visible="false">
    <table border="0" width="100%">
        <tr>
            <td align="center" style="font-weight: bolder;">
                Thank you for your feedback!
            </td>
        </tr>
    </table>
</asp:Panel>
