<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Newsletter.eNewsletter" AutoEventWireup="true" Codebehind="eNewsLetter.ascx.cs" %>
<table cellspacing="2" cellpadding="0" border="0">
    <tr>
        <td style="height: 24px">
            <asp:TextBox ID="txtEmailAddress" runat="server" Columns="21">Enter Your E-mail</asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"></asp:Button>
        </td>
    </tr>
</table>
<asp:Literal ID="litNotify" runat="server" />