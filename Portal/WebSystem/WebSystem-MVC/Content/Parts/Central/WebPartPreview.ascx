<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPartPreview.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebPartPreview" %>
<div>
    <table border="0" style="background-color: ghostwhite;" width="100%" cellpadding="5">
        <tr>
            <td style="text-align: center">
                <asp:Image ID="imgPreview" CssClass="drop_shadow" runat="server" ImageUrl="~/Content/Assets/Images/PartThumb.jpg" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <strong>
                    <asp:Literal ID="lTemplateName" runat="server"></asp:Literal></strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Literal ID="lDescription" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</div>
