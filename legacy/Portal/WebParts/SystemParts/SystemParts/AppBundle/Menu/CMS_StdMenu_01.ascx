<%@ Control Language="C#" AutoEventWireup="true" Inherits="_Sections_STDMENU_CMS_StdMenu_01"
    CodeBehind="CMS_StdMenu_01.ascx.cs" %>
<table>
    <tr>
        <td>
            <table>
                <tr>
                    <td style="width: 84px">
                        Target Site:
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 84px">
                        Section Node:
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 84px">
                        Orientation:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboOrientation" runat="server">
                            <asp:ListItem Selected="True">Horizontal</asp:ListItem>
                            <asp:ListItem>Vertical</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 84px">
                        Home Text:
                    </td>
                    <td>
                        <asp:TextBox ID="txtHome" runat="server" Columns="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 84px">
                        Width:
                    </td>
                    <td>
                        <asp:TextBox ID="txtWidth" runat="server" Columns="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 84px">
                        Height:
                    </td>
                    <td>
                        <asp:TextBox ID="txtHeight" runat="server" Columns="50"></asp:TextBox>
                    </td>
                </tr>
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
