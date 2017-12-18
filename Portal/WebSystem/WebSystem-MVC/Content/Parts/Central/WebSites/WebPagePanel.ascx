<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPagePanel.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebPagePanelController" %>
<%@ Register Src="../Controls/WebPagePanelTab.ascx" TagName="PanelTab" TagPrefix="uc1" %>
<%@ Register Src="../Controls/WebGenericTab.ascx" TagName="WebGenericTab" TagPrefix="uc2" %>
<uc2:WebGenericTab ID="WebGenericTab1" runat="server" />
<br />
<uc1:PanelTab ID="PanelTab1" runat="server" />
<table width="100%">
    <tr>
        <td style="width: 100px">Panel Usage:
        </td>
        <td>
            <asp:DropDownList ID="cboPanelUsage" runat="server" CssClass="input">
                <asp:ListItem Selected="True" Value="-1">Inherit - Use Only Master Page Elements</asp:ListItem>
                <asp:ListItem Value="0">Add - Combine Master Page and Page Elements</asp:ListItem>
                <asp:ListItem Value="1">Override - Use Only Page Elements</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="cmdCancel_Click" />
    </div>
</div>
