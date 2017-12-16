<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileWall.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Social.WallController" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Import Namespace="WCMS.WebSystem" %>
<script runat="server">
    public string VoteUrl = "";
    public string UserName = "";
    public FrameworkData fxJson = new FrameworkData();
    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            var context = new WContext(this);
            VoteUrl = context.Element.GetParameterValue("VoteUrl", "/Public/ASOP.aspx");
            fxJson.UserId = WSession.Current.UserId;
            fxJson.PageId = context.PageId;
            fxJson.SiteId = context.Page.SiteId;
            if (WSession.Current.IsLoggedIn)
                UserName = WSession.Current.User.UserName;
        }
        base.OnLoad(e);
    }
</script>
<!DOCTYPE html>
<html>
<head>
    <title>ASOP Music Festival SG-Mid-Year 2012 People's Choice Nomination</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="//code.jquery.com/mobile/1.1.0/jquery.mobile-1.1.0.min.css" />
    <link rel="stylesheet" href="<%=WebUtil.Version("~/content/parts/social/css/social.min.css")%>" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="//code.jquery.com/mobile/1.1.0/jquery.mobile-1.1.0.min.js"></script>
    <script type="text/javascript" src="<%=WebUtil.Version("~/content/assets/scripts/wcms.min.js")%>"></script>
</head>
<body data-fx='{"userId":<%=fxJson.UserId%>,"pageId":<%=fxJson.PageId%>,"siteId":<%=fxJson.SiteId%>}'>
    <div data-role="page" id="pageDone">
        <div data-role="header" data-theme="e">
            <h1>
                Voting Successful</h1>
        </div>
        <div data-role="content">
            <h1>
                Thank you!</h1>
            <p>
                Your vote has been recorded and your voting code has been included in the lucky
                dip.</p>
            <p>
                <a href="#" id="buttonDone" data-role="button" data-inline="true" data-icon="home"
                    data-iconpos="right" data-theme="b">Done</a><a href="#" id="buttonVoteAgain" data-role="button"
                        data-inline="true" data-icon="refresh" data-iconpos="right" data-theme="b">Vote
                        again!</a></p>
            <p>
                &nbsp;</p>
            <h2>
                ASOP Discussion Wall</h2>
            <form runat="server" data-role="none">
            <div id="panelWall" data-wall='{"oid":<%=ObjectId %>,"rid":<%=RecordId %>}'>
                <div id="panelNewWallPost" runat="server" clientidmode="Static" style="border: 1px solid #B4BBCD;">
                    <textarea id="txtNewPost" data-role="none" class="newPostText" runat="server" placeholder="Write a message..."></textarea>
                    <div class="uiPostControlContainer">
                        <input type="checkbox" data-role="none" id="chkSendEmail" runat="server" clientidmode="Static" /><label
                            for="chkSendEmail">E-mail</label>&nbsp;
                        <input type="checkbox" data-role="none" id="chkSendSMS" runat="server" clientidmode="Static" /><label
                            for="chkSendEmail">SMS</label>
                        <asp:Button data-role="none" ID="cmdPost" runat="server" Text="Post" Width="65px"
                            OnClick="cmdPost_Click" />
                    </div>
                </div>
                <div id="panelWallUpdates" runat="server" clientidmode="Static" style="padding-top: 10px;">
                </div>
            </div>
            </form>
        </div>
        <div data-role="footer">
            <h4>
                ASOP Music Festival SG-Mid-Year 2012
            </h4>
        </div>
    </div>
    <script type="text/javascript" src="<%=WebUtil.Version("~/content/parts/social/js/social.min.js")%>"></script>
    <script type="text/javascript">
        var userName = "<% =UserName %>";
        var voteHref = "<% =VoteUrl %>";

        if (userName == "") {
            location.href = voteHref;
        }

        $("#pageDone").live("pageinit", function (event) {
            $("#buttonDone").click(function () {
                logOffUser();
                location.href = voteHref;
            });

            $("#buttonVoteAgain").click(function () {
                location.href = voteHref;
            });
        });

        function logOffUser() {
            userName = "";

            $.ajax({
                type: "POST",
                url: "/content/parts/common/FxService.asmx/LogOff",
                data: JSON.stringify({}),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    // Do nothing
                },
                error: function (request, status, error) {
                    $.mobile.hidePageLoadingMsg();

                    showAlert("An error has accurred, please contact support.", "Error", "#pageLogin");
                }
            });
        }
    </script>
</body>
</html>
