<%@ Control Language="C#" %>
<div class="animationbtns">
    <asp:PlaceHolder runat="server" ID="phHeadingTopPane"></asp:PlaceHolder>
</div>
<div id="wrapper_sec">
    <%--<div id="masthead">
        <asp:PlaceHolder runat="server" ID="phMasterHeaderPane"></asp:PlaceHolder>
    </div>--%>
    <asp:PlaceHolder runat="server" ID="phMainBodyPane"></asp:PlaceHolder>
    <div id="content_sec">
        <div class="static">
            <asp:PlaceHolder runat="server" ID="phMainContentPane"></asp:PlaceHolder>
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