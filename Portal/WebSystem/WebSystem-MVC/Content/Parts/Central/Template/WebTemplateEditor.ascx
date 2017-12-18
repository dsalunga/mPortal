<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebTemplateEditor.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Template.WebTemplateEditorController" %>
<h1 class="central page-header">
    <asp:Literal ID="lMessage" runat="server" Text="Web Template"></asp:Literal>
</h1>
<table width="100%">
    <tr>
        <td width="100">Name:
        </td>
        <td>
            <asp:TextBox ID="txtName" ReadOnly="true" CssClass="span5" runat="server" Columns="100"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td valign="top">Content:
        </td>
        <td>
            <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="30" Columns="100" CssClass="span7"
                Wrap="False"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Folder Name:
        </td>
        <td>
            <asp:TextBox ID="txtIdentity" ReadOnly="true" runat="server" Columns="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>File Name:
        </td>
        <td>
            <asp:TextBox ID="txtControlURL" ReadOnly="true" runat="server" Columns="50"></asp:TextBox>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" />
    </div>
</div>
