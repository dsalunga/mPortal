﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebRegistryTree.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Tools.WebRegistryTree" %>
<%--<%@ Register Src="../Controls/TreeControls.ascx" TagName="TreeControls" TagPrefix="uc1" %>
<uc1:TreeControls ID="TreeControls1" runat="server" />--%>
<div>
    <asp:TreeView ID="TreeView1" runat="server" CollapseImageUrl="/Content/Assets/Images/TreeView/c.gif"
        ExpandImageUrl="/Content/Assets/Images/TreeView/e.gif" LineImagesFolder="/Content/Assets/Images/TreeView/l"
        EnableTheming="True" NodeWrap="False" NoExpandImageUrl="/Content/Assets/Images/TreeView/ne.gif"
        AutoGenerateDataBindings="False" ClientIDMode="Static">
        <ParentNodeStyle Font-Bold="True" ImageUrl="/Content/Assets/Images/TreeView/f.gif" />
        <HoverNodeStyle Font-Underline="False" Font-Overline="False" ForeColor="DarkOrange" />
        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" />
        <NodeStyle ForeColor="#3366CC" HorizontalPadding="5px"
            VerticalPadding="2px" />
        <RootNodeStyle Font-Bold="True" ImageUrl="/Content/Assets/Images/TreeView/w.gif" />
        <LeafNodeStyle Font-Bold="False" ImageUrl="/Content/Assets/Images/TreeView/t.gif" />
    </asp:TreeView>
</div>
