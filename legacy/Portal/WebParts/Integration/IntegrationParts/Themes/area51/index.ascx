<%@ Control Language="C#" %>
<div class="animationbtns">
    <asp:PlaceHolder runat="server" ID="phHeadingTopPane"></asp:PlaceHolder>
</div>
<div id="wrapper_sec">
    <%--<div id="masthead">
        <asp:PlaceHolder runat="server" ID="phMasterHeaderPane"></asp:PlaceHolder>
    </div>--%>
    <div id="banner">
        <div runat="server" id="phRotatingBanner" clientidmode="static" class="container-box">
        </div>
        <div class="right_accordion">
            <div runat="server" id="phAccordionMenu" class="glossymenu">
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <asp:PlaceHolder runat="server" ID="phMainBodyPane"></asp:PlaceHolder>
    <div id="content_sec">
        <asp:PlaceHolder runat="server" ID="phElementColumnsPane"></asp:PlaceHolder>
        <div class="upcoming_events">
            <asp:PlaceHolder runat="server" ID="phThreePaneLeft"></asp:PlaceHolder>
        </div>
        <div class="location">
            <asp:PlaceHolder runat="server" ID="phThreePaneMiddle"></asp:PlaceHolder>
        </div>
        <div class="gallery">
            <asp:PlaceHolder runat="server" ID="phThreePaneRight"></asp:PlaceHolder>
        </div>
        <div class="clear">
        </div>
        <div class="programe">
            <div class="progs left">
                <asp:PlaceHolder runat="server" ID="phTwoPaneLeft"></asp:PlaceHolder>
            </div>
            <div class="progs right">
                <asp:PlaceHolder runat="server" ID="phTwoPaneRight"></asp:PlaceHolder>
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="clear">
    </div>
</div>
<div class="clear">
</div>
<asp:PlaceHolder runat="server" ID="phFooterPane"></asp:PlaceHolder>
