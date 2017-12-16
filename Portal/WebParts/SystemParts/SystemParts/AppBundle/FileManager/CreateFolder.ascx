<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateFolder.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.FileManager.CreateFolder" %>
<%@ Register Src="Controls/Breadcrumb.ascx" TagName="Breadcrumb" TagPrefix="uc1" %>
<div class="_wp_FileManager wp-FileManager">
    <uc1:Breadcrumb ID="Breadcrumb1" runat="server" />
    <br />
    <div id="createFolder">
        <label for="txtNewFolder">
            New Folder:</label>&nbsp;
        <asp:TextBox ID="txtNewFolder" CssClass="newFolderBox input" ClientIDMode="Static" MaxLength="246" runat="server"></asp:TextBox>
    </div>
    <br />
    <div id="buttonBarRow">
        <div id="buttonBar" class="buttonBar">
            <asp:Button ID="cmdUploadNow" Width="110px" CssClass="btn btn-primary" runat="server" Text="Create folder" OnClick="cmdUploadNow_Click" />&nbsp;
            <asp:Button ID="cmdCancel" Width="75px" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmdCancel_Click" />
        </div>
    </div>
    <br />
    <asp:Label CssClass="Header" Style="color: Red" ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
</div>
