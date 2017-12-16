<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigAnimatedBanner.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Photo.ConfigAnimatedBanner" %>
<%@ Register Src="../Central/Controls/ParameterSetSelector.ascx" TagName="ParameterSetSelector"
    TagPrefix="uc1" %>
<table>
    <tr>
        <td style="width: 84px">Photo Album:
        </td>
        <td>
            <asp:DropDownList ID="cboAlbum" runat="server" DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Template:
        </td>
        <td>
            <uc1:ParameterSetSelector ID="ParameterSetSelector1" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Render Mode:
        </td>
        <td>
            <asp:DropDownList ID="cboRenderMode" runat="server">
                <asp:ListItem Selected="True" Value="0">Random</asp:ListItem>
                <asp:ListItem Value="1">By Order</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
