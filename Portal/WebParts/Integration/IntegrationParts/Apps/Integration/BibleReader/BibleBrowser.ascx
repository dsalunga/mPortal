<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BibleBrowser.ascx.cs" Inherits="WCMS.WebSystem.Apps.Integration.Bible.BibleBrowser" %>

<style type="text/css">
    .verse-id {
        color: #c0c0c0;
    }

    .verse-list {
        font-size: 16px;
        color: #444;
    }

    .lead {
        color: #999;
    }

    .found {
        font-weight: bold;
        text-decoration: underline;
    }
</style>
<ul class="nav nav-pills">
    <li runat="server" id="tabBrowser" enableviewstate="false">
        <a href="#" runat="server" id="linkBrowser">Browser</a>
    </li>
    <li runat="server" id="tabSearch" enableviewstate="false"><a href="#" runat="server" id="linkSearch">Search</a></li>
</ul>
<br />
<div>
    <asp:DropDownList ID="cboLanguages" CssClass="input" runat="server" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="cboLanguages_SelectedIndexChanged">
        <asp:ListItem Text="All Languages" Value="-2"></asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cboVersions" CssClass="input" Style="margin-right: 10px" runat="server" DataTextField="Name" DataValueField="Id" AutoPostBack="True" OnSelectedIndexChanged="cboVersions_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="cboBooks" CssClass="input" runat="server" DataTextField="Name" DataValueField="Id" AutoPostBack="True" OnSelectedIndexChanged="cboBooks_SelectedIndexChanged">
    </asp:DropDownList>
    <span runat="server" id="panelBookChapters">
        <asp:DropDownList ID="cboChapters" CssClass="input" runat="server" DataTextField="Name" DataValueField="Id" AutoPostBack="True" OnSelectedIndexChanged="cboChapters_SelectedIndexChanged">
        </asp:DropDownList></span>
    <div class="pull-right btn-group" runat="server" id="panelNav" enableviewstate="false">
        <button type="submit" runat="server" clientidmode="static" id="cmdPrevious" class="btn btn-default btn-sm" onserverclick="cmdPrevious_Click" style="min-width: 50px"><span class="glyphicon glyphicon-chevron-left"></span></button>
        <button type="submit" runat="server" clientidmode="static" id="cmdNext" class="btn btn-default btn-sm" onserverclick="cmdNext_Click" style="min-width: 50px"><span class="glyphicon glyphicon-chevron-right"></span></button>
    </div>
    <div runat="server" id="panelSearch" class="no-margin-bottom input-group col-md-5" enableviewstate="false">
        <span class="input-group-addon"><span class="glyphicon glyphicon-search"></span></span>
        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
        <span class="input-group-btn">
            <asp:Button ID="cmdSearch" CssClass="btn btn-default" runat="server" Text="Search" OnClick="cmdSearch_Click" />
        </span>
    </div>
</div>
<br />
<h1 runat="server" id="lblHeader" enableviewstate="false">Book Chapter / Search Results</h1>
<div runat="server" id="panelVerses" class="verse-list" enableviewstate="false"></div>
<div class="pull-right btn-group" runat="server" id="panelNav2" enableviewstate="false">
    <button type="submit" runat="server" id="cmdPrevious2" class="btn btn-default btn-sm" onserverclick="cmdPrevious_Click" style="min-width: 50px"><span class="glyphicon glyphicon-chevron-left"></span></button>
    <button type="submit" runat="server" id="cmdNext2" class="btn btn-default btn-sm" onserverclick="cmdNext_Click" style="min-width: 50px"><span class="glyphicon glyphicon-chevron-right"></span></button>
</div>
<%--<script type="text/javascript" src="/content/plugins/TouchSwipe/jquery.touchSwipe.min.js"></script>--%>
<script type="text/javascript">
    /*
    $(function () {
        //Keep track of how many swipes
        var count = 0;

        //Enable swiping...
        $(".verse-list").swipe({
            //Generic swipe handler for all directions
            swipeLeft: function (event, direction, distance, duration, fingerCount) {
                $('#cmdNext').click();
            },
            swipeRight: function (event, direction, distance, duration, fingerCount) {
                $('#cmdPrevious').click();
            },
            //Default is 75px, set to 0 for demo so any distance triggers swipe
            threshold: 25,
            maxTimeThreshold: 500
        });
    });
    */
</script>