<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Photo.Controls.FancyBoxThumbnails"
    EnableViewState="False" CodeBehind="FancyBoxThumbnails.ascx.cs" %>
<script type="text/javascript">
    $(document).ready(function () {

        function formatTitle(title, currentArray, currentIndex, currentOpts) {
            return '<div id="fancybox-custom-title"><span><a href="javascript:;" onclick="$.fancybox.close();"><img title="close" src="/Content/Assets/Images/closelabel.gif" /></a></span>' + (title && title.length ? '<strong>' + title + '</strong>' : '') + 'Photo ' + (currentIndex + 1) + ' of ' + currentArray.length + '</div>';
        }

        $("a[rel=photo_album]").fancybox({
            'transitionIn': 'elastic',
            'transitionOut': 'elastic',
            'titlePosition': 'inside',
            'cyclic': true,
            'overlayOpacity': 0.5,
            'overlayColor': '#000',
            'showCloseButton': false,
            'titleFormat': formatTitle
        });
    });

    /*'titleFormat'       : function(title, currentArray, currentIndex, currentOpts) {
    return '<span id="fancybox-title-inside"><div class="img-pos">Photo ' + (currentIndex + 1) + ' of ' + currentArray.length + '</div>' + (title.length ? title : '') + '</span>';
    */
</script>
<table cellspacing="2" cellpadding="0" border="0" class="gallery-thumb-fancybox">
    <tr>
        <td valign="top">
            <h3>
                <asp:Literal ID="lCategory" runat="server"></asp:Literal></h3>
        </td>
    </tr>
    <tr>
        <td align="left" style="padding-right: 15px">
            <h4>
                <a href='<%: sMainLink %>' title="Gallery Albums">Back to Gallery Albums</a>
            </h4>
        </td>
    </tr>
    <tr>
        <td valign="top" align="left">
            <table cellspacing="0" cellpadding="0" border="0">
                <asp:Literal ID="lThumbs" Text="<tr align='center' valign='top'>$<td valign='top' style='padding: 10px 15px 10px 0;'><a href='[LINK]' title='[TITLE]' rel='photo_album'><img class='fancybox-img' src='[SRC]' border='0' alt='' /></a></td>"
                    Visible="false" runat="server"></asp:Literal></table>
        </td>
    </tr>
    <tr>
        <td>
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
