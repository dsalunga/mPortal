<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Central.Controls.PartDesignTemplate"
    CodeBehind="ElementDesigner.ascx.cs" %>
<div class="ItemContainer" id="<% =ContainerID %>">
    <div class="ItemContainerDragBox" id="<% =ItemID %>">
        <table runat="server" id="panelDesignerHead" class="element-designer-head designer-page">
            <tr>
                <td valign="top" class="ElementTools">
                    <table class="ElementToolbox" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="ElementControls">
                                <a runat="server" id="linkEditMode" href="#" title="Settings: Manage or load the configuration module for this element if there is any">
                                    <img src="/Content/Assets/Images/Common/Objects.gif" class="image-link" alt="Settings" /></a>
                            </td>
                            <td class="ElementControls">
                                <a runat="server" id="linkConfigure" href="#">
                                    <img src="/Content/Assets/Images/Common/ico_edit.gif" class="image-link" alt="Configure" title="Configure: View configuration page for this element" /></a>
                            </td>
                            <td class="ElementControls" id="tdDelete" runat="server">
                                <asp:ImageButton runat="server" CommandName="PageElement" CausesValidation="false"
                                    ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif" ID="imageDelete" AlternateText="Delete"
                                    OnClientClick="return confirm('Are you sure you want to delete this item permanently?');"
                                    OnClick="imageDelete_Click" ToolTip="Delete: Delete this Element permanently" />
                            </td>
                            <td class="ElementName" align="left" runat="server" id="labelModuleName">
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 7px;">
                    <img class="Collapse-Expand" onclick="togglePanel(this.parentNode.parentNode.parentNode.parentNode, this)"
                        src="/Content/Assets/Images/common/collapsepanel.gif" alt="Toggle" title="Toggle Collapse/Expand" />
                </td>
            </tr>
        </table>
        <div class="ItemContainerDragBox-Content">
            <div runat="server" id="itemContainer">
            </div>
        </div>
    </div>
</div>
