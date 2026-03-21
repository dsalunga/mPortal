<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RenameFile.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.FileManager.RenameFile" %>
<%@ Register Src="Controls/Breadcrumb.ascx" TagName="Breadcrumb" TagPrefix="uc1" %>
<div class="_wp_FileManager wp-FileManager">
    <uc1:Breadcrumb ID="Breadcrumb1" runat="server" />
    <br />
    <div class="field-line">
        <label class="field-label" for="txtOldName">
            Old Name:</label>
        <asp:TextBox ID="txtOldName" ReadOnly="true" CssClass="newFolderBox" ClientIDMode="Static"
            MaxLength="246" runat="server"></asp:TextBox>
    </div>
    <div class="field-line">
        <label class="field-label" for="txtNewName">
            New Name:<asp:RequiredFieldValidator ID="rfvNewName" runat="server" ErrorMessage="New Name is required"
                ControlToValidate="txtNewName">*</asp:RequiredFieldValidator></label>
        <asp:TextBox ID="txtNewName" CssClass="newFolderBox" ClientIDMode="Static" MaxLength="246"
            runat="server"></asp:TextBox>
    </div>
    <br />
    <div id="buttonBarRow">
        <div id="buttonBar" class="buttonBar">
            <asp:Button ID="cmdRename" Width="90px" runat="server" Text="Rename" OnClick="cmdRename_Click" />&nbsp;
            <asp:Button ID="cmdCancel" Width="75px" runat="server" Text="Cancel" OnClick="cmdCancel_Click" />
        </div>
    </div>
    <br />
    <asp:Label CssClass="Header" Style="color: Red" ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
