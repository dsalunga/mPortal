<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="WCMS.WebSystem.WebParts.Misc._Sections_DateDisplay_CMS_DateDisplay" Codebehind="CMS_DateDisplay.ascx.cs" %>
<table>
    <tr>
        <td>
            <table>
                <tr>
                    <td>
                        Format String:</td>
                    <td>
                        <asp:TextBox ID="txtFormatString" runat="server" Columns="50">{0:MMMM d, yyyy}</asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button ID="cmdUpdate" runat="server" Text="Update" Width="85px" Font-Bold="True"
                Height="35px" OnClick="cmdUpdate_Click" />
        </td>
    </tr>
</table>
<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>