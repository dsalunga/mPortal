<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Photo.Controls.ThumbnailView"
    EnableViewState="False" CodeBehind="Thumbnails.ascx.cs" %>
<table cellspacing="2" cellpadding="0" border="0" class="gallery-thumb">
    <tr>
        <td valign="top">
            <h3>
                <strong>
                    <asp:Literal ID="lCategory" runat="server"></asp:Literal></strong></h3>
        </td>
    </tr>
    <tr>
        <td align="right">
            <h4>
                <a href='<%: mainLink %>' title="Gallery Albums">Gallery Albums</a>
            </h4>
        </td>
    </tr>
    <tr>
        <td valign="top" align="left">
            <table cellspacing="15" cellpadding="1" border="0">
                <asp:Literal ID="lThumbs" Text="<tr align='center' valign='top'>$<td valign='top' style='padding: 10px 15px 10px 0;'><a href='[LINK]' title='[TITLE]'><img src='[SRC]' border='0' /></a></td>"
                    Visible="false" runat="server"></asp:Literal></table>
        </td>
    </tr>
    <tr>
        <td height="25">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td nowrap="nowrap">
                        <h4>
                            <asp:Literal ID="lNums" Visible="False" runat="server" Text=" <span>[#]</span>$ <a href='[URL]'>[#]</a>"></asp:Literal>&nbsp;
                            <asp:Literal ID="lPrev" Visible="False" runat="server" Text="<a href='[URL]'>Previous</a>"></asp:Literal>
                            <asp:Literal ID="lNext" Visible="False" runat="server" Text="<a href='[URL]'>Next</a>"></asp:Literal>
                        </h4>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
