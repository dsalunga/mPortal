<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MCConfirmVote.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.MusicCompetition.MCConfirmVote" %>
<!DOCTYPE html>
<html>
<head>
    <title><%= EventTitle %></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="//code.jquery.com/mobile/1.1.0/jquery.mobile-1.1.0.min.css" />
    <link rel="stylesheet" href="/content/parts/asop/res/css/style.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="//code.jquery.com/mobile/1.1.0/jquery.mobile-1.1.0.min.js"></script>
    <script type="text/javascript">
        $("#pageConfirm").live("pageinit", function (event) {
            $("#buttonVoteAgain").click(function () {
                location.href = "<%= VoteUrl %>";
            });
        });
    </script>
</head>
<body>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewSuccess" runat="server">
            <div data-role="page" id="pageConfirm">
                <div data-role="header" data-theme="e">
                    <h1>Vote Confirmation Successful</h1>
                </div>
                <div data-role="content">
                    <h1>Thank you!</h1>
                    <p>
                        Your email has been confirmed and your vote has been officially counted.
                    </p>
                    <p>
                        You will receive another email as confirmation to your vote.
                    </p>
                    <p>
                        <a href="#" id="buttonVoteAgain" data-role="button" data-inline="true" data-icon="refresh"
                            data-iconpos="right" data-theme="b">Vote again!</a>
                    </p>
                    <p>
                        <em>NOTE:</em>&nbsp;Please note that this voting system will not affect any scores
                        during the finals night. The decision of the board of judges is final.
                    </p>
                </div>
                <div data-role="footer">
                    <h4><%= ShortEventTitle %></h4>
                </div>
            </div>
        </asp:View>
        <asp:View ID="ViewFailed" runat="server">
            <div data-role="page" id="pageConfirm">
                <div data-role="header" data-theme="e">
                    <h1>Confirmation Failed</h1>
                </div>
                <div data-role="content">
                    <h1>Sorry...</h1>
                    <p>
                        The vote you are trying to confirm is either already confirmed or your request is
                        invalid. Please make sure the URL is correct or you have not confirmed your vote
                        previously. Try voting again.
                    </p>
                    <p>
                        <a href="#" id="buttonVoteAgain" data-role="button" data-inline="true" data-icon="refresh"
                            data-iconpos="right" data-theme="b">Vote again!</a>
                    </p>
                    <p>
                        <em>NOTE:</em>&nbsp;Please note that this voting system will not affect any scores
                        during the finals night. The decision of the board of judges is final.
                    </p>
                </div>
                <div data-role="footer">
                    <h4><%= ShortEventTitle %></h4>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</body>
</html>
