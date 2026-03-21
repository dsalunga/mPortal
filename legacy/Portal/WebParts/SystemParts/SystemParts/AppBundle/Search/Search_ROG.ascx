<%@ Control Language="C#" ClassName="WCMS.WebSystem.WebParts.Search.Search_ROG" Inherits="WCMS.WebSystem.WebParts.Search.SearchView" Codebehind="Search.ascx.cs" %>
<table align="right">
    <tr>
        <td><img height="11" alt="" width="52" border="0" src="/Assets/Uploads/Image/Sites/ROG2/text_sitesearch.jpg" /></td>
        <td><asp:TextBox CssClass="search" ID="txtSearch" runat="server"></asp:TextBox></td>
        <td><asp:LinkButton ID="LinkButton1" runat="server" OnClick="cmdFind_Click"><img height="18" alt="" width="27" src="/Assets/Uploads/Image/Sites/ROG2/btn_go.jpg" border="0" /></asp:LinkButton></td>
    </tr>
</table>