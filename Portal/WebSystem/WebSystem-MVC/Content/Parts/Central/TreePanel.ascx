﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TreePanel.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.TreePanel" %>
<%@ Register Src="~/Content/Parts/Central/Controls/TreeControls.ascx" TagName="TreeControls" TagPrefix="uc1" %>
<uc1:TreeControls ID="TreeControls1" runat="server" />
<div>
    <asp:TreeView ID="t" runat="server" Target="frameMain" CollapseImageUrl="/Content/Assets/Images/TreeView/c.gif"
        ExpandImageUrl="/Content/Assets/Images/TreeView/e.gif" LineImagesFolder="/Content/Assets/Images/Common/TreeView/l"
        EnableTheming="True" NodeWrap="False" NoExpandImageUrl="/Content/Assets/Images/TreeView/ne.gif"
        AutoGenerateDataBindings="False" ClientIDMode="Static">
        <ParentNodeStyle Font-Bold="True" ImageUrl="/Content/Assets/Images/TreeView/f.gif" />
        <HoverNodeStyle Font-Underline="False" Font-Overline="False" ForeColor="DarkOrange" />
        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" />
        <NodeStyle ForeColor="#3366CC" HorizontalPadding="5px" VerticalPadding="2px" />
        <RootNodeStyle Font-Bold="True" ImageUrl="/Content/Assets/Images/TreeView/w.gif" />
        <LeafNodeStyle Font-Bold="False" ImageUrl="/Content/Assets/Images/TreeView/t.gif" />
    </asp:TreeView>
    <div style="text-align: center; display: none">
        <img src="/Content/Assets/Images/Common/indicator.gif" />
    </div>
    <span id="lblStatus" class="system-label" runat="server"></span>
</div>
