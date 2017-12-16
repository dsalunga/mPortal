<%@ Control Language="C#" AutoEventWireup="true" Inherits="_Sections_STDMENU_CMS_ListingPage"
    CodeBehind="CMS_ListingPage.ascx.cs" %>
<table>
    <tr>
        <td>
            <table>
                <tr>
                    <td style="width: 84px">
                        Header Text:
                    </td>
                    <td>
                        <asp:TextBox ID="txtHeaderText" runat="server" Columns="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 84px">
                        Listing Type:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboType" runat="server">
                            <asp:ListItem Selected="True" Value="Full">Top Level</asp:ListItem>
                            <asp:ListItem Value="Relative">Relative Items</asp:ListItem>
                            <asp:ListItem Value="Child">Child Items</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Repeat Columns:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboRepeatColumns" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cell Padding:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboPadding" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <!--
                <tr>
                    <td style="width: 84px">
                        Width:</td>
                    <td>
                        <asp:TextBox ID="txtWidth" runat="server" Columns="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 84px">
                        Height:</td>
                    <td>
                        <asp:TextBox ID="txtHeight" runat="server" Columns="50"></asp:TextBox></td>
                </tr>
                -->
            </table>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button ID="cmdUpdate" runat="server" Text="Update" Width="80px" OnClick="cmdUpdate_Click" />
        </td>
    </tr>
</table>
<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>