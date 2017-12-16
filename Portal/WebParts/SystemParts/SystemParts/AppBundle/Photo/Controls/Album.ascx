<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Photo.Controls.AlbumView"
    CodeBehind="Album.ascx.cs" %>
<asp:DataList ID="rGallery" CssClass="gallery-album" runat="server" RepeatColumns="2" HorizontalAlign="left"
    CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" ItemStyle-VerticalAlign="Top">
    <ItemTemplate>
        <div style='padding-left: 0px; padding-right: <% =CellPadding %>px'>
            <table width="100%" border="0" cellspacing="2" align="center">
                <tr>
                    <td align="center" valign="top">
                        <a href='<%# CreateLink(Eval("Id").ToString()) %>' title='<%# Eval("Title") %>'>
                            <img class="fancybox-img" src='<%# BuildAlbumPath(Eval("ImageFile").ToString()) %>' border="0" alt='<%# Eval("Title") %>' /></a>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <h3>
                            <strong>
                                <%# Eval("Title") %></strong></h3>
                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:DataList>
