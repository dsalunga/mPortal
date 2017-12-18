<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Central.Controls.PlaceHolderToolbox"
    CodeBehind="PanelDesigner.ascx.cs" %>
<div class="Panel">
    <table class="panel-toolbox panel-inherit" id="panelDesignerHead" runat="server" cellpadding="1" cellspacing="0">
        <tr>
            <td class="PanelControls" runat="server" id="panelNewElement">
                <a runat="server" id="linkNewElement" href="#">
                    <img src="/Content/Assets/Images/Common/ico_edit.gif" class="image-link" alt="New Element" title="Add a new Element" /></a>
            </td>
            <td class="PanelControls" runat="server" id="panelViewElements">
                <a runat="server" id="linkViewElements" href="#">
                    <img src="/Content/Assets/Images/Common/ico_pages.gif" class="image-link" alt="View Element List"
                        title="View list of all elements for this Panel" /></a>
            </td>
            <td class="PanelControls" runat="server" id="panelPanelConfig">
                <a runat="server" id="linkPanelConfig" href="#">
                    <img src="/Content/Assets/Images/Common/Tools.gif" class="image-link" alt="Panel Configuration"
                        title="Configurate this Panel" /></a>
            </td>
            <td class="PanelName" id="phName" runat="server">
            </td>
        </tr>
    </table>
    <div>
        <div id="divPlaceHolder" runat="server">
        </div>
    </div>
</div>
