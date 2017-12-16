<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Menu.ConfigDynamicMenuV2"
    CodeBehind="ConfigDynamicMenuV2.ascx.cs" %>
<%@ Register Src="../Central/Controls/ParameterSetSelector.ascx" TagName="ParameterSetSelector"
    TagPrefix="uc1" %>
<%@ Register Src="Controls/AdminTabControl.ascx" TagName="AdminTabControl" TagPrefix="uc2" %>
<uc2:AdminTabControl ID="AdminTabControl1" runat="server" />
<table>
    <tr>
        <td>
            <table>
                <tr>
                    <td style="width: 100px">Menu:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboMenu" runat="server" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
                            <asp:ListItem Value="-1" Selected="True" Text="-- None, use site structure --"></asp:ListItem>
                        </asp:DropDownList>
                        <a href="" runat="server" id="linkManageMenu">
                            <img src="/Content/Assets/Images/Common/Objects.gif" title="Manage" alt="Manage" class="icon" /></a>
                    </td>
                </tr>
                <tr>
                    <td>Template:
                    </td>
                    <td>
                        <uc1:ParameterSetSelector ID="ParameterSetSelector1" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
