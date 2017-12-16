<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Photo.Controls.FullView"
    EnableViewState="False" CodeBehind="FullView.ascx.cs" %>
<script language="javascript" type="text/javascript">
    function LoadImage(sSrc, sCaption) {
        var imageFullView = document.getElementById('imageFull');
        document.getElementById('spanImageCaption').innerHTML = sCaption;

        imageFullView.removeAttribute("width");
        imageFullView.src = '<%= AlbumFolder %>' + sSrc;
    }

    function ResizeImage(srcImage, maxWidth) {
        if (srcImage.width > maxWidth) {
            srcImage.width = maxWidth.toString();
        }
    }
</script>
<table cellspacing="2" cellpadding="0" width="100%" border="0" class="gallery-fullview">
    <tr>
        <td valign="top">
            <h3>
                <strong><span id="spanImageCaption">
                    <%= sImageCaption %></span>&nbsp;-&nbsp;<asp:Literal ID="lCategory" runat="server"></asp:Literal>
                </strong>
            </h3>
        </td>
    </tr>
    <tr>
        <td align="right">
            <h4>
                <a href="<%= sThumbnailLink %>" title="Thumbnail View">Thumbnail View</a>&nbsp;|&nbsp;<a
                    href="<%= AlbumsLink %>" title="Gallery Albums">Gallery Albums</a></h4>
        </td>
    </tr>
    <tr>
        <td valign="top">
            <table cellspacing="2" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="content" align="center">
                        <div class="clearfix" id="photoborder">
                            <div style="margin: 0px; overflow: auto; width: <%= MaxPhotoWidth %>px" onscroll="javascript:ResizeImage(document.getElementById('imageFull'), <%= MaxPhotoWidth %>);">
                                <img alt="" onload="javascript:ResizeImage(this);" name="imageFull" id="imageFull"
                                    src='<%= sFullImage %>' border="0" /></div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="left">
                        <table cellpadding="0" border="0">
                            <asp:Literal ID="lThumbs" runat="server" Text="<tr align='center' valign='top'>$<td style='padding: 6px 12px 6px 0;'><a href='[LINK]' title='[TITLE]'><img src='[SRC]' height='[HEIGHT]' border='0' /></a></td>"
                                Visible="false"></asp:Literal></table>
                    </td>
                </tr>
                <tr>
                    <td align="left" height="25">
                        <h4>
                            <asp:Literal ID="lNums" runat="server" Text=" <span>[#]</span>$ <a href='[URL]'>[#]</a>"
                                Visible="False"></asp:Literal>&nbsp;
                            <asp:Literal ID="lPrev" runat="server" Text="<a href='[URL]'>Previous</a>" Visible="False"></asp:Literal>&nbsp;<asp:Literal
                                ID="lNext" runat="server" Text="<a href='[URL]'>Next</a>" Visible="False"></asp:Literal>
                        </h4>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
