<%@ Control Language="C#" %>

<script runat="server">

</script>

<table cellpadding="0" cellspacing="0" border="0" width="777" style="background-color: white;"
    align="center">
    <tr>
        <td id="top" runat="server" style="background-color: Fuchsia">
            <asp:PlaceHolder ID="phSiteSearch" runat="server"></asp:PlaceHolder></td>
    </tr>
    <tr>
        <td id="header">
            <asp:PlaceHolder ID="phSiteBanner" runat="server"></asp:PlaceHolder></td>
    </tr>
    <tr>
        <td style="background-color: #504EB9;">
            <asp:PlaceHolder ID="phMainMenu" runat="server"></asp:PlaceHolder></td>
    </tr>
    <tr>
        <td style="background-image: url(/Content/Assets/Uploads/Image/Sites/nav_bottom.jpg);">
            <img src="/Content/Assets/Images/spacer.gif" width="1" height="27" alt="" border="0" /></td>
    </tr>
    <tr>
        <td style="padding: 1px;">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td valign="top" id="about_nav">
                        <asp:PlaceHolder ID="phLeftPanel" runat="server"></asp:PlaceHolder></td>
                    <td valign="top" id="about_content">
                        <h2>
                            <asp:Literal ID="lSectionTitle" runat="server"></asp:Literal></h2>
                        <p>
                            <asp:PlaceHolder ID="plhContent" runat="server"></asp:PlaceHolder></p></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="background-color: #010066;">
            <img src="/Content/Assets/Images/spacer.gif" width="1" height="1" alt="" border="0" /></td>
    </tr>
    <tr>
        <td id="footer" valign="middle">
            <asp:PlaceHolder ID="phSiteFooter" runat="server"></asp:PlaceHolder></td>
    </tr>
</table>