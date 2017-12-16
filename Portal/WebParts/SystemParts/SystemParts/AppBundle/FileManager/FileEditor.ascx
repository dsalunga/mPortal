<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileEditor.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.FileManager.FileEditor" %>
<h1>Text File Editor</h1>
<table width="100%" border="0" cellpadding="0" cellspacing="0" style="padding-right: 5px">
    <tr>
        <td class="Header">
            <asp:Label ID="lblStatus" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="35" Wrap="False"
                Font-Names="Courier New,Arial,Verdana" Font-Size="10pt" Width="100%"></asp:TextBox>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" Text="Update" OnClick="cmdUpdate_Click"
            CssClass="btn btn-primary" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmdCancel_Click" />
    </div>
</div>
