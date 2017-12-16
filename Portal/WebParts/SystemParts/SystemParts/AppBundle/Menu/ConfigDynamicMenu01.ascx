<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Menu.MENU_CMS_Menu_01"
    CodeBehind="ConfigDynamicMenu01.ascx.cs" %>
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
                        <%--<a href="" runat="server" id="linkManageMenu">
                            <img src="/Content/Assets/Images/Common/Objects.gif" title="Manage" alt="Manage" class="icon" /></a>--%>
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
                            <asp:ListItem Selected="True" Value="0">Absolute (Render from root)</asp:ListItem>
                            <asp:ListItem Value="1">Relative (based on current page)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <%--<tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Orientation:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboOrientation" runat="server">
                            <asp:ListItem Selected="True" Value="1">Horizontal</asp:ListItem>
                            <asp:ListItem Value="2">Vertical</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Width:
                    </td>
                    <td>
                        <asp:TextBox ID="txtWidth" runat="server" Columns="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Height:
                    </td>
                    <td>
                        <asp:TextBox ID="txtHeight" runat="server" Columns="50"></asp:TextBox>
                    </td>
                </tr>--%>
            </table>
        </td>
    </tr>
    <%--    <tr>
        <td align="right">
            <asp:Button ID="cmdUpdate" runat="server" Text="Update" CssClass="Command" Width="80px"
                OnClick="cmdUpdate_Click" />
        </td>
    </tr>
    --%>
</table>
<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
